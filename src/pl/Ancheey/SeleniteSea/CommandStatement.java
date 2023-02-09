package pl.Ancheey.SeleniteSea;

import java.util.*;

/**
 * Base class for all command statements. Those are special as are able to contain other commands, but execute the same as a base Command from the outside
 */
public class CommandStatement extends Command implements ICommandContainer {
    private final List<Command> commands;
    public String name = "";

    /**
     * Creates a new empty Statement
     */
    public CommandStatement(){
        this.commands = new ArrayList<>();
    };

    /**
     * Creates a new statement with a se tof existing commands
     * @param commands commands to be added to the statement
     */
    public CommandStatement(Collection<Command> commands){
        this.commands = new ArrayList<>();
        add(commands);
    }

    /**
     * Adds a single command at the end of the execution line
     * @param command command to be added
     */
    @Override
    public void add(Command command) {
        commands.add(command);
        command.setParent(this);
    }

    /**
     * Adds a command at selected index
     * @param index Index at which the command is to be put in
     * @param command command to be added
     */
    @Override
    public void add(int index, Command command) {
        commands.add(index, command);
        command.setParent(this);
    }

    /**
     * Inserts an entire collection of commands to the statement
     * @param commands commands to be added
     */
    @Override
    public void add(Collection<Command> commands) {
        this.commands.addAll(commands);
        for (Command c: commands) {
            c.setParent(this);
        }
    }

    /**
     * Inserts an entire collection of commands to the statement at a selected index
     * @param index index to input the collection at
     * @param commands commands to be added
     */
    @Override
    public void add(int index, Collection<Command> commands) {
        this.commands.addAll(index, commands);
        for (Command c: commands) {
            c.setParent(this);
        }
    }

    /**
     * Removes the specified command from the statement
     * @param command command to be removed
     */
    @Override
    public void remove(Command command) {
        commands.remove(command);
        command.setParent(null);
    }

    /**
     * Swaps positions of two commands
     * @param c1 command 1
     * @param c2 command 2
     */
    @Override
    public void swap(Command c1, Command c2) {
        if(!new HashSet<>(commands).containsAll( Arrays.asList(c1,c2) )){
            return;
        }
        Collections.swap(commands, commands.indexOf(c1), commands.indexOf(c2));
    }

    /**
     * Pushes a command to an index, moving all others back. If the command is not in the set already, it is added
     * @param index index to be pushed to
     * @param command command to be pushed
     */
    @Override
    public void pushTo(int index, Command command) {
        commands.remove(command);
        commands.add(index, command);
    }

    /**
     * Empties the statement from all commands
     */
    @Override
    public void clear() {
        for (Command c: commands) {
            c.setParent(null);
        }
        commands.clear();
    }

    /**
     * @return The entire command set of a statement
     */
    @Override
    public Collection<Command> getCommands() {
        return commands;
    }

    /**
     * Executes all commands contained in itself
     */
    @Override
    public void execute() {
        for (Command c: commands
             ) {
            c.execute();
        }
    }

    @Override
    public String getDescription() {
        return "Basic Statement";
    }
}
