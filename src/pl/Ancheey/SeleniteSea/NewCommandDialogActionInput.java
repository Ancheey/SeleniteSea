package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;

/**
 * Allows the creation of a panel that enables the selection of a math action for variables
 */
public class NewCommandDialogActionInput extends NewCommandDialogInput {
    JComboBox<CommandVarModify.Action> actionCombo;
    public NewCommandDialogActionInput() {
        super();
        setOpaque(false);
        setLayout(new FlowLayout(FlowLayout.LEFT));

         actionCombo = new JComboBox<>(CommandVarModify.Action.values());
         actionCombo.setSelectedIndex(0);

        add(actionCombo);

    }

    @Override
    public CommandVarModify.Action getValue() {
        return (CommandVarModify.Action) actionCombo.getSelectedItem();
    }
}
