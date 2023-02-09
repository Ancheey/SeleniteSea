package pl.Ancheey.SeleniteSea;

import javax.swing.*;

/**
 * Base class for boolean dialog inputs in the new command dialog window
 */
public abstract class BooleanDialogInput extends JPanel {
    BooleanDialogInput(){
        super();
    }
    public abstract BooleanStatement getStatement();
}
