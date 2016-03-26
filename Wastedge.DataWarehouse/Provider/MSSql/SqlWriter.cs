using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse.Provider.MSSql
{
    internal class SqlWriter
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public SqlWriter Write(string value)
        {
            _sb.Append(value);
            return this;
        }

        public SqlWriter WriteLine(string value)
        {
            return Write(value).Write(Environment.NewLine);
        }

        public SqlWriter WriteName(string name)
        {
            _sb.Append('[').Append(name).Append(']');
            return this;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}
