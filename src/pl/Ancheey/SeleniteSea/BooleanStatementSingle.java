package pl.Ancheey.SeleniteSea;

import org.openqa.selenium.By;
import org.openqa.selenium.InvalidSelectorException;

public class BooleanStatementSingle implements BooleanStatement{
    SingleVar statement;
    String value;

    public BooleanStatementSingle(String value, SingleVar statement){
        this.value = value;
        this.statement = statement;
    }

    /**
     * @return Statement evaluation based on variables passed on object construction
     */
    public boolean evaluate() {
        switch (statement){
            case EXISTS -> {
                if(SeleniumManager.I().getVars().containsKey(value)){
                    return true;
                }
                else{
                    try {
                        return !SeleniumManager.I().getDriver().findElements(By.xpath(value)).isEmpty();
                    }
                    catch(InvalidSelectorException e){
                        return false;
                    }
                }
            }
            case NOT_EXISTS -> {
                try {
                    return SeleniumManager.I().getDriver().findElements(By.xpath(value)).isEmpty();
                }
                catch(InvalidSelectorException e){
                    return true;
                }
            }
        }
        return false;
    }
    @Override
    public String toString() {
        return value + " " + statement.toString();
    }
}
