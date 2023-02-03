package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;

public class EditorItem extends JPanel {
    private JPanel dataContainer;
    private JLabel itemName;
    private JLabel itemDescription;
    private JButton itemEdit;
    private JButton itemDelete;

    private Command boundCommand;


    public EditorItem(Command command){
        super();

        setLayout(new BorderLayout());
        setBorder(new LineBorder(new Color(210,210,210)));
        setOpaque(false);

        //Create dataContainer
        dataContainer = new JPanel();
        dataContainer.setLayout(new GridLayout(1,4));
        dataContainer.setOpaque(false);

        itemName = new JLabel("Command");
        itemName.setForeground(new Color(210,210,210));
        itemName.setFont(new Font("JetBrains Mono", Font.BOLD, 14));
        dataContainer.add(itemName);

        itemDescription = new JLabel("Description");
        itemDescription.setForeground(new Color(210,210,210));
        itemDescription.setFont(new Font("JetBrains Mono", Font.PLAIN, 10));
        dataContainer.add(itemDescription);

        itemEdit = new JButton("Edit");
        itemEdit.setForeground(new Color(210,210,210));
        itemEdit.setBackground(new Color(45,50,65));
        dataContainer.add(itemEdit);

        //GridBagConstraints itemDeleteConstraints = new GridBagConstraints();
        //itemDeleteConstraints.anchor = GridBagConstraints.FIRST_LINE_END;
        itemDelete = new JButton("Delete");
        itemDelete.setForeground(new Color(210,210,210));
        itemDelete.setBackground(new Color(150,20,20));
        dataContainer.add(itemDelete);

        itemDelete.addActionListener((e) ->{
           if(boundCommand instanceof CommandStatement && !((CommandStatement) boundCommand).getCommands().isEmpty()) {
               return;
           }

            ((CommandStatement)boundCommand.getParent()).remove(boundCommand);

            var parent = this.getParent();
            parent.remove(this);
            parent.revalidate();
            parent.repaint();
        });

        add(dataContainer,BorderLayout.PAGE_START);

        setBoundCommand(command);
    }

    public Command getBoundCommand() {
        return boundCommand;
    }

    public void setBoundCommand(Command boundCommand) {
        this.boundCommand = boundCommand;
        itemName.setText(boundCommand.getClass().getSimpleName());
        itemDescription.setText(boundCommand.getDescription());
    }
}
