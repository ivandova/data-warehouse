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
        private readonly Api _api;

        public DataWarehouseConnection(DataWarehouseProvider provider, string connectionString, ApiCredentials credentials)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));

            _api = new Api(credentials);

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
                    _provider.MigrateSynchronizedTable(connection, _api.GetEntitySchema(table.Path));
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
                    _provider.Synchronize(connection, _api, _api.GetEntitySchema(table.Path), mode);
                }
            }
        }
    }
}
