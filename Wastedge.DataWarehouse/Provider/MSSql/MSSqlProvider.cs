using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WastedgeApi;

namespace Wastedge.DataWarehouse.Provider.MSSql
{
    internal class MSSqlProvider : IDataWarehouseProvider
    {
        private readonly string _connectionString;

        public MSSqlProvider(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public void MigrateSupportSchema()
        {
            using (var connection = OpenConnection())
            {
                connection.Open();

                int currentVersion = GetCurrentVersion(connection);

                for (int i = currentVersion + 1; ; i++)
                {
                    string resourceName = $"{GetType().Namespace}.Migrate.{i:000}.sql";

                    using (var stream = GetType().Assembly.GetManifestResourceStream(resourceName))
                    {
                        if (stream == null)
                            break;

                        using (var reader = new StreamReader(stream))
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = reader.ReadToEnd();
                                command.ExecuteNonQuery();
                            }

                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = @"
                                    update dwh_migrate set version = @version
                                    if @@rowcount = 0
                                        insert into dwh_migrate (version) values (@version)
                                ";
                                AddParameter(command, "version", i);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private static void AddParameter(DbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            if (parameter.Value is DateTime)
                ((SqlParameter)parameter).SqlDbType = SqlDbType.DateTime2;
            command.Parameters.Add(parameter);
        }

        private bool TableExists(DbConnection connection, string tableName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from information_schema.tables where table_name = @table";
                AddParameter(command, "table", tableName);
                return (int)command.ExecuteScalar() > 0;
            }
        }

        private int GetCurrentVersion(DbConnection connection)
        {
            // Check whether the dwh_migrate table exists.

            if (!TableExists(connection, "dwh_migrate"))
                return 0;

            // Get the current version from the migration table.

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select version from dwh_migrate";
                return (int)command.ExecuteScalar();
            }
        }

        public DbConnection OpenConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public NameValueCollection GetConfiguration(DbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var result = new NameValueCollection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select name, value from dwh_config";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }

            return result;
        }

        public void SetConfiguration(DbConnection connection, string name, string value)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (value == null)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from dwh_config where name = @name";
                    AddParameter(command, "name", name);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        update dwh_config set value = @value where name = @name
                        if @@rowcount = 0
                            insert into dwh_config (name, value) values (@name, @value)
                    ";
                    AddParameter(command, "name", name);
                    AddParameter(command, "value", value);
                    if (command.ExecuteNonQuery() > 0)
                    command.ExecuteNonQuery();
                }
            }
        }

