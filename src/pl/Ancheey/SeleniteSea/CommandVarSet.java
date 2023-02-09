package pl.Ancheey.SeleniteSea;

public class CommandVarSet extends CommandVar{


    /**
     * Creates a new variable with this name or overwrites an existing one
     * @param varName variable name
     * @param value name of variable or int to be taken as a parameter
     */
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
