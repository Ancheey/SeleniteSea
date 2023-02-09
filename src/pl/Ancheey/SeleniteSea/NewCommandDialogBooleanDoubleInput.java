package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;

/**
 * Allows for the creation of a panel that lets the user choose a bi-variable boolean and the method of their evaluation
 */
public class NewCommandDialogBooleanDoubleInput extends BooleanDialogInput{
    private JTextField valueField;
    private JComboBox<BooleanStatement.DoubleVar> booleanCombo;
    private JTextField value2Field;
    public NewCommandDialogBooleanDoubleInput(){
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

        booleanCombo = new JComboBox<>(BooleanStatement.DoubleVar.values());
        booleanCombo.setSelectedIndex(0);

        value2Field = new JTextField();
        value2Field.setOpaque(false);
        value2Field.setForeground(new Color(210,210,210));
        value2Field.setPreferredSize(new Dimension(50,30));

        add(nameLabel);
        add(valueField);
        add(booleanCombo);
        add(value2Field);
    }

    @Override
    public BooleanStatement getStatement() {
        return new BooleanStatementDouble(valueField.getText(),(BooleanStatement.DoubleVar) booleanCombo.getSelectedItem(),value2Field.getText());
    }
}
