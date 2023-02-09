package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.lang.reflect.Constructor;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class NewCommandDialog extends JDialog {

    CommandStatement executor;
    private JPanel contentPane;
    private JButton buttonOK;
    private JButton buttonCancel;
    private JComboBox<Class<? extends Command>> typeBox;

    private List<NewCommandDialogInput> inputs = new ArrayList<>();

    private int savedOffsetFromBottom = 0;
    private Command edited;
    private JPanel varPanel;

    public NewCommandDialog(CommandStatement executor) {
        this.executor = executor;

        build();
    }
    public NewCommandDialog(Command edited) {
        this.edited = edited;
        this.executor = (CommandStatement) edited.getParent();
        savedOffsetFromBottom = executor.getCommands().size() - 2 - Arrays.asList(executor.getCommands()).indexOf(edited);

        build();
    }

    private void build(){
        setContentPane(contentPane);
        setModal(true);
        getRootPane().setDefaultButton(buttonOK);
        contentPane.setBackground(new Color(45,50,65));

        buttonOK.addActionListener(e -> onOK());

        buttonCancel.addActionListener(e -> onCancel());

        varPanel.setLayout(new BoxLayout(varPanel,BoxLayout.PAGE_AXIS));



        // call onCancel() when cross is clicked
        setDefaultCloseOperation(DO_NOTHING_ON_CLOSE);
        addWindowListener(new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                onCancel();
            }
        });

        // call onCancel() on ESCAPE
        contentPane.registerKeyboardAction(e -> onCancel(), KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0), JComponent.WHEN_ANCESTOR_OF_FOCUSED_COMPONENT);

        for (Class<? extends Command> c: CommandRegistry.I().getRegistry()) {
            typeBox.addItem(c);
        }

        //Creates the filling for varPanel depending on the first constructor of the class that was selected
        typeBox.addActionListener((e) ->{
            if(typeBox.getSelectedIndex() == -1)
                return;

            //Get the first constructor of the class that is selected
            Constructor<?> constructor = ((Class<? extends Command>)typeBox.getSelectedItem()).getConstructors()[0];
            Object[] params = constructor.getParameterTypes();
            varPanel.removeAll();
            inputs.clear();

            //for each param that is needed, add a new input.
            //THIS PART CAN BE EXTENDED
            //NEEDS REMAKING SO THE IF ELSE IS REPLACED BY A MANAGER OF CLASS - INPUT PAIRS
            for (Object o: params) {
                if(o == BooleanStatement.class){
                    inputs.add(new NewCommandDialogBooleanInput());
                }else if(o == int.class){
                    inputs.add(new NewCommandDialogIntInput("Integer"));
                } else if (o == CommandVarModify.Action.class) {
                    inputs.add(new NewCommandDialogActionInput());
                } else{
                    inputs.add(new NewCommandDialogStringInput("Text"));
                }
            }
            for(NewCommandDialogInput c : inputs){
                varPanel.add(c);
            }
            SwingUtilities.invokeLater(()->{
                varPanel.revalidate();
                varPanel.repaint();
            });
        });

        pack();
    }

    /**
     * Oh boy.
     * This part is an absolute clusterf*ck because it breaks every OOP rule in existence.
     * It takes the class that is selected in the combo box, finds its constructor and tries to build a new instance of the object with the variables provided in inputs
     * It technically should never break, but it doesn't make it less of a sticks-and-tape construction
     */
    private void onOK() {
        if(typeBox.getSelectedIndex() == -1)
            return;

        //Get the constructor of selected class
        Constructor<?> constructor = ((Class<? extends Command>)typeBox.getSelectedItem()).getConstructors()[0];
        try { //here the fun begins

            //This one takes all values contained in the inputs and mashes them into a list. It doesn't know what type of variables these are thoguh
            List<Object> params = new ArrayList<>();
            for (NewCommandDialogInput c: inputs) {
                params.add(c.getValue());
            }

            //With all the mashed variables it creates a new instance
            Command c = (Command) constructor.newInstance(params.toArray());


            //If the creation is a glorified edit of a command, it will try to put commands back inside the statement if it was one to begin with
            //It also removes the old one and replaces it with a new command
            if(savedOffsetFromBottom == 0) {
                executor.add(c);
            }
            else{
                executor.add(executor.getCommands().size() - savedOffsetFromBottom,c);
            }
            if(edited != null){
                executor.remove(edited);
                try {
                    CommandStatement st = (CommandStatement) c;
                    st.add(((CommandStatement)edited).getCommands());
                }
                catch(Exception ignored){}//expected behavior, because sometimes the command is not a statement
            }


            MainWindow.I().loadStatement(EditorStatementManager.I().getCurrentlySelected());
        }
        catch (Exception e){
            MainWindow.I().addTextToConsole(e+ ":  " + e.getMessage());
        }
        dispose();
    }

    private void onCancel() {
        dispose();
    }

}
