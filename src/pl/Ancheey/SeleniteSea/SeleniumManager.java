package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.chrome.ChromeDriver;

import java.util.HashMap;
import java.util.Map;

public class SeleniumManager extends Thread {
    private SeleniumEngine engine;
    private final Map<String,Integer> vars = new HashMap<>();
    private CommandStatement program;

    public SeleniumManager(CommandStatement program, String chromeDriver){
        this.setProgram(program);
        System.setProperty("webdriver.chrome.driver", chromeDriver);
    }
    public SeleniumManager(String chromeDriver){
        System.setProperty("webdriver.chrome.driver", chromeDriver);
    }
    public void run(){
        engine = new SeleniumEngine();
        getProgram().execute(this);
    }
    public ChromeDriver getDriver() {
        return engine.getDriver();
    }
    public Map<String,Integer> getVars() {
        return vars;
    }
    public int getVar(String name) {
        if(vars.containsKey(name)){
            return vars.get(name);
        }
        return 0;
    }
    public void setVar(String name, int value){
        vars.put(name, value);
    }
    public void sleep(int interval) throws InterruptedException {
        Thread.sleep(interval);
    }

    public CommandStatement getProgram() {
        return program;
    }

    public void setProgram(CommandStatement program) {
        this.program = program;
    }
}
