using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks
{
    public abstract class SSBlock(List<SSVarBase> variables)
    {
        public bool Done = false;
        public SSBlockScope? Parent { get; private set; } = null;
        private List<SSVarBase> InheritedVariables = new(variables);
        private readonly List<SSVarBase> PersonalVariables = [];
        public ImmutableList<SSVarBase> Variables => (new List<SSVarBase>(InheritedVariables).Concat(PersonalVariables)).ToImmutableList();
        public ImmutableList<SSVarBase> OnlyPersonalVariables => [.. PersonalVariables];

        public virtual void InheritVariables(IEnumerable<SSVarBase> variables) => InheritedVariables = new(variables);
        /// <summary>
        /// Provides a set of variable references that are meant to be edited when editing this Block
        /// string is for descriptions
        /// </summary>
        public virtual Dictionary<SSVarBase, string> VariablesForEdition() => [];
        public virtual void AddVariable(SSVarBase var)
        {
            if (Variables.Any(v => v.Guid == var.Guid || v.Name == v.Name))
                throw new Exception($"Unable to add a {var.Name} variable to block. Variable with that name or GUID already exists in this scope");
            PersonalVariables.Add(var);
        }
        public virtual void RemoveVariable(SSVarBase var)
        {
            if (!PersonalVariables.Remove(var))
                throw new Exception($"Couldn't remove variable {var} from block. Not found!");
        }
        public virtual void RemoveVariable(string name)
        {
            if (PersonalVariables.RemoveAll(k=>k.Name == name) == 0)
                throw new Exception($"Couldn't remove variable named {name} from block. Not found!");
        }
        public virtual void RemoveVariable(Guid guid)
        {
            if (PersonalVariables.RemoveAll(k => k.Guid == guid) == 0)
                throw new Exception($"Couldn't remove variable with guid: {guid} from block. Not found!");
        }
        /// <returns>True if it was executed propertly</returns>
        public abstract bool Execute(ExecutionData data);
    }
}
