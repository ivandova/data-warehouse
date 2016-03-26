using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Wastedge.DataWarehouse.Service
{
    public partial class WindowsService : ServiceBase
    {
        private static ILog Log;

        public const string EventLogSource = "WEDW";
        public const string EventLogName = "Wastedge Data Warehouse";

        private Daemon _daemon;
        private string _serviceName;

        public WindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _serviceName = GetServiceName();

                int instanceId;
                DataWarehouseConfiguration.TryParseServiceName(_serviceName, out instanceId);

                LogUtils.SetupEventLogging(EventLogSource, EventLogName, instanceId);
                Log = LogManager.GetLogger(typeof(WindowsService));

                Log.Info($"Starting Wastedge Data Warehouse {_serviceName}");

                _daemon = new Daemon(_serviceName, false);

                Log.Info($"Started Wastedge Data Warehouse {_serviceName}");
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                throw;
            }
        }

        private static string GetServiceName()
        {
            int processId = Process.GetCurrentProcess().Id;

            return (string)new ManagementObjectSearcher("SELECT * FROM Win32_Service where ProcessId = " + processId)
                .Get()
                .Cast<ManagementBaseObject>()
                .First()["Name"];
        }

        protected override void OnStop()
        {
            try
            {
                Log.Info($"Stopping Wastedge Data Warehouse {_serviceName}");

                if (_daemon != null)
                {
                    _daemon.Dispose();
                    _daemon = null;
                }

                Log.Info($"Stopped Wastedge Data Warehouse {_serviceName}");
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                throw;
            }
        }
    }
}
