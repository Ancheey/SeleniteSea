package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.chrome.ChromeDriver;

import java.io.File;
import java.net.URISyntaxException;
import java.util.HashMap;
import java.util.Map;

public class SeleniumManager extends Thread {

    public static String CHROMEDRIVER = "chromedriver.exe";
    public boolean used = false;
    private static SeleniumManager instance;
    private ChromeDriver engine;
    private final Map<String,Integer> vars = new HashMap<>();
    private CommandStatement program;

    /**
     * Singleton Declaration for the Selenium Manager
     * THE INSTANCE WILL BE DUMPED IF THE PROCESS HAS FINISHED
     * @return Returns a new or currently active instance of Selenium Manager
     */
    public static SeleniumManager I(){
        if(instance == null || instance.used){
            instance = new SeleniumManager();
        }
        return instance;
    }
    private SeleniumManager() {}

    /**
     * Tries to create a new engine and executes the bound program on it.
     */
    public void run(){
        try{
            engine = new ChromeDriver();
            getProgram().execute();
            MainWindow.I().addTextToConsole( getProgram().name + " finished!");
        }
        catch(Exception e){
            MainWindow.I().addTextToConsole(e.getClass().getSimpleName() + ": " +e.getMessage());
            e.printStackTrace();
        }
        used = true;
    }

    public ChromeDriver getDriver() {
        return engine;
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
