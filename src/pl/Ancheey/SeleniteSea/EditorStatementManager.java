package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
import java.util.*;
import java.util.List;

public final class EditorStatementManager {
    private static EditorStatementManager instance;

    private List<CommandStatement> statements;
    private CommandStatement currentlySelected;


    private EditorStatementManager(){
        statements = new ArrayList<>();
    }

    public static EditorStatementManager I(){
        if(instance == null){
            instance = new EditorStatementManager();
        }
        return instance;
    }

    public List<CommandStatement> getStatements() {
        return statements;
    }

    public Collection<JButton> generateButtons(MainWindow window){
        List<JButton> list = new ArrayList<>();
        getStatements().forEach((value) ->{
            JButton button = new JButton(value.name);
            button.setBackground(new Color(45,50,65));
            button.setForeground(new Color(210,210,210));
            button.setPreferredSize(new Dimension(160,30));
            button.addActionListener((e)-> {
                window.loadStatement(value);
                currentlySelected = value;
            });
            list.add(button);
        });
        return list;
    }
    public void removeStatement(CommandStatement statement){
        statements.remove(statement);
    }
    public void addStatement(CommandStatement statement, String name){
        statement.name = name;
        statements.add(statement);
    }

    public CommandStatement getCurrentlySelected() {
        return currentlySelected;
    }
}
