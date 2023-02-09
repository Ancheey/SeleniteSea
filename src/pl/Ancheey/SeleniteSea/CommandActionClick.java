package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.By;

public class CommandActionClick extends CommandAction{
    /**
     * Creates a command that will try to click an object on currently opened webpage
     * @param xPath xPath of the object to be clicked
     */
    public CommandActionClick(String xPath){
        super(xPath);
    }

    /**
     * If objects with this xpath exist, this will target the first one and click it
     * if not found or the xPath is faulty, this will throw an exception and stop execution
     */
    @Override
    public void execute() {
        SeleniumManager.I().getDriver().findElements(By.xpath(xPath)).get(0).click();
    }

    @Override
    public String getDescription() {
        return "Simulates a click on an item with an xPath: "+ xPath;
    }
}
