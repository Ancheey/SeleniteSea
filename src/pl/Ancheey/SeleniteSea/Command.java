package pl.Ancheey.SeleniteSea;

public abstract class Command {
    public abstract void execute(); //REMOVE THE NEED FOR THIS, REPLACE WITH INSTANCE
    public abstract String getDescription();

    private Command parent;

    public Command getParent() {
        return parent;
    }

    public void setParent(Command parent) {
        this.parent = parent;
    }

    @Override
    public String toString() {
        return getClass().getSimpleName();
    }
}
