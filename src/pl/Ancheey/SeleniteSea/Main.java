package pl.Ancheey.SeleniteSea;

import java.util.Arrays;
import java.util.List;

public class Main {

    public static void main(String[] args) {

        CommandStatementIf if1 = new CommandStatementIf(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.IS_DIVISIBLE_BY, 2));
        if1.add( Arrays.asList(
                new CommandOpen("https://wykop.pl"),
                new CommandSleep(1000)

        ));
        CommandStatementIf if2 = new CommandStatementIf(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.IS_NOT_DIVISIBLE_BY, 2));
        if1.add( Arrays.asList(
                new CommandOpen("https://reddit.com"),
                new CommandSleep(1000)

        ));
        CommandStatementAwait await =  new CommandStatementAwait(new BooleanStatementSingle("//*[@id=\"L2AGLb\"]/div", BooleanStatement.SingleVar.EXISTS), 1000, 100);
        await.add(Arrays.asList(
                new CommandActionClick("//*[@id=\"L2AGLb\"]/div"),
                new CommandSleep(6000)
        ));

        CommandStatementWhile loop = new CommandStatementWhile(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.IS_LESS_THAN, 10));
        loop.add(
                List.of(
                        if1,
                        if2
                )
        );

        CommandStatement program = new CommandStatement(
                Arrays.asList(
                new CommandSleep(1000),
                new CommandOpen("https://google.pl"),
                        await,
                        new CommandVarSet("test", "0"),
                        loop
                )
        );


        //engine.setProgram(program);
        //engine.start();

        EditorStatementManager.I().addStatement(program, "Main");
        EditorStatementManager.I().addStatement(new CommandStatement(), "Test2");

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


        MainWindow.I().setVisible(true);
    }
}