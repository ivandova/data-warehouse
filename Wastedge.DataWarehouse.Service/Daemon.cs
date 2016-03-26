using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Wastedge.DataWarehouse.Service
{
    public class Daemon : IDisposable
    {
        private Synchronizer _synchronizer;
        private bool _disposed;

        public Daemon(string serviceName, bool debug)
        {
            if (serviceName == null)
                throw new ArgumentNullException(nameof(serviceName));

            int instanceId = GetInstanceId(serviceName);

            var configuration = DataWarehouseConfiguration.Load(GetHive(debug), instanceId);
            configuration.Validate();

            _synchronizer = new Synchronizer(configuration);
        }

        private static RegistryKey GetHive(bool debug)
        {
            return debug ? Registry.CurrentUser : Registry.LocalMachine;
        }

        private int GetInstanceId(string serviceName)
        {
            int instanceId;
            if (DataWarehouseConfiguration.TryParseServiceName(serviceName, out instanceId))
                return instanceId;

            throw new InvalidOperationException($"Cannot get instance ID from service name {serviceName}");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_synchronizer != null)
                {
                    _synchronizer.Dispose();
                    _synchronizer = null;
                }

                _disposed = true;
            }
        }
    }
}
