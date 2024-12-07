using Microsoft.Win32;
using SeleniteSeaCore;
using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.variables;
using SeleniteSeaEditor.controls;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace SeleniteSeaEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            new HelloWindow().ShowDialog();


            InitializeComponent();

            Debug.OnDebugMessageEvent += (status, text, caller) =>
            {
                LogConsole.AppendText($"[{status}][{DateTime.Now:HH:mm:ss}] {text}");
            };

            ProjectSelectWindow dialog = new();
            if (dialog.ShowDialog() == true)
            {
                EditorCore.WorkingDirectory = dialog.SelectedPath;
                try
                {
                    LoadProjectDirectory(dialog.SelectedPath);
                }
                catch(Exception e)
                {
                    LogConsole.AppendText($"[ERROR][{DateTime.Now:HH:mm:ss}] {e}");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScopeBox.Children.Add(EditorCore.NewProject());
        }

        private void ProjectSelectButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectSelectWindow dialog = new();
            if(dialog.ShowDialog() == true)
            {
                EditorCore.WorkingDirectory = dialog.SelectedPath;
                LoadProjectDirectory(dialog.SelectedPath);
            }
        }
        public void LoadProjectDirectory(string dir)
        {
            Fileview.Children.Clear();
            EditorCore.LoadWorkingDirectory(dir);
            foreach(var block in EditorCore.LoadedFunctions)
            {
                var label = new Label()
                {
                    Content = block.Key.Title,
                    Cursor = Cursors.Hand,
                    Foreground = Brushes.LightGray
                };
                if (typeof(SSBlockScopeFunction).IsAssignableFrom(block.Key.GetType()))
                {
                    label.ToolTip = ((SSBlockScopeFunction)block.Key).Description;
                }
                label.MouseDoubleClick += (o, e) =>
                {
                    ScopeBox.Children.Clear();
                    ScopeBox.Children.Add(EditorCore.GetFunctionVisual(block.Key));
                };
                Fileview.Children.Add(label);
            }
        }
    }
}