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

            //Get the first constructor
            Constructor<?> constructor = ((Class<? extends Command>)typeBox.getSelectedItem()).getConstructors()[0];
            Object[] params = constructor.getParameterTypes();
            varPanel.removeAll();
            inputs.clear();

            //for each param that is needed, add a boolean or a string input
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

    private void onOK() {
        if(typeBox.getSelectedIndex() == -1)
            return;
        Constructor<?> constructor = ((Class<? extends Command>)typeBox.getSelectedItem()).getConstructors()[0];
        try {
            List<Object> params = new ArrayList<>();
            for (NewCommandDialogInput c: inputs) {
                params.add(c.getValue());
            }
            Command c = (Command) constructor.newInstance(params.toArray());

            if(savedOffsetFromBottom == 0) {
                executor.add(c);
            }
            else{
                executor.add(executor.getCommands().size() - savedOffsetFromBottom,c);
            }
            if(edited != null){
                executor.remove(edited);
            }
            try {
                CommandStatement st = (CommandStatement) c;
                st.add(((CommandStatement)edited).getCommands());
            }
            catch(Exception e){
                MainWindow.I().addTextToConsole(e+ ":  " + e.getMessage());
                MainWindow.I().addTextToConsole("Command Statement was changed to a simple Command, loosing all its contents");
            }


            MainWindow.I().loadStatement(EditorStatementManager.I().getCurrentlySelected());
        }
        catch (Exception e){
            MainWindow.I().addTextToConsole(e+ ":  " + e.getMessage());
        }
        dispose();
    }

    private void onCancel() {
        // add your code here if necessary
        dispose();
    }

}
