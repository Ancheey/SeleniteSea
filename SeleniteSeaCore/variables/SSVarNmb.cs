using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public class SSVarNmb(string name, Guid guid) : SSVarBase(name, guid)
    {
        public double Value { get; set; } = 0;

        public double WithPrecission(int precission) => Math.Round(Value,precission);
        public override string ValueAsString() => Value.ToString();
        public override string ToString() => $"\"{Name}\": ({GetType().Name[5..]}) {Value}";
    }
}
