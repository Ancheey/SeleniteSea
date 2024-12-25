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
        public static async Task<ExecutionData> Execute(SSBlock scope, ExecutionData data)
        {
            scope.Done = false;
            await Task.Run(() => scope.Execute(data));
            return data;
        }
    }
}
