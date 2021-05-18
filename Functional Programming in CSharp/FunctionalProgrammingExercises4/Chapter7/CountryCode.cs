using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7
{
    public class CountryCode
    {
        private string Value;

        public CountryCode(string code)
        {
            this.Value = code;
        }

        // Equality operators
        public static bool operator ==(CountryCode code, CountryCode compare) => code.Value == compare.Value;
        public static bool operator !=(CountryCode code, CountryCode compare) => code.Value != compare.Value;

        public static bool operator ==(CountryCode code, string compare) => code.Value == compare;
        public static bool operator !=(CountryCode code, string compare) => code.Value != compare;

        // Conversion methods
        public static implicit operator string(CountryCode c) => c.Value;
        public static implicit operator CountryCode(string s) => new(s);

        public override string ToString() => Value;
    }
}
