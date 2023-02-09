package pl.Ancheey.SeleniteSea;

/**
 * A boolean statement used as a parameter for commands
 * Takes 2 values and a comparator
 * used to evaluate one value based on the other
 */
public class BooleanStatementDouble implements BooleanStatement{
    DoubleVar statement;
    String firstValue;
    String secondValue;

    public BooleanStatementDouble(String firstValue, DoubleVar statement, String secondValue) {
        this.statement = statement;
        this.firstValue = firstValue;
        this.secondValue = secondValue;
    }

    /**
     * @return evaluates the statement
     */
    public boolean evaluate() {
        Object val1 = firstValue;
        Object val2 = secondValue;

        //Finds out current variable if they exist
        //Changes strings to ints if possible
            if(SeleniumManager.I().getVars().containsKey(firstValue)) {
                val1 = SeleniumManager.I().getVar(firstValue);

            }
            else{
                try{
                    val1 = Integer.parseInt((String)val1);
                }catch(Exception ignored){}
            }
            if(SeleniumManager.I().getVars().containsKey(secondValue))
            {
                val2 = SeleniumManager.I().getVar(secondValue);
            }
            else{
                try{
                    val2 = Integer.parseInt((String)val2);
                }catch(Exception ignored){}
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
        return firstValue + " " + statement.toString() + " " + secondValue;
    }

    /**
     * used to evaluate two ints
     * @param val1 parsed value 1
     * @param val2 parsed value 2
     * @return evaluated boolean
     */
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

    /**
     * Used to evaluate two objects of unknown types
     * @param val1 object 1
     * @param val2 object 2
     * @return evaluated whether two objects are the same object
     */
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
