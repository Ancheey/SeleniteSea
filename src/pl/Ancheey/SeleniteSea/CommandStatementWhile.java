package pl.Ancheey.SeleniteSea;

import java.util.Collection;

public class CommandStatementWhile extends CommandStatement{
    BooleanStatement statement;
    public CommandStatementWhile(BooleanStatement statement){
        this.statement = statement;
    }
    public CommandStatementWhile(BooleanStatement statement, Collection<Command> commands){
        this.statement = statement;
        add(commands);
    }
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
