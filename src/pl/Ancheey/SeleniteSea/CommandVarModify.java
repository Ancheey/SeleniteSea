package pl.Ancheey.SeleniteSea;

/**
 * Used for all variable modifications in the Manager
 */
public class CommandVarModify extends  CommandVar {
    Action action;

    /**
     * Creates a new command that will take the variable varName and use Action on it with value as the parameter
     * @param varName variable name to modify
     * @param value variable name or an int as a parameter
     * @param action action to perform
     */
    public CommandVarModify(String varName, String value, Action action) {
        super(varName, value);
        this.action = action;
    }

    /**
     * Tells the Selenium Manager to set the variable to new value
     */
    @Override
    public void execute() {
        int var = SeleniumManager.I().getVar(varName);
        switch (action){

            case INCREMENT -> SeleniumManager.I().setVar(varName, var + getValue());
            case DECREMENT -> SeleniumManager.I().setVar(varName, var - getValue());
            case MULTIPLY -> SeleniumManager.I().setVar(varName, var * getValue());
            case DIVIDE_BY -> {
                if(getValue() != 0) {
                    SeleniumManager.I().setVar(varName, var / getValue());
                }
            }
        }
    }

    @Override
    public String getDescription() {
        return "Modifies variable \"" + varName + "\" using the action " + action.toString() + " by " + value;
    }

    public enum Action{
        INCREMENT,
        DECREMENT,
        MULTIPLY,
        DIVIDE_BY
    }
}
