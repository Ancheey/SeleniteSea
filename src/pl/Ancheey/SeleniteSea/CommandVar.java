package pl.Ancheey.SeleniteSea;

public abstract class CommandVar extends Command{
    String varName;
    String value;
    public CommandVar(String varName, String value){
        this.varName = varName;
        this.value = value;
    }
    public int getValue(){
        if(SeleniumManager.I().getVars().containsKey(value)){
            return SeleniumManager.I().getVar(value);
        }
        else{
            try{
                return Integer.parseInt(value);
            }
            catch(NumberFormatException ignored){}
            return 0;
        }
    }
}
