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

    /**
     * Singleton declaration for a UI command statement item manager
     * @return New or existing manager instance
     */
    public static EditorStatementManager I(){
        if(instance == null){
            instance = new EditorStatementManager();
        }
        return instance;
    }

    public List<CommandStatement> getStatements() {
        return statements;
    }

    /**
     *Generates a set of buttons that correspond to statements and allow for changing the open one
     * @return
     */
    public Collection<JButton> generateButtons(){
        List<JButton> list = new ArrayList<>();
        getStatements().forEach((value) ->{
            JButton button = new JButton(value.name);
            button.setBackground(new Color(45,50,65));
            button.setForeground(new Color(210,210,210));
            button.setPreferredSize(new Dimension(160,30));
            button.addActionListener((e)-> {
                MainWindow.I().loadStatement(value);
                currentlySelected = value;
            });
            list.add(button);
        });
        return list;
    }

    /**
     * Removes a command statement from the list
     * DOESN'T DELETE THE FILE. ONLY ALLOWS FOR CREATION OF A NEW STATEMENT WITH THE SAME NAME TO OVERWRITE THE OTHER ONE
     * @param statement statement to remove from the list
     */
    public void removeStatement(CommandStatement statement){
        statements.remove(statement);
    }

    /**
     * Adds a statement and allows it to have a name
     * @param statement statement to add
     * @param name displayed name to bind to the statement
     */
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

    /**
     * Saves statements to $dir\statements.
     * Statements are saved as a .selenite file, but are just a serialized object able to be read by any program
     * @throws IOException If something goes wrong with the output stream, this will be thrown
     */
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

    /**
     * Loads statements saved in the &dir\statements folder to the manager
     */
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

    /**
     * @return Currently selected statement
     */
    public CommandStatement getCurrentlySelected() {
        return currentlySelected;
    }
}
