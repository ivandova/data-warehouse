using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum self)
        {
            foreach (DescriptionAttribute attribute in self.GetType()
                .GetField(self.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), true)
            ) {
                return attribute.Description;
            }

            throw new InvalidOperationException("No description has been defined");
        }
    }
}
