package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import javax.swing.border.EmptyBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class EditorItemContainer extends EditorItem{
    private final JPanel statementContainer;

    public EditorItemContainer(CommandStatement statement){
        super(statement);
        statementContainer = new JPanel();
        //statementContainer.setLayout(new BoxLayout(statementContainer, BoxLayout.Y_AXIS));
        statementContainer.setLayout(new BoxLayout(statementContainer, BoxLayout.PAGE_AXIS));
        statementContainer.setOpaque(false);
        statementContainer.setBorder(new EmptyBorder(0,30,0,0));

        JButton addStatement = new JButton("Add");
        addStatement.setForeground(new Color(210,210,210));
        addStatement.setBackground(new Color(45,50,65));

        statementContainer.add(addStatement);

        //TEST
        addStatement.addActionListener(e -> {
            statementContainer.add(new EditorItemContainer(new CommandStatement()));
            statementContainer.revalidate();
            statementContainer.repaint();
        });

        add(statementContainer);

        for (Command c: statement.getCommands()) {
            if(c instanceof CommandStatement){
                statementContainer.add(new EditorItemContainer((CommandStatement) c));
            }
            else{
                statementContainer.add(new EditorItem(c));
            }
        }
    }
}
