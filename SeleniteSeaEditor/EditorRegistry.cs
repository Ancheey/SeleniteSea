using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.variables;
using SeleniteSeaEditor.controls.Displays;
using SeleniteSeaEditor.controls.Editors;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaEditor
{
    public record EditorRegistryActionItem(string Name, string Description, Type Display, Type? Editor, bool Editable = true, bool Deletable = true, bool Moveable = true, bool Createable = true);
    public static class EditorRegistry
    {
        private static readonly Dictionary<Type, EditorRegistryActionItem> ActionData = [];
        public static ImmutableDictionary<Type, EditorRegistryActionItem> Actions => ActionData.ToImmutableDictionary();
        private readonly static Dictionary<string, Type> _registeredTypes = [];
        public static ImmutableDictionary<string, Type> RegisteredTypes => _registeredTypes.ToImmutableDictionary();

        public static void RegisterAction(Type action, EditorRegistryActionItem data)
        {
            ActionData.Add(action, data);
            _registeredTypes.Add(action.ToString(), action);
        }

        static EditorRegistry() 
        {

            RegisterAction(typeof(SSBlockScopeFunction),
                new EditorRegistryActionItem("Function", "A scope that can return a value", typeof(DisplaySSBlockScopeFunction), typeof(EditorSSBlockScopeFunction),
                Editable: true,
                Createable: false,
                Moveable: false,
                Deletable: false));
        }
    }

}
