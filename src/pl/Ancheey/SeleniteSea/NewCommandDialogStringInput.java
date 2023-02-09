package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
/**
 * Allows for the creation of a panel that lets the user input a simple string to be processed later
 */
public class NewCommandDialogStringInput extends NewCommandDialogInput{
    private final JTextField valueField;
    public NewCommandDialogStringInput(String name){
        super();
        setOpaque(false);

        setLayout(new FlowLayout(FlowLayout.LEFT));

        JLabel nameLabel = new JLabel(name);
        nameLabel.setForeground(new Color(210,210,210));

        valueField = new JTextField();
        valueField.setPreferredSize(new Dimension(200,30));
        valueField.setOpaque(false);
        valueField.setForeground(new Color(210,210,210));

        add(nameLabel);
        add(valueField);
    }
    public String getValue(){
        return valueField.getText();
    }

}
