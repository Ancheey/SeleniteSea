using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public sealed class SSBlockActionWrite : SSBlockActionBasic
    {
        public SSBlockActionWrite()
        {
            PublicValues.Add("Text", new(""));
        }
        public override string Title => $"Write out: \"{PublicValues["Text"].Data}\"";

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            PublicValues["Text"].Data = args[0];
        }

        public override bool Execute(ExecutionData data)
        {
            var inttext = PublicValues["Text"].GetInterpolatedValue(data.RuntimeVariables);
            Debug.Log(StatusCode.Info, inttext ,null);
            return true;
        }

        public override string[] GetSerializedMetadata() => [PublicValues["Text"].Data];
    }
}
