using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemEx.Windows.Forms;
using Wastedge.DataWarehouse.Manager.Util;

namespace Wastedge.DataWarehouse.Manager
{
    public partial class ExplorerForm : Util.DockContent
    {
        private readonly Dictionary<Instance, InstanceData> _treeNodes = new Dictionary<Instance, InstanceData>();

        public ObservableCollection<Instance> Instances { get; }

        private InstanceData SelectedInstance
        {
            get
            {
                var treeNode = _treeView.SelectedNode;
                while (treeNode != null && !(treeNode.Tag is InstanceData))
                {
                    treeNode = treeNode.Parent;
                }

                return treeNode?.Tag as InstanceData;
            }
        }

        private NodeType? SelectedNodeType => _treeView.SelectedNode?.Tag as NodeType?;

        public ExplorerForm()
        {
            InitializeComponent();

            VisualStyleUtil.StyleTreeView(_treeView);

            Instances = new ObservableCollection<Instance>();
            Instances.CollectionChanged += Instances_CollectionChanged;

            foreach (var instance in DataWarehouseConfiguration.LoadAll(Program.BaseKey)
                .Select(p => new Instance(p))
                .OrderBy(p => p.Configuration.InstanceId)
            ) {
                Instances.Add(instance);
            }
        }

        private void Instances_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Instance instance in e.OldItems)
                {
                    instance.Changed -= Instance_Changed;

                    InstanceData data;
                    if (_treeNodes.TryGetValue(instance, out data))
                    {
                        _treeNodes.Remove(instance);
                        data.TreeNode.Remove();
                        data.Log?.Dispose();
                        data.SynchronizedTables?.Dispose();
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (Instance instance in e.NewItems)
                {
                    instance.Changed += Instance_Changed;
                    _treeNodes.Add(instance, CreateTreeNode(instance));
                }
            }
        }

        private InstanceData CreateTreeNode(Instance instance)
        {
            var treeNode = new TreeNode();
            var instanceData = new InstanceData(instance, treeNode);
            treeNode.Tag = instanceData;

            _treeView.Nodes.Add(treeNode);

            UpdateTreeNode(treeNode);

            treeNode.Nodes.Add(new TreeNode
            {
                Text = "Log",
                ImageIndex = (int)ImageIndex.Log,
                SelectedImageIndex = (int)ImageIndex.Log,
                Tag = NodeType.Log
            });

            treeNode.Nodes.Add(new TreeNode
            {
                Text = "Synchronized Tables",
                ImageIndex = (int)ImageIndex.Synchronize,
                SelectedImageIndex = (int)ImageIndex.Synchronize,
                Tag = NodeType.Synchronize
            });

            treeNode.Expand();

            return instanceData;
        }

        private void UpdateTreeNode(TreeNode treeNode)
        {
            var instance = ((InstanceData)treeNode.Tag).Instance;

            treeNode.Text = instance.Configuration.InstanceName;
            treeNode.ImageIndex = treeNode.SelectedImageIndex = (int)GetImageIndex(instance.Status);
        }

        private ImageIndex GetImageIndex(ServiceControllerStatus? status)
        {
            if (status == null)
                return ImageIndex.Error;

            switch (status.Value)
            {
                case ServiceControllerStatus.ContinuePending:
                case ServiceControllerStatus.Paused:
                case ServiceControllerStatus.PausePending:
                case ServiceControllerStatus.StartPending:
                case ServiceControllerStatus.StopPending:
                    return ImageIndex.StartStop;
                case ServiceControllerStatus.Running:
                    return ImageIndex.Start;
                case ServiceControllerStatus.Stopped:
                    return ImageIndex.Stop;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Instance_Changed(object sender, EventArgs e)
        {
            UpdateTreeNode(_treeNodes[(Instance)sender].TreeNode);
        }

        private void _refresh_Click(object sender, EventArgs e)
        {
            foreach (var instance in Instances)
            {
                instance.RefreshStatus();
            }
        }

        private void _new_Click(object sender, EventArgs e)
        {
            using (var form = new EditInstanceForm())
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                var instance = new Instance(form.Configuration);

                Instances.Add(instance);

                _treeView.SelectedNode = _treeNodes[instance].TreeNode;

                ValidateInstance(instance);
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new EditInstanceForm(SelectedInstance.Instance.Configuration))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                ValidateInstance(SelectedInstance.Instance);
            }
        }

