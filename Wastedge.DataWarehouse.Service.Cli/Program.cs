using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse.Service.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            LogUtils.SetupConsoleLogging();

            using (new Daemon(args[0], true))
            {
                Console.WriteLine("Press enter to stop the daemon...");
                Console.ReadKey();
            }
        }
    }
}
