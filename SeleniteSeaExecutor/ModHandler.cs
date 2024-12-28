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
        public static string LocalDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public static List<SeaMod> LoadedMods { get; } = [];
        public static bool LoadMods()
        {
            string moddir = @$"{LocalDirectory}\Mods";
            if (!Directory.Exists(moddir))
                Directory.CreateDirectory(moddir);
            //loading assemblies
            try
            {
                var dlls = Directory.GetFiles(moddir, "*.dll");
                foreach (var dll in dlls)
                {
                    Assembly a = Assembly.LoadFrom(dll);
                    var derivedTypes = a.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(SeaMod)));
                    if (derivedTypes.Count() != 1)
                        throw new InvalidOperationException($"Found {derivedTypes.Count()} mod declarations in {dll}. Only one allowed per mod file");
                    var instance = (SeaMod?)Activator.CreateInstance(derivedTypes.First())
                        ?? throw new ArgumentException($"Mod {derivedTypes.First()} from {dll} couldn't be declared. Instance creation failed.");
                    instance.OnLoad();
                    LoadedMods.Add(instance);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(StatusCode.Error, e.ToString(), null);
                return false;
            }
        }
    }
}
