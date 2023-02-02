package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.By;

public class CommandActionClick extends CommandAction{
    public CommandActionClick(String xPath){
        super(xPath);
    }

    @Override
    public void execute(SeleniumManager engine) {
        engine.getDriver().findElement(By.xpath(xPath)).click();
    }
}
