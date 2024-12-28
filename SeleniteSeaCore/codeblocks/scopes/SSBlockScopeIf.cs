using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.scopes
{
    public sealed class SSBlockScopeIf : SSBlockScope
    {
        public SSValueComparerType? Comparer { get; set; }
        public List<SSValue> Values { get; set; } = [];
        public override string Title => $" Execute if {Comparer?.Text} for {string.Join(", ",Values)} evaluates to True";

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            if (args.Length > 0 && args[0] != "")
                Comparer = Comparers.ComparerRegistry[args[0]];
            if (Comparer is null)
                return;
            for (int i = 0; i < Comparer.ComparedValues; i++)
                Values.Add(new(args[1 + i]));
        }

        public override string[] GetSerializedMetadata()
        {
            List<string> a = [Comparer?.GetType().ToString() ?? ""];
            foreach (var s in Values)
                a.Add(s.Data);
            return [.. a];
        }
        public override bool Execute(ExecutionData data)
        {
            CompareResults? result;
            if ((result = Comparer?.Compare(data.RuntimeVariables, [.. Values])) == CompareResults.True)
                return base.Execute(data);
            else if(result == CompareResults.NaN || result == CompareResults.NeV)
            {
                Debug.Log(StatusCode.Error, $"If evaluation returned {result}", this);
                return false;
            }
            return true; //just that it finished
        }
    }
}
