
using SeleniteSeaCore;
using SeleniteSeaExecutor;
using SeleniteSeaExecutor.modding;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
var handle = GetConsoleWindow();
const int SW_HIDE = 0;
//const int SW_SHOW = 5;

bool hidden = false;
//Add debug console display
Debug.OnDebugMessageEvent += Debug_OnDebugMessageEvent;


if (args.Length > 1 && args[1] == "-h")
{
    //hide window if "-h"
    ShowWindow(handle, SW_HIDE);
    hidden = true;
}

if (args.Length > 0 && File.Exists(args[0]))
{
    if (!args[0].EndsWith(".seascript"))
    {
        Debug.Log(StatusCode.Error, "Provided file is not a sea script", null);
        {
            Quit();
            return;
        }
    }
    if (!ModHandler.LoadMods() || !TypeRegistry.LoadTypes())
    {
        Quit();
        return;
    }
        

    var block = SerializationEngine.Deserialize(TypeRegistry.RegisteredTypes.ToDictionary(), args[0]);
    if (block is null)
    {
        Quit();
        return; //just to avoid warnings
    }

    try
    {
        ExeCore.WorkingDirectory = Path.GetDirectoryName(args[0])??ExeCore.LocalDirectory;
        foreach (var mod in ModHandler.LoadedMods)
            mod.BeforeExecution();
        var result = await SSProcess.Execute(block, TypeRegistry.RegisteredTypes.ToDictionary(), ExeCore.WorkingDirectory);
        if (result.ReturnValue is not null)
            Debug.Log(StatusCode.Success, $"{result.ReturnValue}", null);
    }
    catch (Exception e)
    {
        Debug.Log(StatusCode.Error, e.ToString(), null);
    }
    foreach (var mod in ModHandler.LoadedMods)
        mod.AfterExecution();
    Quit();
    return;
}
else
{
    Debug.Log(StatusCode.Error, "File not provided or not found", null);
    Quit();
}

void Quit()
{
    if (!hidden)
        Console.Read();
    Environment.Exit(0);
}
static void Debug_OnDebugMessageEvent(StatusCode status, string text, SeleniteSeaCore.codeblocks.SSBlock? errorCaller)
{
    if (status == StatusCode.Error)
    {
        Console.WriteLine("ERROR: "+text);
    }
    else 
        Console.WriteLine(text);
}

