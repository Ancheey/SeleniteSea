package pl.Ancheey.SeleniteSea.Tests;

import org.junit.After;
import org.junit.Before;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.chrome.ChromeDriver;
import pl.Ancheey.SeleniteSea.*;

import static org.junit.jupiter.api.Assertions.*;

class SeleniumManagerTest {

    @Test
    void i() {
        assertNotNull(SeleniumManager.I());
    }

    @Test
    void run() {
        CommandStatementWhile loop = new CommandStatementWhile(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.NOT_EQUALS, "10"));

        CommandVarSet varset = new CommandVarSet("test", "0");
        CommandVarModify varmod = new CommandVarModify("test", "1", CommandVarModify.Action.INCREMENT);
        CommandOpen open = new CommandOpen("https://google.pl");

        CommandStatement statement = new CommandStatement();
        statement.add(varset);
        statement.add(open);
        statement.add(loop);
        loop.add(varmod);

        SeleniumManager.I().setProgram(statement);
        SeleniumManager.I().start();
    }

    @Test
    void getDriver() {
        ChromeDriver driver = SeleniumManager.I().getDriver();
        assertNotNull(driver);
    }

    @Test
    void getVars() {
        CommandStatementWhile loop = new CommandStatementWhile(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.NOT_EQUALS, "10"));

        CommandVarSet varset = new CommandVarSet("test", "0");
        CommandVarModify varmod = new CommandVarModify("test", "1", CommandVarModify.Action.INCREMENT);
        CommandOpen open = new CommandOpen("https://google.pl");

        CommandStatement statement = new CommandStatement();
        statement.add(varset);
        statement.add(open);
        statement.add(loop);
        loop.add(varmod);

        SeleniumManager.I().setProgram(statement);
        SeleniumManager.I().start();
        assertNotNull(SeleniumManager.I().getVars());
    }

    @Test
    void getVar() {
        CommandStatementWhile loop = new CommandStatementWhile(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.NOT_EQUALS, "10"));

        CommandVarSet varset = new CommandVarSet("test", "1");
        CommandVarModify varmod = new CommandVarModify("test", "1", CommandVarModify.Action.INCREMENT);
        CommandOpen open = new CommandOpen("https://google.pl");

        CommandStatement statement = new CommandStatement();
        statement.add(varset);
        statement.add(open);
        statement.add(loop);
        loop.add(varmod);

        SeleniumManager.I().setProgram(statement);
        SeleniumManager.I().start();

        assertNotEquals(0,SeleniumManager.I().getVar("test"));
    }

    @Test
    void setVar() {
        getVar();
    }

    @Test
    void sleep() {
        CommandSleep sleep = new CommandSleep(1);

        CommandStatement statement = new CommandStatement();
        statement.add(sleep);

        SeleniumManager.I().setProgram(statement);
        SeleniumManager.I().start();
    }

    @Test
    void getProgram() {
        CommandSleep sleep = new CommandSleep(1);

        CommandStatement statement = new CommandStatement();
        statement.add(sleep);

        SeleniumManager.I().setProgram(statement);
        assertEquals(statement, SeleniumManager.I().getProgram());
    }

    @Test
    void setProgram() {
        getProgram();
    }
}