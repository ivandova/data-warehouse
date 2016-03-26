using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wastedge.DataWarehouse.Manager.Util
{
    internal class ComboBoxValue<T>
    {
        private readonly string _label;

        public T Value { get; }

        public ComboBoxValue(T value, string label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            _label = label;
            Value = value;
        }

        public override string ToString()
        {
            return _label;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return Value == null;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is T)
                return obj.Equals(Value);
            var other = obj as ComboBoxValue<T>;
            return other != null && Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }
    }
}
