using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public class SSVarString(string name, Guid guid) : SSVarBase(name, guid)
    {
        string Value { get; set; } = "";

        public string Interpolated(params SSVarBase[] values)
        {
            
            string interpolatedvalue = Value;
            foreach (var v in values)
            {
                if (v == this)
                    continue;//Can't interpolate with self
                interpolatedvalue = interpolatedvalue.Replace($"{{{v.Name}}}", v.ValueAsString());     
            }
            return interpolatedvalue;
        }

        public override string ValueAsString() => Value;
        public override string ToString() => $"\"{Name}\": ({GetType().Name[5..]}) {Value}";
    }
}
