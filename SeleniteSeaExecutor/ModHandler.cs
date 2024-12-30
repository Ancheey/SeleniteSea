using SeleniteSeaCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaExecutor
{
    internal static class ModHandler
    { 
        public static List<SeaMod> LoadedMods { get; } = [];
        public static bool LoadMods()
        {
            string modNameForErrors = "";

            string moddir = @$"{ExeCore.LocalDirectory}mods";
            string depdir = @$"{ExeCore.LocalDirectory}dependencies";
            if (!Directory.Exists(moddir))
                Directory.CreateDirectory(moddir);
            if (!Directory.Exists(moddir))
                Directory.CreateDirectory(moddir);
            //loading assemblies

            var dlls = Directory.GetFiles(moddir, "*.dll");
            foreach (var dll in dlls)
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(dll);
                    var derivedTypes = a.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(SeaMod)));
                    if (derivedTypes.Count() != 1)
                        throw new InvalidOperationException($"Found {derivedTypes.Count()} mod declarations in {dll}. Only one allowed per mod file");

                    var refs = a.GetReferencedAssemblies();
                    foreach (var reference in refs)
                    {
                        if (reference.Name == "SeleniteSeaEditor")
                            continue;
                        if (!AppDomain.CurrentDomain.GetAssemblies().Any(k => k.GetName().FullName == reference.FullName))
                        {
                            try
                            {
                                Assembly.Load(reference);
                            }
                            catch (Exception)
                            {
                                if (File.Exists(@$"{depdir}\{reference.Name}.dll"))
                                    Assembly.LoadFrom(@$"{depdir}\{reference.Name}.dll");
                                else
                                    throw;
                            }
                        }
                    }

                    var instance = (SeaMod?)Activator.CreateInstance(derivedTypes.First())
                        ?? throw new ArgumentException($"Mod {derivedTypes.First()} from {dll} couldn't be declared. Instance creation failed.");
                    modNameForErrors = instance.Name;
                    instance.OnLoad();
                    LoadedMods.Add(instance);
                }
                catch (Exception e)
                {
                    Debug.Log(StatusCode.Error, (modNameForErrors == "" ? "" : $"[{modNameForErrors}] ") + e.Message, null);
                }
            }
            return true;
        }
    }
}
