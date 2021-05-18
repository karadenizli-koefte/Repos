using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7
{
    public class CountryCode : IEquatable<CountryCode>
    {
        private string _value;

        public CountryCode(string code)
        {
            this._value = code;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CountryCode);
        }

        public bool Equals(CountryCode other)
        {
            return other != null &&
                   _value == other._value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }

        public static bool operator ==(CountryCode code, CountryCode compare) => code.Equals(compare);
        public static bool operator !=(CountryCode code, CountryCode compare) => !code.Equals(compare);
        //public static bool operator ==(CountryCode code, string compare) => code._value.Equals(compare);
        //public static bool operator !=(CountryCode code, string compare) => !code._value.Equals(compare);
    }
}
