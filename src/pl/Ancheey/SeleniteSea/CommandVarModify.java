package pl.Ancheey.SeleniteSea;

public class CommandVarModify extends  CommandVar {
    Action action;
    public CommandVarModify(String varName, int value, Action action) {
        super(varName, value);
        this.action = action;
    }
    public CommandVarModify(String varName, EngineVarHandle handle, Action action) {
        super(varName, handle);
        this.action = action;
    }

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
        return "Modifies variable \"" + varName + "\" using the action " + action.toString() + " by " + getValue();
    }

    public enum Action{
        INCREMENT,
        DECREMENT,
        MULTIPLY,
        DIVIDE_BY
    }
}
