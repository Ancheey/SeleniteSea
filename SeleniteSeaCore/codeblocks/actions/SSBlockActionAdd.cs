using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public class SSBlockActionAdd : SSBlockActionBasic
    {
        public SSBlockActionAdd()
        {
            PublicValues.Add("Target",new(""));
            PublicValues.Add("Value", new(""));
        }
        public override string Title => $"Add {PublicValues["Value"]?.Data} to {PublicValues["Target"]?.Data}";

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            PublicValues["Target"].Data = args[0];
            PublicValues["Value"].Data = args[1];
        }

        public override bool Execute(ExecutionData data)
        {
            if(PublicValues["Target"] is null || PublicValues["Value"] is null)
                return false;

            var interpolatedTarget = PublicValues["Target"].GetInterpolatedValue(data.RuntimeVariables); //replace any @references@ for usage

            if (!data.RuntimeVariables.ContainsKey(interpolatedTarget))
            {
                data.RuntimeVariables.Add(interpolatedTarget, new(PublicValues["Value"].Data));
            }
            else
            {
                var variable = data.RuntimeVariables[PublicValues["Target"].Data];
                if (variable.TryParseNumber(data.RuntimeVariables,out var tar) && PublicValues["Value"].TryParseNumber(data.RuntimeVariables,out var val))
                {
                    variable.Data = (tar+val).ToString();
                }
                else
                {
                    variable.Data += PublicValues["Value"];
                }
            }
            return true;
        }

        public override string[] GetSerializedMetadata() => [PublicValues["Target"]?.Data??"", PublicValues["Value"]?.Data??""];
    }
}
