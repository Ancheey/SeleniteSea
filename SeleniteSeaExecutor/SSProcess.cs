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
        public static async Task<ExecutionData> Execute(SSBlock scope, Dictionary<string,Type> RegisteredTypes, string WorkingDirectory)
        {
            var data = new ExecutionData(WorkingDirectory, RegisteredTypes);
            scope.Done = false;

            await Task.Run(() =>
            {
                lock (data)
                {
                    try
                    {
                        scope.Execute(data);
                    }
                    catch(Exception ex)
                    {
                        Debug.Log(StatusCode.Error, "Critical error! " + ex,null);
                    }
                }
            });
            return data;
        }
    }
}
