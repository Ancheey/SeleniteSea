using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public class SSVarBoolStatement(string name, Guid guid) : SSVarBase(name, guid)
    {
        public bool Value
        {
            get {
                switch (Comparer)
                {
                    case SSBoolComparer.Is:
                        if (Item1 is null && Item2 is null)
                            return true;
                        else if (Item1 is null || Item2 is null)
                            return false;
                        else return Item1.Value == Item2.Value;
                    case SSBoolComparer.IsNot:
                        if (Item1 is null && Item2 is null)
                            return false;
                        else if (Item1 is null || Item2 is null)
                            return true;
                        else return Item1.Value != Item2.Value;
                    default:
                        return false;
                }
            }
        }
        public SSVarBool? Item1 { get; set; }
        public SSVarBool? Item2 { get; set; }
        public SSBoolComparer Comparer { get; set; }

        public override string ValueAsString() => Value.ToString();
        public override string ToString() => $"\"{Name}\": ({GetType().Name[5..]}) {Value}?";
    }
    public enum SSBoolComparer
    {
        Is,
        IsNot
    }
}
