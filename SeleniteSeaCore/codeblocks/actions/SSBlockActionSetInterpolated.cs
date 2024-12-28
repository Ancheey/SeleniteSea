using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public sealed class SSBlockActionSetInterpolated : SSBlockActionBasic
    {
        public SSBlockActionSetInterpolated()
        {
            PublicValues.Add("Variable Name", new("New Variable"));
            PublicValues.Add("Value", new(""));
        }

        public override string Title => $"Set value of {PublicValues["Variable Name"]} to an interpolated value of {PublicValues["Value"]}";


        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            PublicValues["Variable Name"].Data = args[0];
            PublicValues["Value"].Data = args[1];
        }

        public override bool Execute(ExecutionData data)
        {
            var varname = PublicValues["Variable Name"].GetInterpolatedValue(data.RuntimeVariables);
            if (data.RuntimeVariables.TryGetValue(varname, out variables.SSValue? value))
                value.Data = PublicValues["Value"].GetInterpolatedValue(data.RuntimeVariables);
            data.RuntimeVariables.Add(varname, new(PublicValues["Value"].GetInterpolatedValue(data.RuntimeVariables)));
            return true;
        }

        public override string[] GetSerializedMetadata() => [PublicValues["Variable Name"].Data, PublicValues["Value"].Data];
    }
}
