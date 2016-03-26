using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WastedgeApi;

namespace Wastedge.DataWarehouse
{
    internal interface IDataWarehouseProvider
    {
        void MigrateSupportSchema();
        DbConnection OpenConnection();
        NameValueCollection GetConfiguration(DbConnection connection);
        void SetConfiguration(DbConnection connection, string name, string value);
        ApiTableCollection GetApiTables(DbConnection connection);
        void SetApiTableLastUpdate(DbConnection connection, string path, DateTime? lastUpdate);
        void MigrateSynchronizedTable(DbConnection connection, EntitySchema schema);
        void Synchronize(DbConnection connection, Api api, EntitySchema schema, SynchronizeMode mode);
    }
}
