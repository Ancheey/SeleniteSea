using SeleniteSeaCore;
using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaExecutor
{
    public static class SSProcess
    {
        public static ExecutionData Execute(SSBlockScope scope)
        {
            var data = new ExecutionData();
            scope.Execute(data);
            return data;
        }
    }
}
