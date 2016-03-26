using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Wastedge.DataWarehouse.Service;

namespace Wastedge.DataWarehouse.Manager
{
    public class Instance : IDisposable
    {
        private bool _disposed;
        private ServiceController _serviceController;

        public DataWarehouseConfiguration Configuration { get; }

        public ServiceControllerStatus? Status { get; private set; }

        public event EventHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public Instance(DataWarehouseConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;

            RefreshStatus();
        }

        public void RaiseChanged()
        {
            OnChanged(EventArgs.Empty);
        }

        public void RefreshStatus()
        {
            if (_serviceController == null)
            {
                string serviceName = Configuration.InstanceName;
                _serviceController = ServiceController.GetServices().SingleOrDefault(p => p.ServiceName == serviceName);

                if (_serviceController == null)
                    return;
            }

            try
            {
                _serviceController.Refresh();
                Status = _serviceController.Status;
            }
            catch (InvalidOperationException)
            {
                _serviceController = null;
            }

            RaiseChanged();
        }

        public void Install()
        {
            Util.ServiceInstaller.Install(
                Configuration.InstanceName,
                $"Wastedge Data Warehouse - Instance {Configuration.InstanceId}",
                typeof(Daemon).Assembly.Location,
                Util.ServiceInstaller.ServiceBootFlag.AutoStart
            );

            RefreshStatus();
        }

        public void Remove()
        {
            Util.ServiceInstaller.Uninstall(Configuration.InstanceName);

            Configuration.Delete(Program.BaseKey);

            RefreshStatus();
        }

        public void Start()
        {
            _serviceController.Start();

            RefreshStatus();
        }

        public void Stop()
        {
            _serviceController.Stop();

            RefreshStatus();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_serviceController != null)
                {
                    _serviceController.Dispose();
                    _serviceController = null;
                }

                _disposed = true;
            }
        }
    }
}
