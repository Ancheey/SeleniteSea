package pl.Ancheey.SeleniteSea;

import java.util.Collection;

/**
 * Command statement containing commands, but will execute only it is able to evaluate the provided boolean to true within a set amount of time
 */
public class CommandStatementAwait extends CommandStatement{
    BooleanStatement statement;
    int timeout;
    int interval;
    int DEFAULT_TIMEOUT = 1500;
    int DEFAULT_INTERVAL = 100;
    @Deprecated
    private CommandStatementAwait(BooleanStatement statement){
        this.statement = statement;
    }
    @Deprecated
    private CommandStatementAwait(BooleanStatement statement, int timeout){
        this.statement = statement;
        this.timeout = timeout;
    }

    /**
     * Creates a new command statement that is able to contain other commands. Will execute only if statement is true within the provided timeout
     * @param statement statement to evaluate
     * @param timeout time to wait for an object to exist. Default: 1500ms
     * @param interval time in between checks. Default: 100ms
     */
    public CommandStatementAwait(BooleanStatement statement, int timeout, int interval){
        this.statement = statement;
        this.timeout = timeout == 0 ? DEFAULT_TIMEOUT : timeout;
        this.interval = interval == 0 ? DEFAULT_INTERVAL : interval;
    }
    @Deprecated
    private CommandStatementAwait(BooleanStatement statement, Collection<Command> commands){
        this.statement = statement;
        add(commands);
    }
    @Deprecated
    private CommandStatementAwait(BooleanStatement statement, int timeout, Collection<Command> commands){
        this.statement = statement;
        this.timeout = timeout;
        add(commands);
    }
    @Deprecated
    private CommandStatementAwait(BooleanStatement statement, int timeout, int interval, Collection<Command> commands){
        this.statement = statement;
        this.timeout = timeout;
        this.interval = interval;
        add(commands);
    }

    /**
     * Evaluates the statement every set "interval" for "timeout" set of time. if the statement is true within this time, it will execute its contents
     */
    @Override
    public void execute() {

        int timeout = this.timeout == 0 ? DEFAULT_TIMEOUT : this.timeout;


        int interval = this.interval == 0 ? DEFAULT_INTERVAL : this.interval;
        try {
            while (timeout > 0 && !statement.evaluate()) {
                if(timeout > interval){
                    SeleniumManager.I().sleep(interval);
                    timeout -= interval;
                }else{
                    SeleniumManager.I().sleep(timeout);
                    timeout = 0;
                }
            }
            if(statement.evaluate()) {
                super.execute();
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

        String existence = statement.toString();
        return "Statement Awaiting for " + existence + " for " + timeout + " milliseconds with " + interval + " intervals";
    }
}
