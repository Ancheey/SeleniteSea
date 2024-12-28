using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public sealed class SSBlockActionReturnValue : SSBlockActionBasic
    {
        public SSBlockActionReturnValue()
        {
            PublicValues.Add("Return Value", new(""));
        }

        public override string Title => $"Finish and return \"{PublicValues["Return Value"]?.Data}\"";

        public override bool Execute(ExecutionData data)
        {
            Done = true;
            data.ReturnValue = PublicValues["Return Value"].GetInterpolatedValue(data.RuntimeVariables);
            return true;
        }

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            if (args.Length > 0)
                PublicValues["Return Value"] = new(args[0]);
        }
        public override string[] GetSerializedMetadata() => [PublicValues["Return Value"].Data];
    }
}
