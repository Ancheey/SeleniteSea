package pl.Ancheey.SeleniteSea;

import java.io.File;

public class Main {

    /**
     * BEFORE YOU TRY TO RUN THIS PROGRAM MAKE SURE YOU HAVE A CHROMEDRIVER.EXE PLACED WITHIN THE SAME FOLDER AS THIS JAR
     * IF NOT THEN ALL HELL IS UPON US
     * @param args don't know, don't care
     */
    public static void main(String[] args) {

        try {
            String path = new File("").getAbsolutePath();
            System.setProperty("webdriver.chrome.driver",path + "\\chromedriver.exe");
        }
        catch(Exception e){
            e.printStackTrace();
        }


        //Class registration
        CommandRegistry.I().register(CommandActionClick.class);
        CommandRegistry.I().register(CommandOpen.class);
        CommandRegistry.I().register(CommandSleep.class);
        CommandRegistry.I().register(CommandStatementAwait.class);
        CommandRegistry.I().register(CommandStatementIf.class);
        CommandRegistry.I().register(CommandStatementWhile.class);
        CommandRegistry.I().register(CommandVarSet.class);
        CommandRegistry.I().register(CommandVarModify.class);
        CommandRegistry.I().register(CommandPrint.class);

        //Magic
        MainWindow.I().setVisible(true);
    }
}