package pl.Ancheey.SeleniteSea;

public class EngineVarHandle {
    SeleniumManager engine;
    String varName;
    public EngineVarHandle(SeleniumManager engine, String varName){
        this.engine = engine;
        this.varName = varName;
    }
    public int getValue(){
        return engine.getVar(varName);
    }
    public boolean exists(){
        return engine.getVars().containsKey(varName);
    }
}
