using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.codeblocks.actions;
using SeleniteSeaCore.codeblocks.scopes;
using SeleniteSeaEditor.controls.Displays;
using SeleniteSeaEditor.controls.Editors;
using SeleniteSeaEditor.modding;
using System.Collections.Immutable;

namespace SeleniteSeaEditor
{
    public record EditorRegistryActionItem(string Name, string Description, Type Display, Type? Editor, bool Editable = true, bool Deletable = true, bool Moveable = true, bool Createable = true);
    public static class EditorRegistry
    {
        private static readonly Dictionary<Type, EditorRegistryActionItem> ActionData = [];
        public static ImmutableDictionary<Type, EditorRegistryActionItem> Actions => ActionData.ToImmutableDictionary();
        private readonly static Dictionary<string, Type> _registeredTypes = [];
        public static ImmutableDictionary<string, Type> RegisteredTypes => _registeredTypes.ToImmutableDictionary();

        public static void RegisterAction<T>(EditorRegistryActionItem data) where T : SSBlock
        {
            ActionData.Add(typeof(T), data);
            _registeredTypes.Add(typeof(T).ToString(), typeof(T));
        }
        static EditorRegistry() 
        {

            RegisterAction<SSBlockScopeFunction>(new EditorRegistryActionItem("Function", "A scope that can return a value", typeof(DisplaySSBlockScopeFunction), typeof(EditorSSBlockScopeFunction),
                Editable: true,
                Createable: false,
                Moveable: false,
                Deletable: false));
            RegisterAction<SSBlockScopeWhile>(new EditorRegistryActionItem("While", "A scope that loops while set comparison is true", typeof(DisplaySSBlockScope), typeof(EditorSSBlockScopeWhile),
                Editable: true,
                Createable: true,
                Moveable: false,
                Deletable: true));
            RegisterAction<SSBlockScopeIterate>(new EditorRegistryActionItem("Iterator", "A scope that loops from point A till point B, where both are numbers", typeof(DisplaySSBlockScope), typeof(EditorSSBlockScopeIterator),
                Editable: true,
                Createable: true,
                Moveable: false,
                Deletable: true));
            RegisterAction<SSBlockScopeIf>(new EditorRegistryActionItem("If", "A scope that executes if a set comparison is true", typeof(DisplaySSBlockScope), typeof(EditorSSBlockScopeIf),
                Editable: true,
                Createable: true,
                Moveable: false,
                Deletable: true));
            RegisterAction<SSBlockActionReturnValue>(new("Return", "Ends execution and can return a value",typeof(DisplaySSBlock),typeof(EditorSSBlockActionBasic)));
            RegisterAction<SSBlockActionWait>(new("Wait", "Waits for a set amount of ms", typeof(DisplaySSBlock), typeof(EditorSSBlockActionBasic)));
            RegisterAction<SSBlockActionAdd>(new("Add", "Adds numbers and concatenates strings", typeof(DisplaySSBlock), typeof(EditorSSBlockActionBasic)));
            RegisterAction<SSBlockActionWrite> (new("Write", "Write out an interpolated text", typeof(DisplaySSBlock), typeof(EditorSSBlockActionBasic)));
            RegisterAction<SSBlockActionSet> (new("Set", "Set a variable to a value", typeof(DisplaySSBlock), typeof(EditorSSBlockActionBasic)));
            RegisterAction<SSBlockActionSetInterpolated> (new("InSet", "Set a variable to an interpolated value (removes references)", typeof(DisplaySSBlock), typeof(EditorSSBlockActionBasic)));
            RegisterAction<SSBlockActionBreak> (new("Break", "Break out of a loop", typeof(DisplaySSBlock), null, Editable: false));
            RegisterAction<SSBlockActionContinue> (new("Continue", "Continue loop to next iteration", typeof(DisplaySSBlock), null, Editable: false));
            RegisterAction<SSBlockActionExecuteFunction> (new("Call Function", "Calls a designated function and saves the return value", typeof(DisplaySSBlock), typeof(EditorSSBlockActionExecuteFunction)));


            foreach(var mod in ModHandler.LoadedMods)
            {
                mod.OnRegisterActions();
            }
        }
    }

}
