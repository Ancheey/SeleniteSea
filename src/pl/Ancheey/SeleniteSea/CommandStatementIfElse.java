package pl.Ancheey.SeleniteSea;

import java.util.Collection;

/**
 * USE IFS INSTEAD.
 * Editor does not support Bi-container statements
 */
@Deprecated
public class CommandStatementIfElse extends CommandStatement{
    private CommandStatement elseStatement = new CommandStatement();
    BooleanStatement statement;
    public CommandStatementIfElse(BooleanStatement statement){
        this.statement = statement;
    }
    public CommandStatementIfElse(BooleanStatement statement, Collection<Command> commands, Collection<Command> elseCommands){
        this.statement = statement;
        add(commands);
        getElseStatement().add(elseCommands);
    }
    public void addElse(Command command){
        getElseStatement().add(command);
    }
    public void addElse(Collection<Command> command){
        getElseStatement().add(command);
    }
    @Override
    public void execute(SeleniumManager engine) {
        if(statement.evaluate()) {
            super.execute(engine);
        }
        else{
            elseStatement.execute(engine);
        }
    }

    public CommandStatement getElseStatement() {
        return elseStatement;
    }

    public void setElseStatement(CommandStatement elseStatement) {
        this.elseStatement = elseStatement;
    }
}
