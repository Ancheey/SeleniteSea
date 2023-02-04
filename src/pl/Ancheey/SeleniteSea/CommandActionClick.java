package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.By;

public class CommandActionClick extends CommandAction{
    public CommandActionClick(String xPath){
        super(xPath);
    }

    @Override
    public void execute() {
        SeleniumManager.I().getDriver().findElement(By.xpath(xPath)).click();
    }

    @Override
    public String getDescription() {
        return "Simulates a click on an item with an xPath: "+ xPath;
    }
}
