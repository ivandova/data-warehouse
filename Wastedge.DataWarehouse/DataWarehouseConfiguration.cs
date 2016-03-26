using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using WastedgeApi;

namespace Wastedge.DataWarehouse
{
    public class DataWarehouseConfiguration
    {
        private const string BaseKey = "Software\\Wastedge Data Warehouse";

        public int InstanceId { get; private set; }
        public int? TrackedTableInterval { get; set; }
        public int? UntrackedTableInterval { get; set; }
        public DataWarehouseProvider? Provider { get; set; }
        public string ConnectionString { get; set; }
        public string Url { get; set; }
        public string Tenant { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InstanceName => $"WEDW.{InstanceId}";

        public static List<DataWarehouseConfiguration> LoadAll(RegistryKey hive)
        {
            if (hive == null)
                throw new ArgumentNullException(nameof(hive));

            var result = new List<DataWarehouseConfiguration>();

            using (var key = hive.OpenSubKey(BaseKey))
            {
                if (key == null)
                    return result;

                foreach (string instanceName in key.GetSubKeyNames())
                {
                    int instanceId;
                    if (TryParseServiceName(instanceName, out instanceId))
                        result.Add(Load(hive, instanceId));
                }
            }

            return result;
        }

        public static DataWarehouseConfiguration Load(RegistryKey hive, int instanceId)
        {
            if (hive == null)
                throw new ArgumentNullException(nameof(hive));

            var configuration = new DataWarehouseConfiguration(instanceId);

            using (var key = hive.OpenSubKey(GetRegistryPath(instanceId)))
            {
                if (key == null)
                    return configuration;

                configuration.TrackedTableInterval = key.GetValue(nameof(configuration.TrackedTableInterval)) as int?;
                configuration.UntrackedTableInterval = key.GetValue(nameof(configuration.UntrackedTableInterval)) as int?;

                string providerName = key.GetValue(nameof(configuration.Provider)) as string;
                if (providerName != null)
                {
                    DataWarehouseProvider provider;
                    if (Enum.TryParse(providerName, true, out provider))
                        configuration.Provider = provider;
                }

                configuration.ConnectionString = key.GetValue(nameof(configuration.ConnectionString)) as string;
                configuration.Url = key.GetValue(nameof(configuration.Url)) as string;
                configuration.Tenant = key.GetValue(nameof(configuration.Tenant)) as string;
                configuration.Username = key.GetValue(nameof(configuration.Username)) as string;
                configuration.Password = key.GetValue(nameof(configuration.Password)) as string;
            }

            return configuration;
        }

        public DataWarehouseConfiguration()
            : this(0)
        {
        }

        private DataWarehouseConfiguration(int instanceId)
        {
            InstanceId = instanceId;
        }

        public void Validate()
        {
            if (!IsValid)
                throw new DataWarehouseException("One or more confirugation parameters are missing");
        }

        public bool IsValid =>
            TrackedTableInterval.HasValue &&
            UntrackedTableInterval.HasValue &&
            Provider.HasValue &&
            ConnectionString != null &&
            Url != null &&
            Tenant != null &&
            Username != null &&
            Password != null;

        public DataWarehouseConnection OpenConnection()
        {
            return new DataWarehouseConnection(
                Provider.Value,
                ConnectionString,
                new ApiCredentials(Url, Tenant, Username, Password)
            );
        }

        public void Save(RegistryKey hive)
        {
            if (hive == null)
                throw new ArgumentNullException(nameof(hive));

            if (InstanceId == 0)
            {
                int largest = 0;

                using (var key = hive.OpenSubKey(BaseKey))
                {
                    if (key != null)
                    {
                        foreach (string serviceName in key.GetSubKeyNames())
                        {
                            int instanceId;
                            if (TryParseServiceName(serviceName, out instanceId))
                                largest = Math.Max(largest, instanceId);
                        }
                    }
                }

                InstanceId = largest + 1;
            }

            using (var key = hive.CreateSubKey(GetRegistryPath(InstanceId)))
            {
                UpdateInteger(key, nameof(TrackedTableInterval), TrackedTableInterval);
                UpdateInteger(key, nameof(UntrackedTableInterval), UntrackedTableInterval);
                UpdateString(key, nameof(Provider), Provider?.ToString().ToLower());
                UpdateString(key, nameof(ConnectionString), ConnectionString);
                UpdateString(key, nameof(Url), Url);
                UpdateString(key, nameof(Tenant), Tenant);
                UpdateString(key, nameof(Username), Username);
                UpdateString(key, nameof(Password), Password);
            }
        }

        public void Delete(RegistryKey hive)
        {
            if (hive == null)
                throw new ArgumentNullException(nameof(hive));

            hive.DeleteSubKey(GetRegistryPath(InstanceId));
        }

        private void UpdateInteger(RegistryKey key, string name, int? value)
        {
            if (value.HasValue)
                key.SetValue(name, value.Value);
            else
                key.DeleteValue(name);
        }

        private void UpdateString(RegistryKey key, string name, string value)
        {
            if (value != null)
                key.SetValue(name, value);
            else
                key.DeleteValue(name);
        }

        private static string GetRegistryPath(int instanceId)
        {
            return BaseKey + $"\\WEDW.{instanceId}";
        }

        public static bool TryParseServiceName(string serviceName, out int instanceId)
        {
            if (serviceName == null)
                throw new ArgumentNullException(nameof(serviceName));

            instanceId = 0;

            if (!serviceName.StartsWith("WEDW."))
                return false;

            return int.TryParse(serviceName.Substring(5), out instanceId);
        }
    }
}
