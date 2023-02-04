package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.By;

public class BooleanStatementSingle implements BooleanStatement{
    SingleVar statement;
    Object value;

    public BooleanStatementSingle(Object value, SingleVar statement){
        this.value = value;
        this.statement = statement;
    }

    public boolean evaluate() {
        if(value.getClass() == String.class){
            switch (statement){
                case EXISTS -> {
                    if(SeleniumManager.I().getVars().containsKey((String) value)){
                        return true;
                    }
                    else{
                        return !SeleniumManager.I().getDriver().findElements(By.xpath((String) value)).isEmpty();
                    }
                }
                case NOT_EXISTS -> {
                    return SeleniumManager.I().getDriver().findElements(By.xpath((String) value)).isEmpty();
                }
            }
        }
        return statement == SingleVar.EXISTS;
    }
    @Override
    public String toString() {
        return value.toString() + " " + statement.toString() + " " + value.toString();
    }
}
