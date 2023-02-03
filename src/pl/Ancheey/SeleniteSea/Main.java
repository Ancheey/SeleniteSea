package pl.Ancheey.SeleniteSea;

import java.util.Arrays;
import java.util.List;

public class Main {

    static SeleniumManager engine;
    static CommandStatement program;
    public static void main(String[] args) {

        engine = new SeleniumManager("D:\\Java Projects\\SeleniteSea\\chromedriver.exe");


        program = new CommandStatement(
                Arrays.asList(
                new CommandSleep(1000),
                new CommandOpen("https://google.pl"),
                        new CommandStatementAwait(new BooleanStatementSingleXPath(engine, "//*[@id=\"L2AGLb\"]/div", BooleanStatement.SingleVar.EXISTS),
                                Arrays.asList(
                                        new CommandActionClick("//*[@id=\"L2AGLb\"]/div"),
                                        new CommandSleep(2000)
                                )
                        ),
                        new CommandVarSet("test", 0),
                        new CommandStatementWhile(new BooleanStatementDoubleVar(new EngineVarHandle(engine, "test"), BooleanStatement.DoubleVar.IS_LESS_THAN, 10),
                                List.of(
                                        new CommandStatementIf(new BooleanStatementDoubleVar(new EngineVarHandle(engine, "test"), BooleanStatement.DoubleVar.IS_DIVISIBLE_BY, 2),
                                                Arrays.asList(
                                                        new CommandOpen("https://wykop.pl"),
                                                        new CommandSleep(1000)

                                                )),
                                        new CommandStatementIf(new BooleanStatementDoubleVar(new EngineVarHandle(engine, "test"), BooleanStatement.DoubleVar.IS_NOT_DIVISIBLE_BY, 2),
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
        MainWindow window = new MainWindow();
    }
}