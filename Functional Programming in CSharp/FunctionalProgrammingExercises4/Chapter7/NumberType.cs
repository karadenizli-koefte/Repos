using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7
{
    public class NumberType : IEquatable<NumberType>
    {
        private string _value;
        public NumberType(string type)
        {
            this._value = type;
        }
        public static bool operator ==(NumberType code, string compare) => code._value.Equals(compare);
        public static bool operator !=(NumberType code, string compare) => !code._value.Equals(compare);
        public static bool operator ==(string code, NumberType compare) => code.Equals(compare._value);
        public static bool operator !=(string code, NumberType compare) => !code.Equals(compare._value);

        public override bool Equals(object obj)
        {
            return Equals(obj as NumberType);
        }

        public bool Equals(NumberType other)
        {
            return other != null &&
                   _value == other._value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }
    }
}
