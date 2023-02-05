package pl.Ancheey.SeleniteSea;

public class CommandOpen extends Command{
    String url;
    private int value;
    public CommandOpen(String url){
        this.url = url;
    }
    @Override
    public void execute() {
        SeleniumManager.I().getDriver().get(url);
    }

    @Override
    public String getDescription() {
        return "Opens " + url;
    }
}