        private void ValidateInstance(Instance instance)
        {
            // Verify whether the Windows service has even been installed.

            instance.RefreshStatus();

            if (instance.Status == null)
            {
                instance.Install();

                InstanceControl(instance, InstanceControlAction.Start);

                return;
            }

            // If it already was installed, see whether we need to restart it.

            if (instance.Status == ServiceControllerStatus.Running)
            {
                var result = MessageBox.Show(
                    this,
                    "The instance needs to be restarted to apply the configuration. Do you want to restart the instance?",
                    FindForm().Text,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                    InstanceControl(instance, InstanceControlAction.Restart);
            }
        }

        private void _contextMenu_Opening(object sender, CancelEventArgs e)
        {
            _contextMenu.Enabled = SelectedInstance != null;
            if (SelectedInstance == null)
                return;

            var instance = SelectedInstance.Instance;

            instance.RefreshStatus();

            startToolStripMenuItem.Enabled =
            deleteToolStripMenuItem.Enabled =
                instance.Status == ServiceControllerStatus.Stopped;

            restartToolStripMenuItem.Enabled =
            stopToolStripMenuItem.Enabled =
                instance.Status == ServiceControllerStatus.Running;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanceControl(SelectedInstance.Instance, InstanceControlAction.Start);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanceControl(SelectedInstance.Instance, InstanceControlAction.Stop);
        }

        private void InstanceControl(Instance instance, InstanceControlAction control)
        {
            using (var form = new InstanceControlForm(instance, control))
            {
                form.ShowDialog(this);
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanceControl(SelectedInstance.Instance, InstanceControlAction.Restart);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                this,
                "Are you sure you want to delete this instance?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            SelectedInstance.Instance.Remove();

            Instances.Remove(SelectedInstance.Instance);
        }

        private enum ImageIndex
        {
            Start,
            StartStop,
            Stop,
            Error,
            Log,
            Synchronize
        }

        private void _treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var nodeType = SelectedNodeType;
            var instanceData = SelectedInstance;

            if (e.Button != MouseButtons.Left || !nodeType.HasValue || instanceData == null)
                return;

            switch (nodeType.Value)
            {
                case NodeType.Log:
                    if (instanceData.Log != null)
                    {
                        instanceData.Log.DockHandler.Activate();
                        return;
                    }

                    instanceData.Log = new LogForm(instanceData.Instance);
                    instanceData.Log.Disposed += (s, ea) => instanceData.Log = null;
                    instanceData.Log.Show(((MainForm)Parent.FindForm()).DockPanel);
                    break;

                case NodeType.Synchronize:
                    if (instanceData.SynchronizedTables != null)
                    {
                        instanceData.SynchronizedTables.DockHandler.Activate();
                        return;
                    }

                    instanceData.SynchronizedTables = new SynchronizedTablesForm(instanceData.Instance);
                    instanceData.SynchronizedTables.Disposed += (s, ea) => instanceData.SynchronizedTables = null;
                    instanceData.SynchronizedTables.Show(((MainForm)Parent.FindForm()).DockPanel);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var nodeType = SelectedNodeType;
            var instanceData = SelectedInstance;

            if (!nodeType.HasValue || instanceData == null)
                return;

            switch (nodeType.Value)
            {
                case NodeType.Log:
                    instanceData.Log?.DockHandler.Activate();
                    break;

                case NodeType.Synchronize:
                    instanceData.SynchronizedTables?.DockHandler.Activate();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum NodeType
        {
            Log,
            Synchronize
        }

        private class InstanceData
        {
            public TreeNode TreeNode { get; }
            public Instance Instance { get; }
            public LogForm Log { get; set; }
            public SynchronizedTablesForm SynchronizedTables { get; set; }

            public InstanceData(Instance instance, TreeNode treeNode)
            {
                TreeNode = treeNode;
                Instance = instance;
            }
        }
    }
}
