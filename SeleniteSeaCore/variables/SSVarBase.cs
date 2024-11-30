using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public abstract class SSVarBase(string name, Guid guid)
    {
        public Guid Guid { get; } = guid; public string Name { get; } = name;

        public abstract string ValueAsString();
        public override string ToString() => $"\"{Name}\": {GetType().Name[5..]}";
    }
    public enum VariableReturnTypes
    {
        Any,
        Number,
        Bool,
        String,
        Object
    }
}
