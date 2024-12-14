using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks
{
    public abstract class SSBlockScope : SSBlock
    {

        private List<SSBlock> children = [];
        public ImmutableList<SSBlock> Children => [.. children];

        public void AddChild(SSBlock child, int index = -1) //adjust inherited variables
        {
            if (children.Contains(child))
                throw new Exception("Child cannot be added. It already is inside this scope");
            if(index == -1)
                children.Add(child);
            else
                children.Insert(index,child);
            child.Parent = this;
        }
        public bool RemoveChild(SSBlock child) => children.Remove(child);
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
