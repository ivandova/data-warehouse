using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    internal static class DbDataReaderExtensions
    {
        public static DateTime? GetDateTimeOpt(this DbDataReader self, int ordinal)
        {
            if (self.IsDBNull(ordinal))
                return null;
            return self.GetDateTime(ordinal);
        }
    }
}
