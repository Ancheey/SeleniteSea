using SeleniteSeaCore;
using SeleniteSeaExecutor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaEditor.modding
{
    internal static class ModHandler
    {
        public static List<EditorMod> LoadedMods { get; } = [];
        public static bool LoadMods()
        {
            string modNameForErrors = "";

            string moddir = @$"{ExeCore.LocalDirectory}mods";
            if (!Directory.Exists(moddir))
                Directory.CreateDirectory(moddir);
            //loading assemblies

            var dlls = Directory.GetFiles(moddir, "*.dll");
            foreach (var dll in dlls)
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(dll);
                    var derivedTypes = a.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(EditorMod)));
                    if (derivedTypes.Count() != 1)
                        throw new InvalidOperationException($"Found {derivedTypes.Count()} mod declarations in {dll}. Only one allowed per mod file");
                    var instance = (EditorMod?)Activator.CreateInstance(derivedTypes.First())
                        ?? throw new ArgumentException($"Mod {derivedTypes.First()} from {dll} couldn't be declared. Instance creation failed.");
                    modNameForErrors = instance.Name;
                    instance.OnLoad();
                    LoadedMods.Add(instance);
                }
                catch (Exception e)
                {
                    Debug.Log(StatusCode.Error, (modNameForErrors == "" ? "" : $"[{modNameForErrors}] ") + e.ToString(), null);
                }
            }
            return true;
        }
    }
}
