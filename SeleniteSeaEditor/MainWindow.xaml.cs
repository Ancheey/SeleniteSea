using Microsoft.Win32;
using SeleniteSeaCore;
using SeleniteSeaCore.variables;
using SeleniteSeaEditor.controls;
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

namespace SeleniteSeaEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Debug.OnDebugMessageEvent += (status, text, caller) =>
            {
                LogConsole.AppendText($"[{status}][{DateTime.Now:HH:mm:ss}] {text}");
            };


            ProjectSelectWindow dialog = new();
            if (dialog.ShowDialog() == true)
            {
                EditorCore.WorkingDirectory = dialog.SelectedPath;
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
            }
        }
    }
}