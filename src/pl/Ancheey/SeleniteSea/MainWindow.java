package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class MainWindow extends JFrame{
    private JPanel MainPanel;
    private JButton saveButton;
    private JButton loadButton;
    private JButton newStatementButton;
    private JButton runButton;
    private JButton stopButton;
    private JButton removeStatementButton;
    private JPanel editorPanel;

    public MainWindow(){
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        double width = screenSize.getWidth();
        double height = screenSize.getHeight();

        editorPanel.setLayout(new BoxLayout(editorPanel, BoxLayout.PAGE_AXIS));

        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setContentPane(MainPanel);

        pack();
        setSize((int) (width/2), (int) (height*2/3));
        setVisible(true);
        loadButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                loadStatement(Main.program);
                revalidate();
                repaint();
            }
        });
    }
    public void clearEditor(){
        editorPanel.removeAll();
    }
    public void loadStatement(CommandStatement statement){
        editorPanel.add(new EditorItemContainer(statement));
    }
}
