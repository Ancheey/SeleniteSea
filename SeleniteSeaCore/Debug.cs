using SeleniteSeaCore.codeblocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore
{
    public delegate void OnDebugMessage(StatusCode status, string text, SSBlock? errorCaller);
    public static class Debug
    {
        public static int REFERENCE_DEPTH { get; set; } = 8;
        public static event OnDebugMessage? OnDebugMessageEvent;

        public static void Log(StatusCode status, string text, SSBlock? errorCaller)
        {
            if(OnDebugMessageEvent is null)
                throw new ArgumentNullException("No registered Debug outputs");
            OnDebugMessageEvent(status, text, errorCaller);
        }
        
    }
    public enum StatusCode
    {
        Success,
        Info,
        Error
    }
}
