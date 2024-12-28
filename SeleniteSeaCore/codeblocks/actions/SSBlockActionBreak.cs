
using SeleniteSeaCore.interfaces;

namespace SeleniteSeaCore.codeblocks.actions
{
    public sealed class SSBlockActionBreak : SSBlockActionBasic
    {
        public override string Title => "Break out of a loop";

        public override void DeserializeAndApplyMetadata(params string[] args){}

        public override bool Execute(ExecutionData data)
        {
            SSBlockScope parent = Parent??throw new Exception("Break point found not in a scope");
            while (parent != null)
            {
                if (parent is IBreakable b)
                {
                    b.Break();
                    return true;
                }
                parent = parent.Parent ?? throw new Exception("Break point not in a breakable loop");
            }
            return false;
        }

        public override string[] GetSerializedMetadata() => [];
    }
}
