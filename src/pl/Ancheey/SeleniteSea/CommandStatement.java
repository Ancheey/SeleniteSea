package pl.Ancheey.SeleniteSea;

import java.util.*;

public class CommandStatement extends Command implements ICommandContainer {
    private final List<Command> commands;

    public CommandStatement(){
        this.commands = new ArrayList<>();
    };
    public CommandStatement(Collection<Command> commands){
        this.commands = new ArrayList<>();
        add(commands);
    }

    @Override
    public void add(Command command) {
        commands.add(command);
    }
    @Override
    public void add(int index, Command command) {
        commands.add(index, command);
    }

    @Override
    public void add(Collection<Command> commands) {
        this.commands.addAll(commands);
    }
    @Override
    public void add(int index, Collection<Command> commands) {
        this.commands.addAll(index, commands);
    }

    @Override
    public void remove(Command command) {
        commands.remove(command);
    }

    @Override
    public void swap(Command c1, Command c2) {
        if(!new HashSet<>(commands).containsAll( Arrays.asList(c1,c2) )){
            return;
        }
        Collections.swap(commands, commands.indexOf(c1), commands.indexOf(c2));
    }

    @Override
    public void pushTo(int index, Command command) {
        commands.remove(command);
        commands.add(index, command);
    }

    @Override
    public void clear() {
        commands.clear();
    }

    @Override
    public Collection<Command> getCommands() {
        return commands;
    }

    @Override
    public void execute(SeleniumManager engine) {
        for (Command c: commands
             ) {
            c.execute(engine);
        }
    }
}
