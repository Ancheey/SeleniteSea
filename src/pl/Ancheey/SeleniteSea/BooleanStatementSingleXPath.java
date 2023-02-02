package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.By;

public class BooleanStatementSingleXPath extends BooleanStatementSingle{
    SeleniumManager engine;
    String xPath;
    public BooleanStatementSingleXPath(SeleniumManager engine, String xPath, SingleVar statement) {
        super(statement);
        this.engine = engine;
        this.xPath = xPath;
    }

    @Override
    boolean evaluate() {
        switch (statement) {
            case EXISTS -> {
                return !engine.getDriver().findElements(By.xpath(xPath)).isEmpty();
            }
            case NOT_EXISTS -> {
                return engine.getDriver().findElements(By.xpath(xPath)).isEmpty();
            }
        }
        return false;
    }
}
