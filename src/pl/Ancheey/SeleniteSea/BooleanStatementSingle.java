package pl.Ancheey.SeleniteSea;

public abstract class BooleanStatementSingle extends BooleanStatement{
    SingleVar statement;

    public BooleanStatementSingle(SingleVar statement){
        this.statement = statement;
    }
}
