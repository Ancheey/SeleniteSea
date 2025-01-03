using SeleniteSeaCore;
using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.codeblocks.actions;
using SeleniteSeaCore.variables;
using SeleniteSeaEditor.controls;
using SeleniteSeaEditor.controls.Displays;
using SeleniteSeaEditor.modding;
using SeleniteSeaExecutor;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace SeleniteSeaEditor
{
    public static class EditorCore
    {
        
        public static SSBlock? CurrentBlock = null;

        //Load functions to it and save last state of the display so we don't have to rebuild it each time we open the script
        public static Dictionary<SSBlock, DisplayBlock?> LoadedFunctions { get; private set; } = [];


        public static DisplayBlock NewProject() => 
            new DisplaySSBlockScopeFunction(new SSBlockScopeFunction());
        //TODO: Run new editor right after



        public static DisplayBlock? InstantiateTryEditAndGetDisplay(SSBlockScope Parent, Type blocktype, int index = -1)
        {
            //Try get action data from registry
            if (!EditorRegistry.Actions.TryGetValue(blocktype, out EditorRegistryActionItem? data) || data is null)
                throw new InvalidOperationException("Unregistered action creation");

            //Try get constructor of that block type from the SSCore
            var ctor = blocktype.GetConstructor([])
                    ?? throw new InvalidOperationException($"{blocktype} couldn't be instantiated. Missing blank constructor.");

            //Invoke the constructor and pass parent variables as params

            var Block = ctor.Invoke([]) as SSBlock 
                ?? throw new InvalidOperationException($"{blocktype} couldn't be instantiated. Constructor returned null");
            //tries to open editor. if edited but closed then dump creation
            if (!TryOpenEditor(Block, out bool edited) && edited)
                return null;

            Parent.AddChild(Block, index);
            return InstantiateDisplay(Block);
        }


        public static DisplayBlock? InstantiateDisplay(SSBlock block) 
        {
            //Try get action data from registry
            if (!EditorRegistry.Actions.TryGetValue(block.GetType(), out EditorRegistryActionItem? data) || data is null)
                throw new InvalidOperationException("Unregistered action creation");

            //Get constructor for the display that takes the block type, default block or default scope as param

            var dsctor = data.Display.GetConstructors()
            .Where(ci => ci.GetParameters().Length == 1)
            .FirstOrDefault(ci => ci?.GetParameters().First().ParameterType.IsAssignableFrom(block.GetType()) == true,null)
            ?? throw new InvalidOperationException($"{block.GetType()} display couldn't be instantiated. Missing constructor taking that object type as a parameter.");

            //data.Display.GetConstructor([block.GetType()]) ?? data.Display.GetConstructor([typeof(SSBlock)]) ?? data.Display.GetConstructor([typeof(SSBlockScope)])
            //   ?? throw new InvalidOperationException($"{block.GetType()} display couldn't be instantiated. Missing constructor taking that object type as a parameter.");

            //Check if the class is in fact a derivant of DisplayBlock
            if (!typeof(DisplayBlock).IsAssignableFrom(data.Display))
                throw new InvalidOperationException($"{block.GetType()} display block be instantiated. It does not inherit the DisplayBlock class");

            //Try instantiate the display via the constructor
            return dsctor.Invoke([block]) as DisplayBlock;
        }


        public static bool TryOpenEditor(SSBlock block, out bool edited)
        {
            if (!EditorRegistry.Actions.TryGetValue(block.GetType(), out EditorRegistryActionItem? data) || data is null)
                throw new InvalidOperationException($"{block.GetType()} is not registered as an action");

            //if the object is editable
            if (data.Editable && data.Editor is not null)
            {
                edited = true;
                //Try get the constructor for the editor that takes the block type as param
                //throw new Exception($"{data.Editor.GetConstructors()[0].GetParameters()[0].ParameterType} Or {block.GetType()}\n {data.Editor.GetConstructors()[0].GetParameters()[0].ParameterType == block.GetType()}");

                var edctor = data.Editor.GetConstructors()
                .Where(ci => ci.GetParameters().Length == 1)
                .FirstOrDefault(ci => ci?.GetParameters().First().ParameterType.IsAssignableFrom(block.GetType()) == true, null)
                ?? throw new InvalidOperationException($"{block.GetType()} editor ({data.Editor}) doesn't contain a constructor allowing that type as a parameter");

                //GetConstructor([block.GetType()]) ?? data.Editor.GetConstructor([typeof(SSBlockActionBasic)]) ?? data.Editor.GetConstructor([typeof(SSBlockScope)])
                //?? throw new InvalidOperationException($"{block.GetType()} editor ({data.Editor}) doesn't contain a constructor allowing that type as a parameter");
                //check if the editor class is a window
                if (!typeof(Window).IsAssignableFrom(data.Editor))
                    throw new InvalidOperationException($"{block.GetType()} editor doesn't inherit from a window and can't be opened as a dialog");
                //Create an instance of the editor
                Window? editor = edctor.Invoke([block]) as Window
                    ?? throw new InvalidOperationException($"{block.GetType()} editor couldn't be instantiated. Constructor returned null");
                return editor.ShowDialog() == true;
            }
            edited = false;
            return false;
        }
        public static void LoadWorkingDirectory(string path)
        {

            ExeCore.WorkingDirectory = path;
            var files = Directory.GetFiles(path, "*.seascript");
            LoadedFunctions.Clear();
            foreach (var file in files)
            {
                var a = SerializationEngine.Deserialize(EditorRegistry.RegisteredTypes.ToDictionary(), file);
                if(a is not null)
                    LoadedFunctions.Add(a,null);
            }
        }
        public static DisplayBlock? GetFunctionVisual(SSBlock block)
        {
            if (LoadedFunctions.ContainsKey(block))
            {
                if (LoadedFunctions[block] is null)
                    LoadedFunctions[block] = BuildVisual(block);
                return LoadedFunctions[block];
            }
            return BuildVisual(block);
        }
        public static DisplayBlock? BuildVisual(SSBlock block)
        {
            var display = InstantiateDisplay(block);
            if (typeof(SSBlockScope).IsAssignableFrom(block.GetType()) && display is IActionContainer container)
            {
                SSBlockScope scope = (SSBlockScope)block;
                for(int i = 0; i < scope.Children.Count; i++)
                {
                    var visual = BuildVisual(scope.Children[i]);
                    if (visual is not null)
                        container.AddAction(visual, i);
                }
            }
            return display;
        }
        public static void Save(SSBlock block, string dir)
        {
            SerializationEngine.Serialize(block, dir + $"\\{block.Title}.seascript");
        }
        
    }
}
