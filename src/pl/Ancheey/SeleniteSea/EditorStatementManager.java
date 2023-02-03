package pl.Ancheey.SeleniteSea;

import java.util.HashMap;
import java.util.Map;

public final class EditorStatementManager {
    private static EditorStatementManager instance;

    private Map<String, CommandStatement> statements;


    private EditorStatementManager(){
        statements = new HashMap<>();
    }

    public static EditorStatementManager I(){
        if(instance == null){
            instance = new EditorStatementManager();
        }
        return instance;
    }

    public Map<String, CommandStatement> getStatements() {
        return statements;
    }
}
