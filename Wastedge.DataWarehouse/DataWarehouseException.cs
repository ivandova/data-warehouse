using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    public class DataWarehouseException : Exception
    {
        public DataWarehouseException()
        {
        }

        public DataWarehouseException(string message)
            : base(message)
        {
        }

        public DataWarehouseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
