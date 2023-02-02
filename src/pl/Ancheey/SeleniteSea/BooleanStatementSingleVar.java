package pl.Ancheey.SeleniteSea;

public class BooleanStatementSingleVar extends BooleanStatementSingle{
    EngineVarHandle handle;
    public BooleanStatementSingleVar(EngineVarHandle handle, SingleVar statement) {
        super(statement);
        this.handle = handle;
    }

    @Override
    boolean evaluate() {
        switch (statement) {
            case EXISTS -> {
                return handle.exists();
            }
            case NOT_EXISTS -> {
                return !handle.exists();
            }
        }
        return false;
    }
}
