using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;
using Wastedge.DataWarehouse.Provider.MSSql;
using WastedgeApi;

namespace Wastedge.DataWarehouse.Cli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);

                var connection = new DataWarehouseConnection(
                    arguments.Provider.Value,
                    arguments.ConnectionString,
                    new ApiCredentials(arguments.Url, arguments.Tenant, arguments.Username, arguments.Password)
                    );

                Console.WriteLine("Migrating database support tables");

                connection.MigrateSupportSchema();

                Console.WriteLine("Migrating synchronized schema");

                connection.MigrateSynchronizedSchema();

                Console.WriteLine("Synchronizing tables");

                connection.Synchronize(arguments.Mode);

                return 0;
            }
            catch (OptionException ex)
            {
                Console.WriteLine(ex.Message);

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown error: {ex.Message}");
                if (ex.StackTrace != null)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.StackTrace);
                }

                return 2;
            }
        }
    }
}
