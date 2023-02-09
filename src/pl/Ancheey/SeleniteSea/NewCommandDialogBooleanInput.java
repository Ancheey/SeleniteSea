package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;

/**
 * Allows for a creation of a choice box that lets the user choose if they want a Single or a Double parameter boolean. Adds said dialog to self upon choice
 */
public class NewCommandDialogBooleanInput extends NewCommandDialogInput {
    BooleanDialogInput input;

    public NewCommandDialogBooleanInput() {
        super();
        setOpaque(false);
        setLayout(new FlowLayout(FlowLayout.LEFT));

        JComboBox<String> typeCombo = new JComboBox<>();
        typeCombo.addItem("Single");
        typeCombo.addItem("Double");

        typeCombo.addActionListener((e) -> {
            if (typeCombo.getSelectedIndex() == -1)
                return;
            if (input != null)
                remove(input);

            if (typeCombo.getSelectedItem().toString().equals("Single")) {
                input = new NewCommandDialogBooleanSingleInput();
            } else {
                input = new NewCommandDialogBooleanDoubleInput();
            }
            add(input);
            revalidate();
            repaint();
        });
        add(typeCombo);

    }

    @Override
    public BooleanStatement getValue() {
        return input.getStatement();
    }
}
