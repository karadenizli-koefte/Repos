using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SqlTemplate
    {
        string Value { get; }
        public SqlTemplate(string value) { Value = value; }

        public static implicit operator string(SqlTemplate c)
           => c.Value;
        public static implicit operator SqlTemplate(string s)
           => new SqlTemplate(s);

        public override string ToString() => Value;
    }
}
