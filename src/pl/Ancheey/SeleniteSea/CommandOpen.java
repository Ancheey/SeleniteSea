package pl.Ancheey.SeleniteSea;

public class CommandOpen extends Command{
    String url;
    private int value;
    EngineVarHandle handle;
    public CommandOpen(String url){
        this.url = url;
    }
    @Override
    public void execute(SeleniumManager engine) {
        engine.getDriver().get(url);
    }

    @Override
    public String getDescription() {
        return "Opens " + url;
    }
}
