package pl.Ancheey.SeleniteSea;

public abstract class CommandAction extends Command{
    String xPath;
    public CommandAction(String xPath){
        this.xPath = xPath;
    }
}
