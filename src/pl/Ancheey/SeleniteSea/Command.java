package pl.Ancheey.SeleniteSea;

public abstract class Command {
    public abstract void execute(SeleniumManager engine);
    public abstract String getDescription();

    private Command parent;

    public Command getParent() {
        return parent;
    }

    public void setParent(Command parent) {
        this.parent = parent;
    }
}
