using SeleniteSeaCore.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public sealed class SSBlockActionContinue : SSBlockActionBasic
    {
        public override string Title => "Continue to the next iteration";

        public override void DeserializeAndApplyMetadata(params string[] args){}

        public override bool Execute(ExecutionData data)
        {
            SSBlockScope parent = Parent ?? throw new Exception("Continue action found not in a scope");
            while (parent != null)
            {
                if (parent is IContinueable b)
                {
                    b.Continue();
                    return true;
                }
                parent = parent.Parent ?? throw new Exception("Continue action not in a continueable loop");
            }
            return false;
        }

        public override string[] GetSerializedMetadata() => [];
    }
}
