package pl.Ancheey.SeleniteSea;

public abstract class CommandVar extends Command{
    String varName;
    private int value;
    EngineVarHandle handle;
    public CommandVar(String varName, int value){
        this.varName = varName;
        this.value = value;
    }
    public CommandVar(String varName, EngineVarHandle handle){
        this.varName = varName;
        this.handle = handle;
    }
    public int getValue(){
        if(handle != null){
            return handle.getValue();
        }
        return value;
    }
}
