using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;

namespace Wastedge.DataWarehouse.Cli
{
    internal class Arguments
    {
        public DataWarehouseProvider? Provider { get; private set; }
        public string ConnectionString { get; private set; }
        public string Url { get; private set; }
        public string Tenant { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public SynchronizeMode Mode { get; private set; }

        public Arguments(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            var options = new OptionSet
            {
                { "p|provider=", "Database provider", p => Provider = ParseProvider(p) },
                { "c|connectionstring=", "Connection string", p => ConnectionString = p },
                { "u|url=", "URL to Wastedge", p => Url = p },
                { "t|tenant=", "Tenant ID", p => Tenant = p },
                { "n|username=", "Username", p => Username = p },
                { "w|password=", "Password", p => Password = p },
                { "tracked", "Synchronize tracked", p => Mode |= SynchronizeMode.Tracked },
                { "untracked", "Synchronize untracked", p => Mode |= SynchronizeMode.Untracked }
            };

            var extras = options.Parse(args);

            if (extras.Count > 0)
                throw new ArgumentsException("Cannot parse arguments");

            if (Provider == null)
                throw new ArgumentsException("Provider is mandatory");
            if (ConnectionString == null)
                throw new ArgumentsException("Connection string is mandatory");
            if (Url == null)
                throw new ArgumentsException("Url is mandatory");
            if (Tenant == null)
                throw new ArgumentsException("Tenant is mandatory");
            if (Username == null)
                throw new ArgumentsException("Username is mandatory");
            if (Password == null)
                throw new ArgumentsException("Password is mandatory");

            if (Mode == 0)
                Mode = SynchronizeMode.All;
        }

        private DataWarehouseProvider ParseProvider(string name)
        {
            DataWarehouseProvider provider;
            if (Enum.TryParse(name, true, out provider))
                return provider;

            throw new ArgumentsException("Invalid value for provider");
        }
    }
}
