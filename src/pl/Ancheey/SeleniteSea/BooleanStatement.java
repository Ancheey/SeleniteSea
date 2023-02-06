package pl.Ancheey.SeleniteSea;

import java.io.Serializable;

public interface BooleanStatement extends Serializable {

    /**
     * Used to evaluate the outcome at the moment of calling, not creation.
     */

    boolean evaluate();

    String toString();
    /**
     * Contains all actions possible within a bool statement with a single variable
     */
    enum SingleVar{
        EXISTS,
        NOT_EXISTS
    }

    /**
     * Contains all actions possible within a bool statement with 2 variables
     */
    enum DoubleVar{
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
