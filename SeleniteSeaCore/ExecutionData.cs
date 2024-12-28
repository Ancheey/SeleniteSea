using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore
{
    public class ExecutionData(string dir, Dictionary<string, Type> registeredTypes)
    {
        public string Directory { get; set; } = dir;
        public string? ReturnValue { get; set; }
        public Dictionary<string,SSValue> RuntimeVariables { get; set; } = [];
        public Dictionary<string, Type> RegisteredTypes { get; set; } = registeredTypes;
    }
}
