package pl.Ancheey.SeleniteSea;

public abstract class BooleanStatement {
    /**
     * Used to evaluate the outcome at the moment of calling, not creation.
     */

    abstract boolean evaluate();
    /**
     * Contains all actions possible within a bool statement with a single variable
     */
    public enum SingleVar{
        EXISTS,
        NOT_EXISTS
    }

    /**
     * Contains all actions possible within a bool statement with 2 variables
     */
    public enum DoubleVar{
        EQUALS,
        NOT_EQUALS,
        IS_LESS_THAN,
        IS_MORE_THAN,
        IS_LESS_OR_EQUAL_THAN,
        IS_MORE_OR_EQUAL_THAN,
        IS_DIVISIBLE_BY,
        IS_NOT_DIVISIBLE_BY
    }
}
