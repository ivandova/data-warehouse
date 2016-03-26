using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    internal class ApiTableCollection : KeyedCollection<string, ApiTable>
    {
        protected override string GetKeyForItem(ApiTable item)
        {
            return item.Path;
        }
    }
}
