package pl.Ancheey.SeleniteSea.Tests;

import static org.junit.jupiter.api.Assertions.*;

import org.junit.After;
import org.junit.Before;
import org.openqa.selenium.InvalidSelectorException;
import pl.Ancheey.SeleniteSea.*;


class BooleanStatementSingleTest {

    @Before
    public void prep(){
        Main.main(new String[0]);
        CommandStatementWhile loop = new CommandStatementWhile(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.NOT_EQUALS, "10"));

        CommandVarSet varset = new CommandVarSet("test", "0");
        CommandVarModify varmod = new CommandVarModify("test", "1", CommandVarModify.Action.INCREMENT);
        CommandSleep sleep = new CommandSleep(500);
        CommandOpen open = new CommandOpen("https://google.pl");

        CommandStatement statement = new CommandStatement();
        statement.add(varset);
        statement.add(open);
        statement.add(loop);
        loop.add(varmod);
        loop.add(sleep);

        SeleniumManager.I().setProgram(statement);
        SeleniumManager.I().start();
    }
    @After
    public void finish(){
        MainWindow.I().dispose();
    }
    @org.junit.jupiter.api.Test
    void testEvaluate() {
        var f = new BooleanStatementSingle("5", BooleanStatement.SingleVar.EXISTS);
        assertFalse(f.evaluate());

        f = new BooleanStatementSingle("5", BooleanStatement.SingleVar.NOT_EXISTS);
        assertTrue(f.evaluate());

        MainWindow.I().dispose();
    }

    @org.junit.jupiter.api.Test
    void testToString() {
        var f = new BooleanStatementSingle("5", BooleanStatement.SingleVar.EXISTS);
        assertEquals("5 EXISTS", f.toString());
    }
}