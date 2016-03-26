using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    internal class ApiTable
    {
        public string Path { get; }
        public DateTime? LastUpdate { get; }

        public ApiTable(string path, DateTime? lastUpdate)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Path = path;
            LastUpdate = lastUpdate;
        }
    }
}
