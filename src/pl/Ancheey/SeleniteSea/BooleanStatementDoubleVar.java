package pl.Ancheey.SeleniteSea;

public class BooleanStatementDoubleVar extends BooleanStatementDouble{
    EngineVarHandle firstValue;
    int firstValueInt;

    EngineVarHandle secondValue;
    int secondValueInt;

    public BooleanStatementDoubleVar(EngineVarHandle firstValue, DoubleVar statement, EngineVarHandle secondValue) {
        super(statement);
        this.firstValue = firstValue;
        this.secondValue = secondValue;
    }
    public BooleanStatementDoubleVar(EngineVarHandle firstValue, DoubleVar statement, int secondValue) {
        super(statement);
        this.firstValue = firstValue;
        this.secondValueInt = secondValue;
    }
    public BooleanStatementDoubleVar(int firstValue, DoubleVar statement, EngineVarHandle secondValue) {
        super(statement);
        this.firstValueInt = firstValue;
        this.secondValue = secondValue;
    }
    public BooleanStatementDoubleVar(int firstValue, DoubleVar statement, int secondValue) {
        super(statement);
        this.firstValueInt = firstValue;
        this.secondValueInt = secondValue;
    }

    @Override
    boolean evaluate() {
        int val1 = firstValue != null ? firstValue.getValue() : firstValueInt;
        int val2 = secondValue != null ? secondValue.getValue() : secondValueInt;
        switch (statement) {
            case EQUALS -> {
                return val1 == val2;
            }
            case NOT_EQUALS -> {
                return val1 != val2;
            }
            case IS_LESS_THAN -> {
                return val1 < val2;
            }
            case IS_MORE_THAN -> {
                return val1 > val2;
            }
            case IS_LESS_OR_EQUAL_THAN -> {
                return val1 <= val2;
            }
            case IS_MORE_OR_EQUAL_THAN -> {
                return val1 >= val2;
            }
            case IS_DIVISIBLE_BY -> {
                return val1 % val2 == 0;
            }
            case IS_NOT_DIVISIBLE_BY -> {
                return val1 % val2 != 0;
            }
        }
        return false;
    }
}
