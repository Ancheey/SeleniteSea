using SeleniteSeaCore.interfaces;
using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.scopes
{
    public class SSBlockScopeWhile : SSBlockScope, IBreakable, IContinueable
    {
        public SSValueComparerType? Comparer { get; set; }
        public List<SSValue> Values { get; set; } = [];
        public override string Title => $"Loop while {Comparer?.Text} for {string.Join(", ",Values)}";
        private bool _continueFlag = false;
        private bool _breakFlag = false;
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
            _continueFlag = false;
            _breakFlag = false;
            try
            {
                CompareResults? result = CompareResults.NeV;
                while (!Done && !_breakFlag && (result = Comparer?.Compare(data.RuntimeVariables, [.. Values])) == CompareResults.True)
                {
                    _continueFlag = false;
                    foreach (var item in Children)
                    {
                        if (!item.Execute(data))
                            return false;
                        if (item.Done)
                            Done = true;
                        if (_continueFlag || _breakFlag || Done)
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(StatusCode.Error, ex.Message, this);
                Done = true;
                return false;
            }
        }
        /// <summary>
        /// Skips the iteration and does another loop
        /// </summary>
        public void Continue() => _continueFlag = true;
        public void Break() => _breakFlag = true;
    }
}
