package pl.Ancheey.SeleniteSea;

import java.util.Collection;

public class CommandStatementAwait extends CommandStatement{
    BooleanStatementSingleXPath statement;
    int timeout;
    int interval;

    public CommandStatementAwait(BooleanStatementSingleXPath statement){
        this.statement = statement;
    }
    public CommandStatementAwait(BooleanStatementSingleXPath statement, int timeout){
        this.statement = statement;
        this.timeout = timeout;
    }
    public CommandStatementAwait(BooleanStatementSingleXPath statement, int timeout, int interval){
        this.statement = statement;
        this.timeout = timeout;
        this.interval = interval;
    }
    public CommandStatementAwait(BooleanStatementSingleXPath statement, Collection<Command> commands){
        this.statement = statement;
        add(commands);
    }
    public CommandStatementAwait(BooleanStatementSingleXPath statement, int timeout, Collection<Command> commands){
        this.statement = statement;
        this.timeout = timeout;
        add(commands);
    }
    public CommandStatementAwait(BooleanStatementSingleXPath statement, int timeout, int interval, Collection<Command> commands){
        this.statement = statement;
        this.timeout = timeout;
        this.interval = interval;
        add(commands);
    }
    @Override
    public void execute(SeleniumManager engine) {
        int DEFAULT_TIMEOUT = 1500;
        int timeout = this.timeout == 0 ? DEFAULT_TIMEOUT : this.timeout;

        int DEFAULT_INTERVAL = 100;
        int interval = this.interval == 0 ? DEFAULT_INTERVAL : this.interval;
        try {
            while (timeout > 0 && !statement.evaluate()) {
                if(timeout > interval){
                    engine.sleep(interval);
                    timeout -= interval;
                }else{
                    engine.sleep(timeout);
                    timeout = 0;
                }
            }
            if(statement.evaluate()) {
                super.execute(engine);
            }
        }
        catch(Exception ignored){
            //means that engine was interrupted
        }
    }

    @Override
    public String getDescription() {
        int DEFAULT_TIMEOUT = 1500;
        int timeout = this.timeout == 0 ? DEFAULT_TIMEOUT : this.timeout;

        int DEFAULT_INTERVAL = 100;
        int interval = this.interval == 0 ? DEFAULT_INTERVAL : this.interval;

        String existance = statement.statement.toString();
        return "Statement Awaiting for " + statement.xPath + " to " + existance + " for " + timeout + " milliseconds with " + interval + " intervals";
    }
}
