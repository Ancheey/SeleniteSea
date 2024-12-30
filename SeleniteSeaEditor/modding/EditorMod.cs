using SeleniteSeaExecutor.modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaEditor.modding
{
    public abstract class EditorMod : SeaMod
    {
        /// <summary>
        /// Register your editor actions here
        /// EditorRegistry.RegisterAction<Type>(data)
        /// </summary>
        public abstract void OnRegisterActions();
    }
}
