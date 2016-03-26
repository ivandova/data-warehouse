using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemEx.Windows.Forms;
using Wastedge.DataWarehouse.Manager.Util;

namespace Wastedge.DataWarehouse.Manager
{
    public partial class SynchronizedTablesForm : DockContent
    {
        private readonly DataWarehouseConnection _connection;

        public SynchronizedTablesForm(Instance instance)
        {
            InitializeComponent();

            VisualStyleUtil.StyleListView(_synchronized);
            VisualStyleUtil.StyleListView(_available);

            Text = String.Format(Text, instance.Configuration.InstanceName);

            _connection = instance.Configuration.OpenConnection();

            var entities = new HashSet<string>(_connection.Api.GetSchema().Entities);
            var synchronized = _connection.GetSynchronized();

            foreach (var table in synchronized.OrderBy(p => p))
            {
                _synchronized.Items.Add(table);
                entities.Remove(table);
            }

            foreach (var table in entities.OrderBy(p => p))
            {
                _available.Items.Add(table);
            }

            UpdateEnabled();
        }

        private void _synchronized_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        private void UpdateEnabled()
        {
            _remove.Enabled = _synchronized.SelectedItems.Count > 0;
            _add.Enabled = _available.SelectedItems.Count > 0;
        }

        private void _available_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        private void _add_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                this,
                "Adding tables will immediately start synchronizing them. Are you sure you want to continue?",
                Parent.FindForm().Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            foreach (ListViewItem item in _available.SelectedItems)
            {
                _connection.AddSynchronized(item.Text);
                item.Remove();
                _synchronized.Items.Add(item.Text);
            }

            _synchronized.Sort();
        }

        private void _remove_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                this,
                "Removing tables will immediately stop synchronizing them. If you want to start synchronzing them later on, synchronization will have to start over with a complete dump. Are you sure you want to continue?",
                Parent.FindForm().Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            foreach (ListViewItem item in _synchronized.SelectedItems)
            {
                _connection.RemoveSynchronized(item.Text);
                item.Remove();
                _available.Items.Add(item.Text);
            }

            _available.Sort();
        }
    }
}
