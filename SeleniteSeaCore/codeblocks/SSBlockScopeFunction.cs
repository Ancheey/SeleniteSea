using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks
{
    public sealed class SSBlockScopeFunction : SSBlockScope
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string Title => Name + $"({string.Join(", ",RequestedVariables.Keys)})";

        /// <summary>
        /// {Name, (Default_Value, description)} pair for requested variables for function execution
        /// </summary>
        public Dictionary<string,(string description, string defaultValue)> RequestedVariables = [];
        public SSBlockScopeFunction() : base()
        {
            Name = "New Function";
            Description = "";
        }

        public override string[] GetSerializedMetadata()
        {
            List<string> data = [Name, Description];
            foreach (var item in RequestedVariables)
            {
                data.Add($"{item.Key}");
                data.Add($"{item.Value.description}");
                data.Add($"{item.Value.defaultValue}");
            }
            return [.. data];
        }


        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            //Name
            //Description
            //Var1 Name
            //Var1 description
            //Var1 defaultvalue
            //Var2 Name
            //...

            if(args.Length > 0)
                Description = args[0];
            if (args.Length > 1)
                Description = args[1];
            for(int i = 2; args.Length >= i+2; i += 3)
                RequestedVariables.Add(args[i], (args[i + 1], args[i + 2]));
        }
    }
}
