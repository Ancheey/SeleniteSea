package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.Dimension;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

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


        SwingUtilities.invokeLater(this::loadStatementList);

    }
    public static MainWindow I(){
        if(instance == null){
            instance = new MainWindow();
        }
        return instance;
    }


    public void clearEditor(){
        editorPanel.removeAll();
    }
    public void loadStatement(CommandStatement statement){
        clearEditor();
        statementNameLabel.setText(statement.name);
        editorPanel.add(new EditorItemContainer(statement));
        editorPanel.revalidate();
        editorPanel.repaint();
    }

    public void clearStatements(){statementsPanel.removeAll();}
    public void loadStatementList(){
        clearStatements();
        for (JButton b: EditorStatementManager.I().generateButtons(this)) {
            statementsPanel.add(b);
        }
        statementsPanel.revalidate();
        statementsPanel.repaint();
    }
}
