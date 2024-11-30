using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public class SSVarBool(string name, Guid guid) : SSVarBase(name, guid)
    {
        public bool Value { get; set; } = true;

        public override string ValueAsString() => Value.ToString();
        public override string ToString() => $"\"{Name}\": ({GetType().Name[5..]}) {Value}";
    }
}
