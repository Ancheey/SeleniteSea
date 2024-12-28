using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public class SSBlockActionExecuteFunction : SSBlock
    {
        public string Function { get; set; } = "";
        public SSValue TargetVariable { get; set; } = new("");
        public Dictionary<string, SSValue> PassedValues = [];
        public override string Title
        {
            get
            {
                List<string> args = [];
                foreach (var arg in PassedValues)
                    args.Add($"{arg.Key}: {arg.Value.Data}");
                if(TargetVariable.Data == "") 
                    return $"Run {Function}({string.Join(", ",args)})";
                return $"Run {Function}({string.Join(", ", args)}) and save value as {TargetVariable.Data}";
            }
        }

        public override void DeserializeAndApplyMetadata(params string[] args)
        {

            Function = args[0];
            TargetVariable.Data = args[1];
            for (int i = 2; args.Length >= i + 1; i += 2)
                PassedValues.Add(args[i], new(args[i + 1]));
        }

        public override bool Execute(ExecutionData data)
        {
            //deserialize block
            SSBlock? block = SerializationEngine.Deserialize(data.RegisteredTypes, @$"{data.Directory}\{Function}.seascript") 
                ?? throw new InvalidDataException("Deserialization during a call returned null");
            //build execution data
            ExecutionData ed = new(data.Directory, data.RegisteredTypes);
            foreach (var arg in PassedValues)
                ed.RuntimeVariables.Add(arg.Key, new(arg.Value.GetInterpolatedValue(data.RuntimeVariables)));
            //call function
            block.Execute(ed);

            if(TargetVariable.Data != "" && ed.ReturnValue != null)//If there's a target var && a return value
            {
                if (data.RuntimeVariables.TryGetValue(TargetVariable.GetInterpolatedValue(data.RuntimeVariables), out var val))
                    val.Data = ed.ReturnValue; //Change var value
                else
                    data.RuntimeVariables.Add(TargetVariable.GetInterpolatedValue(data.RuntimeVariables), new(ed.ReturnValue));//add a new var
            }

            return true;
        }

        public override string[] GetSerializedMetadata()
        {
            List<string> args = [Function, TargetVariable.Data];
            foreach (var arg in PassedValues)
            {
                args.Add(arg.Key);
                args.Add(arg.Value.Data);
            }
            return args.ToArray();
        }
    }
}
