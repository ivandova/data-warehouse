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

namespace Wastedge.DataWarehouse.Manager
{
    public partial class ExplorerForm : Util.DockContent
    {
        private readonly Dictionary<Instance, TreeNode> _treeNodes = new Dictionary<Instance, TreeNode>();

        public ObservableCollection<Instance> Instances { get; }

        private Instance SelectedInstance
        {
            get
            {
                var treeNode = _treeView.SelectedNode;
                while (treeNode != null && !(treeNode.Tag is Instance))
                {
                    treeNode = treeNode.Parent;
                }

                return treeNode?.Tag as Instance;
            }
        }

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
                    if (_treeNodes.Remove(instance))
                        _treeNodes[instance].Remove();
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

        private TreeNode CreateTreeNode(Instance instance)
        {
            var treeNode = new TreeNode
            {
                Tag = instance
            };

            _treeView.Nodes.Add(treeNode);

            UpdateTreeNode(treeNode);

            return treeNode;
        }

        private void UpdateTreeNode(TreeNode treeNode)
        {
            var instance = (Instance)treeNode.Tag;

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
            UpdateTreeNode(_treeNodes[(Instance)sender]);
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

                _treeView.SelectedNode = _treeNodes[instance];

                ValidateInstance(instance);
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new EditInstanceForm(SelectedInstance.Configuration))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                ValidateInstance(SelectedInstance);
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

            startToolStripMenuItem.Enabled =
                SelectedInstance.Status == ServiceControllerStatus.Stopped;

            restartToolStripMenuItem.Enabled =
            stopToolStripMenuItem.Enabled =
                SelectedInstance.Status == ServiceControllerStatus.Running;
        }

        private enum ImageIndex
        {
            Start,
            StartStop,
            Stop,
            Error
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanceControl(SelectedInstance, InstanceControlAction.Start);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanceControl(SelectedInstance, InstanceControlAction.Stop);
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
            InstanceControl(SelectedInstance, InstanceControlAction.Restart);
        }
    }
}
