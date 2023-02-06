package pl.Ancheey.SeleniteSea;



import javax.swing.*;
import java.awt.*;
import javax.xml.*;
import java.io.*;
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
        for(CommandStatement cs : statements){
            if(cs.name.equals(name)){
                MainWindow.I().addTextToConsole("Statement \"" + name + "\" already exists");
                return;
            }
        }
        statement.name = name;
        statements.add(statement);
    }
    public void saveStatements() throws IOException {
        FileOutputStream fos;
        ObjectOutputStream oos;
        File file;
        new File("statements").mkdir();
        for (CommandStatement c: statements) {
            file = new File("statements", c.name + ".selenite");
            file.createNewFile();
            fos = new FileOutputStream(file.getPath());
            oos = new ObjectOutputStream(fos);
            oos.writeObject(c);
            oos.close();
        }
        MainWindow.I().addTextToConsole("Successfully saved all statements!");
    }
    public  void loadStatements(){
        File folder = new File("statements");
        FileInputStream fis;
        ObjectInputStream ois;
        if(!folder.exists())
            return;

        statements.clear();
        for(final File file : folder.listFiles()) {
            try {
                fis = new FileInputStream(file);
                ois = new ObjectInputStream(fis);
                CommandStatement statement = (CommandStatement) ois.readObject();
                addStatement(statement, statement.name);
            } catch (Exception e) {
                MainWindow.I().addTextToConsole("Tried to load " + file.getName() + " but the file is either corrupted or not a selenite object");
            }
        }
    }

    public CommandStatement getCurrentlySelected() {
        return currentlySelected;
    }
}
