package pl.Ancheey.SeleniteSea;

import java.io.Serializable;

/**
 * Base building block of all Commands
 * Not to be created by itself, but to build functioning commands from it
 * This and all inheriting objects are serializable to enable saving files
 */
public abstract class Command implements Serializable {
    public abstract void execute();
    public abstract String getDescription();

    private Command parent;

    /**
     * @return Returns the parent of this node. CAN BE NULL IF THE COMMAND IS A MAIN STATEMENT
     */
    public Command getParent() {
        return parent;
    }

    /**
     * A statement passes itself as a parent when a command is added to it
     * @param parent A CommandStatement that will own this node
     */
    public void setParent(Command parent) {
        this.parent = parent;
    }

    @Override
    public String toString() {
        return getClass().getSimpleName();
    }
}
