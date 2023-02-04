package pl.Ancheey.SeleniteSea;

import java.util.Arrays;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        CommandStatement program = new CommandStatement(
                Arrays.asList(
                new CommandSleep(1000),
                new CommandOpen("https://google.pl"),
                        new CommandStatementAwait(new BooleanStatementSingle("//*[@id=\"L2AGLb\"]/div", BooleanStatement.SingleVar.EXISTS),
                                Arrays.asList(
                                        new CommandActionClick("//*[@id=\"L2AGLb\"]/div"),
                                        new CommandSleep(6000)
                                )
                        ),
                        new CommandVarSet("test", 0),
                        new CommandStatementWhile(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.IS_LESS_THAN, 10),
                                List.of(
                                        new CommandStatementIf(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.IS_DIVISIBLE_BY, 2),
                                                Arrays.asList(
                                                        new CommandOpen("https://wykop.pl"),
                                                        new CommandSleep(1000)

                                                )),
                                        new CommandStatementIf(new BooleanStatementDouble("test", BooleanStatement.DoubleVar.IS_NOT_DIVISIBLE_BY, 2),
                                                Arrays.asList(
                                                        new CommandOpen("https://reddit.com"),
                                                        new CommandSleep(1000)
                                                )
                                        ),
                                        new CommandVarModify("test", 1, CommandVarModify.Action.INCREMENT)
                                )
                        )
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


        MainWindow.I().setVisible(true);
    }
}