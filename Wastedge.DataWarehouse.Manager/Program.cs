using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Wastedge.DataWarehouse.Manager.Util;

namespace Wastedge.DataWarehouse.Manager
{
    static class Program
    {
        public static RegistryKey BaseKey => Registry.LocalMachine;

        [STAThread]
        static void Main()
        {
            ToolStripManager.Renderer = new VS2012ToolStripRenderer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
