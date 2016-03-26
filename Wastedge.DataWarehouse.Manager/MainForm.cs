using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Wastedge.DataWarehouse.Manager
{
    public partial class MainForm : SystemEx.Windows.Forms.Form
    {
        private readonly ExplorerForm _explorer;

        public ObservableCollection<Instance> Instances => _explorer.Instances;

        public MainForm()
        {
            InitializeComponent();

            _dockPanel.Theme = new VS2012LightTheme();

            _explorer = new ExplorerForm();

            _explorer.Show(_dockPanel, DockState.DockLeft);
        }
    }
}
