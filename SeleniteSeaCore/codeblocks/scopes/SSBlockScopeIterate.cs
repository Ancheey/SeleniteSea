using SeleniteSeaCore.interfaces;
using SeleniteSeaCore.variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.scopes
{
    public class SSBlockScopeIterate : SSBlockScope, IContinueable, IBreakable
    {
        public SSValue StartingPoint = new("1");
        public SSValue EndingPoint = new("10");
        public SSValue Jump = new("1");
        public SSValue VariableName = new("i");
        private bool _continueFlag = false;
        private bool _breakFlag = false;
        public override string Title => $"Iterate from {StartingPoint.Data} to {EndingPoint.Data} by {Jump.Data} as {VariableName.Data}";

        public override void DeserializeAndApplyMetadata(params string[] args)
        {
            StartingPoint = new(args[0]);
            EndingPoint = new(args[1]);
            Jump = new(args[2]);
            VariableName = new(args[3]);
        }

        public override string[] GetSerializedMetadata() => [StartingPoint.Data,EndingPoint.Data,Jump.Data,VariableName.Data];
        public override bool Execute(ExecutionData data)
        {

            _continueFlag = false;
            _breakFlag = false;
            try
            {
                if (!StartingPoint.TryParseNumber(data.RuntimeVariables, out var A))
                    throw new InvalidCastException($"Starting point for an iterator is not a number. ({StartingPoint.Data}) => ({StartingPoint.GetInterpolatedValue(data.RuntimeVariables)})");
                if (!EndingPoint.TryParseNumber(data.RuntimeVariables, out var B))
                    throw new InvalidCastException($"Ending point for an iterator is not a number. ({EndingPoint.Data}) => ({EndingPoint.GetInterpolatedValue(data.RuntimeVariables)})");
                if (!Jump.TryParseNumber(data.RuntimeVariables, out var j))
                    throw new InvalidCastException($"Jump for an iterator is not a number. ({Jump.Data}) => ({Jump.GetInterpolatedValue(data.RuntimeVariables)})");

                string varname = VariableName.GetInterpolatedValue(data.RuntimeVariables);
                SSValue? var = null;
                if(varname != "" && !data.RuntimeVariables.ContainsKey(varname))
                    var = data.RuntimeVariables[varname];

                for (double i = A; !Done && !_breakFlag && A <= B; A += j)
                {
                    if (var is not null)
                        var.Data = i.ToString();
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
        public void Continue() => _continueFlag = true;
        public void Break() => _breakFlag = true;
    }
}
