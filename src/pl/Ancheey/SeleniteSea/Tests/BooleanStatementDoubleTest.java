package pl.Ancheey.SeleniteSea.Tests;

import pl.Ancheey.SeleniteSea.BooleanStatement;
import pl.Ancheey.SeleniteSea.BooleanStatementDouble;

import static org.junit.jupiter.api.Assertions.*;

class BooleanStatementDoubleTest {

    @org.junit.jupiter.api.Test
    void testEvaluate() {
        var f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.EQUALS, "10");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.NOT_EQUALS, "5");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.IS_DIVISIBLE_BY, "2");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.IS_NOT_DIVISIBLE_BY, "5");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.IS_LESS_THAN, "4");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.IS_MORE_THAN, "5");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.IS_LESS_OR_EQUAL_THAN, "4");
        assertFalse(f.evaluate());

        f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.IS_MORE_OR_EQUAL_THAN, "6");
        assertFalse(f.evaluate());

    }

    @org.junit.jupiter.api.Test
    void testToString() {
        var f = new BooleanStatementDouble("5", BooleanStatement.DoubleVar.EQUALS, "10");
        assertEquals("5 EQUALS 10", f.toString());
    }
}