package pl.Ancheey.SeleniteSea;

import java.util.ArrayList;
import java.util.List;

/**
 * ONLY COMMANDS REGISTERED HERE CAN BE USED AND CREATED.
 * DO NOT ADD CommandStatement HERE
 */
public class CommandRegistry {
    private static CommandRegistry instance;
    private final List<Class< ? extends Command>> registry;
    private CommandRegistry(){
        registry = new ArrayList<>();
    }

    public static CommandRegistry I(){
        if(instance == null)
            instance = new CommandRegistry();
        return instance;
    }

    public void register(Class<? extends Command> command){
        if(!getRegistry().contains(command))
            getRegistry().add(command);
    }
    public List<Class<? extends Command>> getRegistry() {
        return registry;
    }



}
