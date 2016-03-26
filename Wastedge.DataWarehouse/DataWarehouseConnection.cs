using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wastedge.DataWarehouse.Provider.MSSql;
using WastedgeApi;

namespace Wastedge.DataWarehouse
{
    public class DataWarehouseConnection
    {
        private readonly IDataWarehouseProvider _provider;

        public Api Api { get; }

        public DataWarehouseConnection(DataWarehouseProvider provider, string connectionString, ApiCredentials credentials)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));

            Api = new Api(credentials);

            switch (provider)
            {
                case DataWarehouseProvider.MSSql:
                    _provider = new MSSqlProvider(connectionString);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
            }
        }

        public void MigrateSupportSchema()
        {
            _provider.MigrateSupportSchema();
        }

        public void MigrateSynchronizedSchema()
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                foreach (var table in _provider.GetApiTables(connection))
                {
                    _provider.MigrateSynchronizedTable(connection, Api.GetEntitySchema(table.Path));
                }
            }
        }

        public void Synchronize()
        {
            Synchronize(SynchronizeMode.All);
        }

        public void Synchronize(SynchronizeMode mode)
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                foreach (var table in _provider.GetApiTables(connection))
                {
                    _provider.Synchronize(connection, Api, Api.GetEntitySchema(table.Path), mode);
                }
            }
        }

        public List<LogLine> GetLog(DateTime? since, int count)
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                return _provider.GetLog(connection, since, count);
            }
        }

        public void ClearLog()
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                _provider.ClearLog(connection);
            }
        }

        public List<string> GetSynchronized()
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                return _provider.GetApiTables(connection).Select(p => p.Path).ToList();
            }
        }

        public void AddSynchronized(string path)
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                _provider.AddApiTable(connection, path);
            }
        }

        public void RemoveSynchronized(string path)
        {
            using (var connection = _provider.OpenConnection())
            {
                connection.Open();

                _provider.RemoveApiTable(connection, path);
            }
        }
    }
}
