using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    [Flags]
    public enum SynchronizeMode
    {
        Tracked = 1,
        Untracked = 2,
        All = Tracked | Untracked
    }
}
