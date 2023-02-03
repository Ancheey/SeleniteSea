package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class RemoveStatementDialog extends JDialog {
    private JPanel contentPane;
    private JButton buttonOK;
    private JButton buttonCancel;
    private JTextArea errorText;

    public RemoveStatementDialog(CommandStatement statement) {
        setContentPane(contentPane);
        setModal(true);
        getRootPane().setDefaultButton(buttonOK);
        contentPane.setBackground(new Color(45,50,65));
        buttonCancel.setBackground(new Color(45,50,65));
        buttonCancel.setForeground(new Color(210,210,210));
        buttonOK.setBackground(new Color(45,50,65));
        buttonOK.setForeground(new Color(210,210,210));

        errorText.setText("Are you sure you want to remove "+ statement.name + "?");
        errorText.setForeground(new Color(210,210,210));

        buttonOK.addActionListener(e -> onOK(statement));

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

    private void onOK(CommandStatement statement) {
        if(EditorStatementManager.I().getStatements().contains(statement)) {
            EditorStatementManager.I().removeStatement(statement);
            MainWindow.I().loadStatementList();
        }

        dispose();
    }

    private void onCancel() {
        dispose();
    }
}
