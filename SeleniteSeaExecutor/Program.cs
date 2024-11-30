// See https://aka.ms/new-console-template for more information
using SeleniteSeaCore;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
var handle = GetConsoleWindow();
const int SW_HIDE = 0;
//const int SW_SHOW = 5;


Debug.OnDebugMessageEvent += Debug_OnDebugMessageEvent;
if (args.Length > 1 && args[1] == "-h")
    //hide window if "-h"
    ShowWindow(handle, SW_HIDE);

Console.WriteLine("Hello, World!");
if (args.Length > 0)
{
    //check for file if exists - error out if it doesnt
}
//Execute code (this mode is for drag and drop or console execution of functions)  


    




static void Debug_OnDebugMessageEvent(StatusCode status, string text, SeleniteSeaCore.codeblocks.SSBlock? errorCaller)
{
    if (status == StatusCode.Error)
    {
        Console.WriteLine("ERROR: "+text);
    }
    else 
        Console.WriteLine(text);
}

