using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaEditor.modding
{
    public abstract class EditorMod
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Version { get; }
        public abstract string Author { get; }
        /// <summary>
        /// Runs first, when editor core is loading the mod
        /// </summary>
        public abstract void OnLoad();
        /// <summary>
        /// Runs after EditorRegistry registers default types. Register your actions here.
        /// </summary>
        public abstract void OnRegister();
    }
}
