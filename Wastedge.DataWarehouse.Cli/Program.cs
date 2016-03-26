using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wastedge.DataWarehouse.Provider.MSSql;
using WastedgeApi;

namespace Wastedge.DataWarehouse.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = (DataWarehouseProvider)Enum.Parse(typeof(DataWarehouseProvider), args[0], true);

            var connection = new DataWarehouseConnection(
                provider,
                args[1],
                new ApiCredentials(args[2], args[3], args[4], args[5])
            );

            Console.WriteLine("Migrating database support tables");

            connection.MigrateSupportSchema();

            Console.WriteLine("Migrating synchronized schema");

            connection.MigrateSynchronizedSchema();

            Console.WriteLine("Synchronizing tables");

            connection.Synchronize();
        }
    }
}
