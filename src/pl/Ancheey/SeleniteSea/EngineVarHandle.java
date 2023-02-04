package pl.Ancheey.SeleniteSea;

public class EngineVarHandle {
    String varName;
    public EngineVarHandle(String varName){
        this.varName = varName;
    }
    public int getValue(){
        return SeleniumManager.I().getVar(varName);
    }
    public boolean exists(){
        return SeleniumManager.I().getVars().containsKey(varName);
    }
}
