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
    public void execute(SeleniumManager engine) {
        while(statement.evaluate()) {
            super.execute(engine);
        }
    }
    public String getDescription() {
        return "Looping statement";
    }

}
