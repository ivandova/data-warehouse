using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse.Service
{
    internal static class Program
    {
        public static void Main()
        {
            ServiceBase.Run(new ServiceBase[]
            {
                new WindowsService()
            });
        }
    }
}
