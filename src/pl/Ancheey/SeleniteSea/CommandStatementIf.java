package pl.Ancheey.SeleniteSea;

import java.util.Collection;

public class CommandStatementIf extends CommandStatement{
    BooleanStatement statement;
    public CommandStatementIf(BooleanStatement statement){
        this.statement = statement;
    }
    public CommandStatementIf(BooleanStatement statement, Collection<Command> commands){
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
        return "Statement that runs only if conditions are met.";
    }
}
