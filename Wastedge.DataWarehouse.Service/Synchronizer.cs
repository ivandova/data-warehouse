using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using WastedgeApi;

namespace Wastedge.DataWarehouse.Service
{
    internal class Synchronizer : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Synchronizer));
        private static readonly TimeSpan ConnectRetryInterval = TimeSpan.FromSeconds(10);

        private readonly DataWarehouseConfiguration _configuration;
        private Thread _thread;
        private bool _disposed;
        private ManualResetEvent _event = new ManualResetEvent(false);
        private DataWarehouseConnection _connection;

        public Synchronizer(DataWarehouseConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;

            _thread = new Thread(ThreadProc) { Name = "Synchronizer" };
            _thread.Start();
        }

        private void ThreadProc()
        {
            while (true)
            {
                Log.Info("Connecting to Wastedge");

                try
                {
                    Connect();
                    break;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                if (_event.WaitOne(ConnectRetryInterval))
                    return;
            }

            int trackedInterval = _configuration.TrackedTableInterval.Value;
            int untrackedInterval = _configuration.UntrackedTableInterval.Value;

            var trackedStopwatch = Stopwatch.StartNew();
            var untrackedStopwatch = Stopwatch.StartNew();

            while (true)
            {
                SynchronizeMode mode = 0;

                if (trackedStopwatch.Elapsed.TotalSeconds >= trackedInterval)
                {
                    mode |= SynchronizeMode.Tracked;
                    trackedStopwatch.Restart();

                    Log.Debug("Synchronizing tracked");
                }

                if (untrackedStopwatch.Elapsed.TotalSeconds >= untrackedInterval)
                {
                    mode |= SynchronizeMode.Untracked;
                    untrackedStopwatch.Restart();

                    Log.Debug("Synchronizing untracked");
                }

                try
                {
                    Synchronize(mode);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                int wait = Math.Min(
                    trackedInterval - (int)trackedStopwatch.Elapsed.TotalSeconds,
                    untrackedInterval - (int)untrackedStopwatch.Elapsed.TotalSeconds
                );

                wait = Math.Max(wait, 0);

                if (_event.WaitOne(TimeSpan.FromSeconds(wait)))
                    return;
            }
        }

        private void Connect()
        {
            _connection = _configuration.OpenConnection();

            Log.Info("Migrating database support tables");

            _connection.MigrateSupportSchema();

            Log.Info("Migrating synchronized schema");

            _connection.MigrateSynchronizedSchema();
        }

        private void Synchronize(SynchronizeMode mode)
        {
            _connection.Synchronize(mode);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _event?.Set();

                if (_thread != null)
                {
                    _thread.Join();
                    _thread = null;
                }

                if (_event != null)
                {
                    _event.Dispose();
                    _event = null;
                }

                _disposed = true;
            }
        }
    }
}
