package pl.Ancheey.SeleniteSea;

public class BooleanStatementDouble implements BooleanStatement{
    DoubleVar statement;
    Object firstValue;
    Object secondValue;

    public BooleanStatementDouble(Object firstValue, DoubleVar statement, Object secondValue) {
        this.statement = statement;
        this.firstValue = firstValue;
        this.secondValue = secondValue;
    }

    public boolean evaluate() {
        Object val1 = firstValue;
        Object val2 = secondValue;

        //Finds out current variable if they exist
        //Changes strings to ints if possible
        if (firstValue.getClass() == String.class && SeleniumManager.I().getVars().containsKey((String) firstValue)) {
            val1 = SeleniumManager.I().getVar((String) firstValue);
        }
        if (secondValue.getClass() == String.class && SeleniumManager.I().getVars().containsKey((String) secondValue)) {
            val2 = SeleniumManager.I().getVar((String) secondValue);
        }

        //if items are of the same class, then compare. If not then see if equal or not, else return false;
        //if items are of the same class then evaluate ints or objects
        if (val1.getClass() == val2.getClass()) {
            if (val1 instanceof Integer) {
                return evaluateInt((Integer) val1, (Integer) val2);
            }
            else{
                return evaluateObject(val1, val2);
            }
        } else {
            return statement == DoubleVar.NOT_EQUALS;
        }
    }

    @Override
    public String toString() {
        return firstValue.toString() + " " + statement.toString() + " " + secondValue.toString();
    }

    private boolean evaluateInt(int val1, int val2) {
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
    private boolean evaluateObject(Object val1, Object val2){
        switch (statement){
            case EQUALS -> {
                return val1 == val2;
            }
            case NOT_EQUALS -> {
                return val1 != val2;
            }
            default -> {
                return false;
            }
        }
    }
}
