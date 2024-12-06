using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public class SSBlockActionReturnValue : SSBlock
    {
        string? ReturnValue { get; set; }

        public override string Title => $"Finish and return {ReturnValue??"nothing"}";

        public override bool Execute(ExecutionData data)
        {
            if (Parent is not null)
                Parent.Done = true;
            else
                return false;
            data.ReturnValue = ReturnValue;
            return true;
        }

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            if (args.Length > 0)
                ReturnValue = args[0];
        }
        public override string[] GetSerializedMetadata() => [ReturnValue ?? ""];
    }
}
