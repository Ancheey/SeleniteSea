package pl.Ancheey.SeleniteSea;

import java.util.Collection;

/**
 * A command statement containing other commands that will execute over and over until the provided boolean statement evaluates to false
 */
public class CommandStatementWhile extends CommandStatement{
    BooleanStatement statement;

    /**
     * Creates a new While statement
     * @param statement boolean to evaluate
     */
    public CommandStatementWhile(BooleanStatement statement){
        this.statement = statement;
    }
    @Deprecated
    private CommandStatementWhile(BooleanStatement statement, Collection<Command> commands){
        this.statement = statement;
        add(commands);
    }

    /**
     * Executes over and over while the boolean statement is true
     */
    @Override
    public void execute() {
        while(statement.evaluate()) {
            super.execute();
        }
    }
    public String getDescription() {
        return "Looping statement";
    }

}
