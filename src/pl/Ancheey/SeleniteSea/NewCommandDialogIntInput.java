package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;

/**
 * Allows for the creation of a panel that lets the user input a text that will be either a number or a variable name. If neither is correct then it will return 0.
 */
public class NewCommandDialogIntInput extends NewCommandDialogInput{
    private final JTextField valueField;
    public NewCommandDialogIntInput(String name){
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
    public Integer getValue(){
        try{
            return Integer.parseInt(valueField.getText());
        }
        catch (Exception e){
            MainWindow.I().addTextToConsole("Found NaN in input, using default value.");
            return 0;
        }

    }

}
