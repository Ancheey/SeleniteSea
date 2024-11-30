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
    public record EditorRegistryVariableItem(string Name, Type plate, VariableReturnTypes ReturnTypes);
    public static class EditorRegistry
    {
        private static readonly Dictionary<Type, EditorRegistryActionItem> ActionData = [];
        public static ImmutableDictionary<Type, EditorRegistryActionItem> Actions => ActionData.ToImmutableDictionary();

        private static readonly Dictionary<Type, EditorRegistryVariableItem> VariableData = [];
        public static ImmutableDictionary<Type, EditorRegistryVariableItem> Variables => VariableData.ToImmutableDictionary();

        public static void RegisterAction(Type action, EditorRegistryActionItem data)
        {
            ActionData.Add(action, data);
        }
        public static void RegisterVariable(string name,Type var, Type plate, VariableReturnTypes returntype)
        {
            if (VariableData.Any(kvp => kvp.Key == var || kvp.Value.Name == name))
                throw new ArgumentException($"Variable named {name} is already registered for {var}");
            VariableData.Add(var, new EditorRegistryVariableItem(name, plate, returntype));
        }

        static EditorRegistry() 
        {
            //ReturnTypes.Add(typeof(SSVarBool), new("Bool",,VariableReturnTypes.Bool));
            //ReturnTypes.Add(typeof(SSVarBoolStatement), VariableReturnTypes.Bool);
            //ReturnTypes.Add(typeof(SSVarNmb), VariableReturnTypes.Number);
            //ReturnTypes.Add(typeof(SSVarString), VariableReturnTypes.String);

            RegisterAction(typeof(SSBlockScopeFunction),
                new EditorRegistryActionItem("Function", "A scope that can return a value", typeof(DisplaySSBlockScopeFunction), typeof(EditorSSBlockScopeFunction),
                Editable: false,
                Createable: false,
                Moveable: false,
                Deletable: false));
        }
    }

}
