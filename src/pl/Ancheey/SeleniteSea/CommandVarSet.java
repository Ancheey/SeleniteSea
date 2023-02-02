package pl.Ancheey.SeleniteSea;

public class CommandVarSet extends CommandVar{


    public CommandVarSet(String varName, int value) {
        super(varName, value);
    }

    public CommandVarSet(String varName, EngineVarHandle handle) {
        super(varName, handle);
    }

    @Override
    public void execute(SeleniumManager engine) {
        engine.setVar(varName, getValue());
    }
}
