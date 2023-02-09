package pl.Ancheey.SeleniteSea;

public class CommandOpen extends Command{
    String url;
    private int value;

    /**
     * Creates a command that upon execution will open a webpage
     * @param url webpage address to open
     */
    public CommandOpen(String url){
        this.url = url;
    }

    /**
     * This method forces the driver inside SeleniumManager to open a specified webpage
     */
    @Override
    public void execute() {
        SeleniumManager.I().getDriver().get(url);
    }

    @Override
    public String getDescription() {
        return "Opens " + url;
    }
}
