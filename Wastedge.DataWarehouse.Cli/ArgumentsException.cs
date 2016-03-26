using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse.Cli
{
    internal class ArgumentsException : Exception
    {
        public ArgumentsException()
        {
        }

        public ArgumentsException(string message)
            : base(message)
        {
        }

        public ArgumentsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
