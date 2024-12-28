using SeleniteSeaCore;
using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.codeblocks.actions;
using SeleniteSeaCore.codeblocks.scopes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SeleniteSeaExecutor
{
    internal static class TypeRegistry
    {
        private readonly static Dictionary<string, Type> _registeredTypes = [];
        public static ImmutableDictionary<string, Type> RegisteredTypes => _registeredTypes.ToImmutableDictionary();
        public static void RegisterType<T> () where T : SSBlock => _registeredTypes.Add(typeof(T).ToString(), typeof(T));
        public static bool LoadTypes()
        {
            try
            {
                RegisterType<SSBlockScopeFunction>();
                RegisterType<SSBlockScopeIterate>();
                RegisterType<SSBlockScopeIf>();
                RegisterType<SSBlockScopeWhile>();
                RegisterType<SSBlockActionAdd>();
                RegisterType<SSBlockActionBreak>();
                RegisterType<SSBlockActionContinue>();
                RegisterType<SSBlockActionExecuteFunction>();
                RegisterType<SSBlockActionReturnValue>();
                RegisterType<SSBlockActionSet>();
                RegisterType<SSBlockActionSetInterpolated>();
                RegisterType<SSBlockActionWait>();
                RegisterType<SSBlockActionWrite>();

                foreach (var mod in ModHandler.LoadedMods)
                {
                    mod.OnRegisterExecutor();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(StatusCode.Error, $"Couldn't register types: {ex}", null);
                return false;
            }
        }
    }
}
