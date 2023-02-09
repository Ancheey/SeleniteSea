package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.Dimension;
import java.awt.Toolkit;
import java.sql.Time;
import java.time.LocalDateTime;

public class MainWindow extends JFrame{

    private static MainWindow instance;
    private JPanel MainPanel;
    private JButton saveButton;
    private JButton loadButton;
    private JButton newStatementButton;
    private JButton runButton;
    private JButton removeStatementButton;
    private JPanel editorPanel;
    private JPanel statementsPanel;
    private JLabel statementNameLabel;
    private JTextArea consoleText;
    private JScrollPane consoleScroll;

    private MainWindow(){

        setTitle("Selenite Sea");
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        double width = screenSize.getWidth();
        double height = screenSize.getHeight();

        editorPanel.setLayout(new BoxLayout(editorPanel, BoxLayout.PAGE_AXIS));
        statementsPanel.setLayout(new BoxLayout(statementsPanel, BoxLayout.Y_AXIS));

        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setContentPane(MainPanel);

        pack();
        setSize((int) (width/2), (int) (height*2/3));

        runButton.addActionListener((e)->{
            SeleniumManager.I().setProgram(EditorStatementManager.I().getCurrentlySelected());
            SeleniumManager.I().start();
        });
        newStatementButton.addActionListener((e)->{
            NewStatementDialog dialog = new NewStatementDialog();
            dialog.setVisible(true);
        });
        removeStatementButton.addActionListener((e)->{
            RemoveStatementDialog dialog = new RemoveStatementDialog(EditorStatementManager.I().getCurrentlySelected());
            dialog.setVisible(true);
        });

        saveButton.addActionListener((e)->{
            try {
                EditorStatementManager.I().saveStatements();
            }
            catch (Exception ex){
                MainWindow.I().addTextToConsole(ex + ": " + ex.getMessage());
                ex.printStackTrace();
            }
        });
        loadButton.addActionListener((e)->{
            try {
                EditorStatementManager.I().loadStatements();
            }
            catch (Exception ex){
                MainWindow.I().addTextToConsole(ex + ": " + ex.getMessage());
                ex.printStackTrace();
            }
            loadStatementList();
        });


        SwingUtilities.invokeLater(this::loadStatementList);

    }
    public static MainWindow I(){
        if(instance == null){
            instance = new MainWindow();
        }
        return instance;
    }


    /**
     * Clears the screen of the editor from all commands
     */
    public void clearEditor(){
        editorPanel.removeAll();
    }

    /**
     * Builds a set of Editor Items on the editor pane based on the provided statement
     * @param statement Command Statement to build of off
     */
    public void loadStatement(CommandStatement statement){
        clearEditor();
        statementNameLabel.setText(statement.name);
        editorPanel.add(new EditorItemContainer(statement));
        editorPanel.revalidate();
        editorPanel.repaint();
    }

    /**
     * Removes all statement buttons from the statement list
     */
    public void clearStatements(){statementsPanel.removeAll();}

    /**
     * Loads buttons for statement on the statement list
     */
    public void loadStatementList(){
        clearStatements();
        for (JButton b: EditorStatementManager.I().generateButtons()) {
            statementsPanel.add(b);
        }
        statementsPanel.revalidate();
        statementsPanel.repaint();
    }

    /**
     * Inserts a provided line of text on the debug console with a timestamp
     * @param text string of text to be displayed
     */
    public void addTextToConsole(String text){
        if(!consoleText.getText().equals("")) {
            consoleText.append("\n\n");
        }
        consoleText.append("[" + LocalDateTime.now().getHour() + ":" + LocalDateTime.now().getMinute() +":"+ LocalDateTime.now().getSecond() + "] " + text);
        consoleScroll.getVerticalScrollBar().setValue(consoleScroll.getVerticalScrollBar().getMaximum());
    }
}
