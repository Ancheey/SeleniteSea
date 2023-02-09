package pl.Ancheey.SeleniteSea;

import java.util.Collection;

/**
 * A command statement containing other commands that will execute if the provided boolean evaluates to true upon execution
 */
public class CommandStatementIf extends CommandStatement{
    BooleanStatement statement;

    /**
     * Creates a new IF statement based on the provided boolean
     * @param statement boolean statement to evaluate
     */
    public CommandStatementIf(BooleanStatement statement){
        this.statement = statement;
    }
    @Deprecated
    private CommandStatementIf(BooleanStatement statement, Collection<Command> commands){
        this.statement = statement;
        add(commands);
    }
    @Override
    public void execute() {
        if(statement.evaluate()) {
            super.execute();
        }
    }

    @Override
    public String getDescription() {
        return "Statement that runs only if conditions are met: " + statement.toString();
    }
}
