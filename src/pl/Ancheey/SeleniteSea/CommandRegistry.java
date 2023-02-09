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

    /**
     * Singleton declaration for a registry of commands. Used to keep track of commands that should be allowed to be created by the user.
     * @return A new or existing instance of the command registry
     */
    public static CommandRegistry I(){
        if(instance == null)
            instance = new CommandRegistry();
        return instance;
    }

    /**
     * Registers a class of a command
     * @param command command class to be registered - [classname].class
     */
    public void register(Class<? extends Command> command){
        if(!getRegistry().contains(command))
            getRegistry().add(command);
    }

    /**
     * @return the entire registry
     */
    public List<Class<? extends Command>> getRegistry() {
        return registry;
    }



}
