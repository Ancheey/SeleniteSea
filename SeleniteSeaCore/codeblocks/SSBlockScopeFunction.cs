using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks
{
    public class SSBlockScopeFunction : SSBlockScope
    {
        public string Name { get; set; }
        public SSBlockScopeFunction(List<SSVarBase> variables) : base(variables)
        {
            Name = "Unnamed";
        }
        public SSBlockScopeFunction(string name, SSVarBase ReturnVar,  List<SSVarBase> variables) : base(variables)
        {
            Name = name;
        }
        public override Dictionary<SSVarBase, string> VariablesForEdition() =>[];
    }
}
