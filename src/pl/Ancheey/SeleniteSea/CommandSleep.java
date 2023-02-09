package pl.Ancheey.SeleniteSea;

public class CommandSleep extends Command{
    private final int interval;

    /**
     * Creates a command that will force the driver to fall asleep for a provided amount of time
     * @param interval time in milliseconds
     */
    public CommandSleep(int interval){
        this.interval = interval;
    }

    /**
     * Forces the driver thread to sleep
     */
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
