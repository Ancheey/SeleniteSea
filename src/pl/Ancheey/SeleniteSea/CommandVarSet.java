package pl.Ancheey.SeleniteSea;

public class CommandVarSet extends CommandVar{


    public CommandVarSet(String varName, String value) {
        super(varName, value);
    }

    @Override
    public void execute() {
        SeleniumManager.I().setVar(varName, getValue());
    }

    @Override
    public String getDescription() {
        return "Creates or overwrites variable \"" + varName + "\" by setting it to "+ getValue();
    }
}
