using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public sealed class SSBlockActionWait : SSBlockActionBasic
    {
        public SSBlockActionWait() 
        {
            PublicValues.Add("Miliseconds", new variables.SSValue("0"));
        }
        public override string Title => $"Await for {PublicValues["Miliseconds"].Data}ms";

        

        public override bool Execute(ExecutionData data)
        {
            if (PublicValues["Miliseconds"]?.TryParseNumber(data.RuntimeVariables, out double value) == true) {
                
                double jumps = 0;
                while (!Done && jumps * 100 < value)
                {
                    Thread.Sleep(100);
                    jumps++;
                }
                return true;
             }
            else
                throw new InvalidDataException($"Cannot convert {PublicValues["Miliseconds"]?.Data} to a number");
        }

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            if (args.Length > 0)
                PublicValues["Miliseconds"].Data = args[0];
        }

        public override string[] GetSerializedMetadata() => [PublicValues["Miliseconds"]?.Data??"0"];
    }
}
