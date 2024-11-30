using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public class SSBlockActionReturnValue(List<SSVarBase> variables) : SSBlock(variables)
    {
        public SSVarBase returnvar = new SSVarNmb("DefaultReturnValueForNewReturnAction", Guid.NewGuid());
        public override bool Execute(ExecutionData data)
        {
            if (Parent is not null)
                Parent.Done = true;
            else
                return false;
            data.ReturnValue = returnvar;
            return true;
        }
    }
}
