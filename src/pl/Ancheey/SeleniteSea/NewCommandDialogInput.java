package pl.Ancheey.SeleniteSea;

import javax.swing.*;

/**
 * Base for all panels that are to be placed in the new command dialog
 */
public abstract class NewCommandDialogInput extends JPanel {

    public NewCommandDialogInput(){
        super();
    }
    public abstract Object getValue();
}
