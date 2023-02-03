package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.chrome.ChromeDriver;

import java.util.HashMap;
import java.util.Map;

public class SeleniumManager extends Thread {

    public static String CHROMEDRIVER = "D:\\Java Projects\\SeleniteSea\\chromedriver.exe";
    public boolean haltAfterAction = false;
    private static SeleniumManager instance;
    private SeleniumEngine engine;
    private final Map<String,Integer> vars = new HashMap<>();
    private CommandStatement program;

    public static SeleniumManager I(){
        if(instance == null){
            instance = new SeleniumManager();
        }
        return instance;
    }
    private SeleniumManager(){
        System.setProperty("webdriver.chrome.driver", CHROMEDRIVER);
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
