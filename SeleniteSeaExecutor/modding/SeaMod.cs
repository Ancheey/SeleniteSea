using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaExecutor.modding
{
    public abstract class SeaMod
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Version { get; }
        public abstract string Author { get; }
        /// <summary>
        /// Runs first, when editor core is loading the mod
        /// </summary>
        public virtual void OnLoad() { }
        public virtual void OnUnload() { }
        /// <summary>
        /// Register your types here for an Executor
        /// TypeRegistry for Executor only
        /// Won't be executed on the Editor Side
        /// </summary>
        public abstract void OnRegisterExecutor();
        public virtual void BeforeExecution() { }
        public virtual void AfterExecution() { }
    }
}
