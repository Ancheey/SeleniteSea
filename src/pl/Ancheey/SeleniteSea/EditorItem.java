package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;

/**
 * Base for all command containers in the visual context
 */
public class EditorItem extends JPanel {
    private JPanel dataContainer;
    private JLabel itemName;
    private JLabel itemDescription;
    private JButton itemEdit;
    private JButton itemDelete;

    private Command boundCommand;


    /**
     * Creates a new command item panel to be put on the UI
     * @param command Command to base this item on
     */
    public EditorItem(Command command){
        super();

        setLayout(new BorderLayout());
        setBorder(new LineBorder(new Color(210,210,210)));
        setOpaque(false);

        //Create dataContainer
        dataContainer = new JPanel();
        dataContainer.setLayout(new GridLayout(2,1));
        dataContainer.setOpaque(false);

        JPanel textPanel = new JPanel();
        textPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
        textPanel.setOpaque(false);

        itemName = new JLabel("Command");
        itemName.setForeground(new Color(210,210,210));
        itemName.setFont(new Font("JetBrains Mono", Font.BOLD, 14));
        itemName.setPreferredSize(new Dimension(200,30));
        textPanel.add(itemName);

        itemDescription = new JLabel("Description");
        itemDescription.setForeground(new Color(210,210,210));
        itemDescription.setFont(new Font("JetBrains Mono", Font.PLAIN, 10));
        textPanel.add(itemDescription);

        JPanel buttonPanel = new JPanel();
        buttonPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
        buttonPanel.setOpaque(false);

        itemEdit = new JButton("Edit");
        itemEdit.setFont(new Font("JetBrains Mono", Font.PLAIN, 8));
        itemEdit.setForeground(new Color(210,210,210));
        itemEdit.setBackground(new Color(45,50,65));
        itemEdit.setPreferredSize(new Dimension(60,20));
        buttonPanel.add(itemEdit);

        //GridBagConstraints itemDeleteConstraints = new GridBagConstraints();
        //itemDeleteConstraints.anchor = GridBagConstraints.FIRST_LINE_END;
        itemDelete = new JButton("Delete");
        itemDelete.setFont(new Font("JetBrains Mono", Font.PLAIN, 8));
        itemDelete.setForeground(new Color(210,210,210));
        itemDelete.setBackground(new Color(150,20,20));
        itemDelete.setPreferredSize(new Dimension(60,20));
        buttonPanel.add(itemDelete);

        dataContainer.add(textPanel);
        dataContainer.add(buttonPanel);

        //Deletes itself upon execution and tells the parent to remove the child
        itemDelete.addActionListener((e) ->{
           if(boundCommand instanceof CommandStatement && !((CommandStatement) boundCommand).getCommands().isEmpty()) {
               return;
           }
            if(boundCommand.getParent() == null){
                return;
            }

            ((CommandStatement)boundCommand.getParent()).remove(boundCommand);

            var parent = this.getParent();
            parent.remove(this);
            parent.revalidate();
            parent.repaint();
        });

        //Uses the new item dialog to overwrite this item. Simulates editing
        itemEdit.addActionListener((e) ->{
            if(boundCommand.getParent() == null){
                return;
            }

            NewCommandDialog ncd = new NewCommandDialog(boundCommand);
            ncd.setVisible(true);
        });

        add(dataContainer,BorderLayout.PAGE_START);

        setBoundCommand(command);
    }

    public Command getBoundCommand() {
        return boundCommand;
    }

    /**
     * Binds a command to this item, setting the text on the item
     * @param boundCommand command to bind
     */
    private void setBoundCommand(Command boundCommand) {
        this.boundCommand = boundCommand;
        itemName.setText(boundCommand.getClass().getSimpleName());
        itemDescription.setText(boundCommand.getDescription());
    }
}