        public ApiTableCollection GetApiTables(DbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var result = new ApiTableCollection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select path, last_update from dwh_synchronize";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ApiTable(reader.GetString(0), reader.GetDateTimeOpt(1)));
                    }
                }
            }

            return result;
        }

        public void SetApiTableLastUpdate(DbConnection connection, string path, DateTime? lastUpdate)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update dwh_synchronize set last_update = @last_update where path = @path";
                AddParameter(command, "path", path);
                AddParameter(command, "last_update", lastUpdate);
                command.ExecuteNonQuery();
            }
        }

        public void MigrateSynchronizedTable(DbConnection connection, EntitySchema schema)
        {
            string tableName = GetTableName(schema);

            if (TableExists(connection, tableName))
                UpdateTable(connection, tableName, schema);
            else
                CreateTable(connection, tableName, schema);
        }

        private static string GetTableName(EntitySchema schema)
        {
            return schema.Name.Replace('/', '_');
        }

        private void CreateTable(DbConnection connection, string tableName, EntitySchema schema)
        {
            var sql = new SqlWriter();

            sql
                .Write("create table").WriteName(tableName).WriteLine(" (")
                .WriteName("$id").Write(" nvarchar(200) not null primary key");

            foreach (var field in schema.Members.OfType<EntityTypedField>())
            {
                if (field is EntityIdField)
                    continue;

                sql.WriteLine(",").WriteName(field.Name).Write(" ").Write(GetDataType(field));
            }

            sql.WriteLine("").Write(")");

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql.ToString();
                command.ExecuteNonQuery();
            }
        }

        private static string GetDataType(EntityTypedField field)
        {
            switch (field.DataType)
            {
                case EntityDataType.Bytes:
                    return "varbyte(max)";
                case EntityDataType.String:
                    return "nvarchar(max)";
                case EntityDataType.Date:
                    return "date";
                case EntityDataType.DateTime:
                    return "datetime2(3)";
                case EntityDataType.DateTimeTz:
                    return "datetimeoffset";
                case EntityDataType.Decimal:
                    return $"decimal(18, {field.Decimals.GetValueOrDefault(0)})";
                case EntityDataType.Long:
                    return "bigint";
                case EntityDataType.Int:
                    return "int";
                case EntityDataType.Bool:
                    return "bit";
                default:
                    throw new InvalidOperationException();
            }
        }

        private void UpdateTable(DbConnection connection, string tableName, EntitySchema schema)
        {
            // Get all the field names that we already have.

            var fieldNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select column_name from information_schema.columns where table_name = @table";
                AddParameter(command, "table", tableName);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fieldNames.Add(reader.GetString(0));
                    }
                }
            }

            // Add the fields we're missing.

            var sql = new SqlWriter();
            bool hadOne = false;

            foreach (var field in schema.Members.OfType<EntityTypedField>().Where(p => !fieldNames.Contains(p.Name)))
            {
                if (!hadOne)
                {
                    hadOne = true;
                    sql.Write("alter table ").WriteName(tableName).Write(" add");
                }
                else
                {
                    sql.Write(",");
                }

                sql.Write(" ").WriteName(field.Name).Write(" ").Write(GetDataType(field));
            }

            if (hadOne)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Synchronize(DbConnection connection, Api api, EntitySchema schema, SynchronizeMode mode)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));

            var schemaMode = schema.Members.Contains("update_timestamp") ? SynchronizeMode.Tracked : SynchronizeMode.Untracked;
            if (!mode.HasFlag(schemaMode))
                return;

            var filters = new List<Filter>();

            if (schemaMode == SynchronizeMode.Tracked)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select last_update from dwh_synchronize where path = @path";
                    AddParameter(command, "path", schema.Name);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return;

                        var lastUpdate = reader.GetDateTimeOpt(0);

                        if (lastUpdate.HasValue)
                        {
                            var field = (EntityPhysicalField)schema.Members["update_timestamp"];

                            filters.Add(new Filter(field, FilterType.GreaterThan, lastUpdate.Value));
                            filters.Add(new Filter(field, FilterType.NotIsNull, null));
                        }
                    }
                }
            }

            string tableName = GetTableName(schema);
            var sql = new SqlWriter();

            sql.Write("update ").WriteName(tableName).Write(" set ");

            bool hadOne = false;
            int index = 0;

            foreach (var field in schema.Members.OfType<EntityTypedField>())
            {
                if (field is EntityIdField)
                    continue;

                if (hadOne)
                    sql.Write(", ");
                else
                    hadOne = true;

                sql.WriteName(field.Name).Write($" = @p{index++}");
            }

            sql.Write(" where ").WriteName("$id").WriteLine(" = @id").WriteLine("if @@rowcount = 0").Write("insert into ").WriteName(tableName).Write(" (").WriteName("$id");

            foreach (var field in schema.Members.OfType<EntityTypedField>())
            {
                if (field is EntityIdField)
                    continue;

                sql.Write(", ").WriteName(field.Name);
            }

            sql.Write(") values (@id");

            index = 0;

            foreach (var field in schema.Members.OfType<EntityTypedField>())
            {
                if (field is EntityIdField)
                    continue;

                sql.Write($", @p{index++}");
            }

            sql.Write(")");

            var logger = new Logger(connection, schema);

            logger.Start();

            try
            {
                var resultSet = api.Query(schema, filters);

                while (resultSet.Next())
                {
                    logger.HaveResults(resultSet.RowCount);

                    using (var transaction = connection.BeginTransaction())
                    {
                        DateTime? lastUpdate = null;

                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = sql.ToString();

                            var parameters = new Dictionary<string, DbParameter>();

                            var parameter = command.CreateParameter();
                            parameters.Add("$id", parameter);
                            parameter.ParameterName = "id";
                            command.Parameters.Add(parameter);

                            index = 0;

                            foreach (var field in schema.Members.OfType<EntityTypedField>())
                            {
                                if (field is EntityIdField)
                                    continue;

                                parameter = command.CreateParameter();
                                parameters.Add(field.Name, parameter);
                                parameter.ParameterName = $"p{index++}";
                                command.Parameters.Add(parameter);
                            }

                            do
                            {
                                for (int i = 0; i < resultSet.FieldCount; i++)
                                {
                                    var fieldName = resultSet.GetFieldName(i);

                                    if (!parameters.TryGetValue(fieldName, out parameter))
                                        throw new InvalidOperationException();

                                    parameter.Value = resultSet[i] ?? DBNull.Value;
                                    if (parameter.Value is DateTime)
                                        ((SqlParameter)parameter).SqlDbType = SqlDbType.DateTime2;

                                    if (
                                        String.Equals(fieldName, "update_timestamp", StringComparison.OrdinalIgnoreCase) &&
                                        !resultSet.IsNull(i)
                                        )
                                    {
                                        var thisLastUpdate = resultSet.GetDateTime(i);
                                        if (lastUpdate == null || thisLastUpdate > lastUpdate.Value)
                                            lastUpdate = thisLastUpdate;
                                    }
                                }

                                command.ExecuteNonQuery();
                            }
                            while (resultSet.Next());
                        }

                        if (lastUpdate.HasValue)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                command.CommandText = "update dwh_synchronize set last_update = @last_update where path = @path";
                                AddParameter(command, "last_update", lastUpdate.Value);
                                AddParameter(command, "path", schema.Name);
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }

                    if (!resultSet.HasMore)
                        break;

                    logger.Start();

                    resultSet = resultSet.GetNext();
                }
            }
            catch (Exception ex)
            {
                logger.SetException(ex);
            }
            finally
            {
                logger.End();
            }
        }

        public List<LogLine> GetLog(DbConnection connection, DateTime? since, int count)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var result = new List<LogLine>();

            string sql = $"select top {count} start, [end], duration, path, error, record_count from dwh_log";

            if (since.HasValue)
                sql += " where start > @start";

            sql += " order by start";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                if (since.HasValue)
                    AddParameter(command, "start", since.Value);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new LogLine(
                            reader.GetDateTime(0),
                            reader.GetDateTime(1),
                            reader.GetInt32(2),
                            reader.GetString(3),
                            reader.IsDBNull(4) ? null : reader.GetString(4),
                            reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                        ));
                    }
                }
            }

            return result;
        }

        public void ClearLog(DbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from dwh_log";
                command.ExecuteNonQuery();
            }
        }

        public void AddApiTable(DbConnection connection, string path)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into dwh_synchronize (path) values (@path)";
                AddParameter(command, "path", path);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveApiTable(DbConnection connection, string path)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from dwh_synchronize where path = @path";
                AddParameter(command, "path", path);
                command.ExecuteNonQuery();
            }
        }

        private class Logger
        {
            private readonly DbConnection _connection;
            private readonly EntitySchema _schema;
            private Stopwatch _stopwatch;
            private DateTime _start;
            private Exception _exception;
            private int? _records;

            public Logger(DbConnection connection, EntitySchema schema)
            {
                _connection = connection;
                _schema = schema;
            }

            public void Start()
            {
                _start = DateTime.Now;
                _stopwatch = Stopwatch.StartNew();
                _exception = null;
                _records = null;
            }

            public void SetException(Exception exception)
            {
                _exception = exception;
            }

            public void End()
            {
                var end = DateTime.Now;

                int elapsed = (int)_stopwatch.ElapsedMilliseconds;

                int? records = _records.HasValue ? _records : _exception == null ? (int?)0 : null;

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "insert into dwh_log (start, [end], duration, path, error, record_count) values (@start, @end, @duration, @path, @error, @records)";
                    AddParameter(command, "start", _start);
                    AddParameter(command, "end", end);
                    AddParameter(command, "duration", elapsed);
                    AddParameter(command, "path", _schema.Name);
                    AddParameter(command, "error", GetError());
                    AddParameter(command, "records", records);
                    command.ExecuteNonQuery();
                }
            }

            private string GetError()
            {
                if (_exception == null)
                    return null;

                var sb = new StringBuilder();

                var exception = _exception;

                while (true)
                {
                    sb.Append(exception.Message).Append(" (").Append(exception.GetType().FullName).AppendLine(")");

                    if (exception.StackTrace != null)
                        sb.AppendLine().AppendLine(exception.StackTrace.TrimEnd());

                    if (exception.InnerException == null)
                        break;

                    exception = exception.InnerException;

                    sb.AppendLine("== Caused by ==").AppendLine();
                }

                return sb.ToString();
            }

            public void HaveResults(int records)
            {
                _records = records;
            }
        }
    }
}
