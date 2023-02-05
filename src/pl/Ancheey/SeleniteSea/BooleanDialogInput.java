package pl.Ancheey.SeleniteSea;

import javax.swing.*;

public abstract class BooleanDialogInput extends JPanel {
    BooleanDialogInput(){
        super();
    }
    public abstract BooleanStatement getStatement();
}
