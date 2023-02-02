package pl.Ancheey.SeleniteSea;

import javax.swing.*;
import java.awt.*;

public class MainWindow extends JFrame{
    private JPanel MainPanel;
    private JButton saveButton;
    private JButton loadButton;
    private JButton newStatementButton;
    private JButton runButton;
    private JButton stopButton;
    private JButton removeStatementButton;

    public MainWindow(){
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        double width = screenSize.getWidth();
        double height = screenSize.getHeight();


        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setContentPane(MainPanel);

        pack();
        setSize((int) (width/2), (int) (height*2/3));
        setVisible(true);
    }
}
