package pl.Ancheey.SeleniteSea;

public class CommandSleep extends Command{
    private final int interval;
    public CommandSleep(int interval){
        this.interval = interval;
    }
    @Override
    public void execute() {
        try {
            SeleniumManager.I().sleep(interval);
        }
        catch(Exception ignored){
            //No need dealing with it
        }
    }

    @Override
    public String getDescription() {
        return "Waits for "+ interval + " milliseconds";
    }
}
