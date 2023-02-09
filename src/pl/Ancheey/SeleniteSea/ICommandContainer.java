package pl.Ancheey.SeleniteSea;

import java.util.Collection;

/**
 * This distinguishes Command Statements from base Commands
 */
public interface ICommandContainer {
    void add(Command command);
    void add(int index, Command command);
    void add(Collection<Command> commands);
    void add(int index, Collection<Command> commands);
    void remove(Command command);
    void swap(Command c1, Command c2);
    void pushTo(int index, Command command);
    void clear();
    Collection<Command> getCommands();
}
