package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

/**
 * All of this just to pass a new name to the Statement manager
 * SMH
 */
public class NewStatementDialog extends JDialog {
    private JPanel contentPane;
    private JButton buttonOK;
    private JButton buttonCancel;
    private JTextField statementName;

    public NewStatementDialog() {
        setContentPane(contentPane);
        setModal(true);
        getRootPane().setDefaultButton(buttonOK);
        contentPane.setBackground(new Color(45,50,65));
        buttonCancel.setBackground(new Color(45,50,65));
        buttonCancel.setForeground(new Color(210,210,210));
        buttonOK.setBackground(new Color(45,50,65));
        buttonOK.setForeground(new Color(210,210,210));
        statementName.setBackground(new Color(30,35,50));
        statementName.setForeground(new Color(210,210,210));

        buttonOK.addActionListener(e -> onOK());

        buttonCancel.addActionListener(e -> onCancel());

        // call onCancel() when cross is clicked
        setDefaultCloseOperation(DO_NOTHING_ON_CLOSE);
        addWindowListener(new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                onCancel();
            }
        });

        // call onCancel() on ESCAPE
        contentPane.registerKeyboardAction(e -> onCancel(), KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0), JComponent.WHEN_ANCESTOR_OF_FOCUSED_COMPONENT);
        pack();
    }

    private void onOK() {
        // add your code here
        if(statementName.getText().equals(""))
            return;

        EditorStatementManager.I().addStatement(new CommandStatement(), statementName.getText());
        MainWindow.I().loadStatementList();
        dispose();
    }

    private void onCancel() {
        dispose();
    }
}
