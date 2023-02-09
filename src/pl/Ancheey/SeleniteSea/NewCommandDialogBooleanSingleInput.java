package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
/**
 * Allows for the creation of a panel that lets the user choose a single-variable boolean and the method of its evaluation
 */
public class NewCommandDialogBooleanSingleInput extends BooleanDialogInput {
    private JTextField valueField;
    private JComboBox<BooleanStatement.SingleVar> booleanCombo;
    public NewCommandDialogBooleanSingleInput(){
        super();
        build();
    }
    private void build(){

        setOpaque(false);

        setLayout(new FlowLayout(FlowLayout.LEFT));
        setOpaque(false);

        JLabel nameLabel = new JLabel("If");
        nameLabel.setForeground(new Color(210,210,210));


        valueField = new JTextField();
        valueField.setOpaque(false);
        valueField.setForeground(new Color(210,210,210));
        valueField.setPreferredSize(new Dimension(50,30));

        booleanCombo = new JComboBox<>(BooleanStatement.SingleVar.values());
        booleanCombo.setSelectedIndex(0);

        add(nameLabel);
        add(valueField);
        add(booleanCombo);
    }

    @Override
    public BooleanStatement getStatement() {
        return new BooleanStatementSingle(valueField.getText(),(BooleanStatement.SingleVar) booleanCombo.getSelectedItem());
    }
}
