using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore
{
    public class ExecutionData
    {
        public Type? ReturnType => ReturnValue?.GetType();
        public SSVarBase? ReturnValue;
    }
}
