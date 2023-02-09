package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.chrome.ChromeDriver;
@Deprecated
public class SeleniumEngine {
    private final ChromeDriver driver;

    public SeleniumEngine() {
        this.driver = new ChromeDriver();
    }

    public ChromeDriver getDriver() {
        return driver;
    }
}
