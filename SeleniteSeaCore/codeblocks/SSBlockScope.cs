using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks
{
    public abstract class SSBlockScope(List<SSVarBase> variables) : SSBlock(variables)
    {

        private List<SSBlock> children = [];
        public ImmutableList<SSBlock> Children => [.. children];

        public override void InheritVariables(IEnumerable<SSVarBase> variables)
        {
            base.InheritVariables(variables);
            AdjustChildrenInheritance();
        }
        public void AdjustChildrenInheritance()
        {
            foreach (var item in children)
            {
                item.InheritVariables(Variables);
            }
        }
        public void AddChild(SSBlock child, int index = -1) //adjust inherited variables
        {
            if (children.Contains(child))
                throw new Exception("Child cannot be added. It already is inside this scope");
            if(index == -1)
                children.Add(child);
            else
                children.Insert(index,child);
            child.InheritVariables(Variables);
        }
        public override void AddVariable(SSVarBase var)
        {
            try
            {
                base.AddVariable(var);
                AdjustChildrenInheritance();
            }
            catch (Exception ex) 
            {
                Debug.Log(StatusCode.Error, ex.Message, this);
                /*Add console info after adding manager class with console delegates*/ 
            }
        }
        public override void RemoveVariable(SSVarBase var)
        {
            base.RemoveVariable(var); 
            AdjustChildrenInheritance();//this can be optimized to just remove the unwanted kids and not reassign an entire new list but oh well
        }
        public override void RemoveVariable(Guid guid)
        {
            base.RemoveVariable(guid);
            AdjustChildrenInheritance();
        }
        public override void RemoveVariable(string name)
        {
            base.RemoveVariable(name);
            AdjustChildrenInheritance();
        }
        /// <summary>
        /// If you're overwritting this in scopes, make sure to stop execution if "Done" is set to true
        /// </summary>
        public override bool Execute(ExecutionData data)
        {
            try
            {
                foreach (var item in children)
                {
                    if (Done)
                    {
                        if(Parent is not null)
                            Parent.Done = true;
                        return true;
                    }
                    if (!item.Execute(data))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(StatusCode.Error,ex.Message, this);
                Done = true;
                return false;
            }
        }
    }
}
