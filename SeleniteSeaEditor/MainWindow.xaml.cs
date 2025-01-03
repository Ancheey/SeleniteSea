using Microsoft.Win32;
using SeleniteSeaCore;
using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.variables;
using SeleniteSeaEditor.controls;
using SeleniteSeaEditor.controls.Displays;
using SeleniteSeaEditor.modding;
using SeleniteSeaExecutor;
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
                Dispatcher.Invoke(() =>
                {
                    LogConsole.AppendText($"\r[{status}][{DateTime.Now:HH:mm:ss}] {text}");
                    LogConsole.ScrollToEnd();
                });
            };

            ModHandler.LoadMods();
            if (ModHandler.LoadedMods.Count == 0)
                ModLister.Visibility = Visibility.Collapsed;


            ProjectSelectWindow dialog = new();
            if (dialog.ShowDialog() == true)
            {
                ExeCore.WorkingDirectory = dialog.SelectedPath;
                try
                {
                    LoadProjectDirectory(dialog.SelectedPath);
                }
                catch(Exception e)
                {
                    LogConsole.AppendText($"\r[ERROR][{DateTime.Now:HH:mm:ss}] {e}");
                    LogConsole.ScrollToEnd();
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
                ExeCore.WorkingDirectory = dialog.SelectedPath;
                LoadProjectDirectory(dialog.SelectedPath);
            }
        }
        public void LoadProjectDirectory(string dir)
        {
            void BlockCreation(SSBlock block)
            {
                var label = new Label()
                {
                    Content = block.Title,
                    Cursor = Cursors.Hand,
                    Foreground = Brushes.LightGray
                };
                if (typeof(SSBlockScopeFunction).IsAssignableFrom(block.GetType()))
                {
                    label.ToolTip = ((SSBlockScopeFunction)block).Description;
                }
                label.MouseDoubleClick += (o, e) =>
                {
                    ScopeBox.Children.Clear();
                    ScopeBox.Children.Add(EditorCore.GetFunctionVisual(block));
                    EditorCore.CurrentBlock = block;
                    SaveButton.IsEnabled = true;
                };
                Fileview.Children.Add(label);
            }

            Fileview.Children.Clear();
            EditorCore.LoadWorkingDirectory(dir);
            ProjectName.Content = dir.Split("\\").Last();

            var add = new Label()
            {
                Content = "+",
                Cursor = Cursors.Hand,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
            };
            add.MouseLeftButtonUp += (s, e) =>
            {
                var block = new SSBlockScopeFunction();
                var db = new DisplaySSBlockScopeFunction(block);
                if (!EditorCore.TryOpenEditor(block, out bool edited) || !edited)
                    return;
                db.Refresh();
                ScopeBox.Children.Clear();
                ScopeBox.Children.Add(db);
                EditorCore.LoadedFunctions.Add(block, db);
                EditorCore.CurrentBlock = block;
                SaveButton.IsEnabled = true;
                BlockCreation(block);

            };
            Fileview.Children.Add(add);
            foreach(var block in EditorCore.LoadedFunctions)
                BlockCreation(block.Key);
        }

        /// <summary>
        /// save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (EditorCore.CurrentBlock is not null)
            {
                EditorCore.Save(EditorCore.CurrentBlock, ExeCore.WorkingDirectory);
                LoadProjectDirectory(ExeCore.WorkingDirectory);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (EditorCore.CurrentBlock is not null)
            {
                RunButton.IsEnabled = false;
                HaltButton.IsEnabled = true;
                CurrentlyExecuted = EditorCore.CurrentBlock;
                ExecuteTask(EditorCore.CurrentBlock);
            }
        }
        private SSBlock? CurrentlyExecuted = null;
        private async void ExecuteTask(SSBlock s)
        {
            try
            {
                foreach (var mod in ModHandler.LoadedMods)
                    mod.BeforeExecution();
                var result = await SSProcess.Execute(s, EditorRegistry.RegisteredTypes.ToDictionary(), ExeCore.WorkingDirectory);
                if (result.ReturnValue is not null)
                    Debug.Log(StatusCode.Success, $"Execution ended with result: {result.ReturnValue}", null);
                else
                    Debug.Log(StatusCode.Success, $"Execution ended", null);
            }
            catch (Exception e)
            {
                Debug.Log(StatusCode.Error, e.ToString(), null);
            }
            Dispatcher.Invoke(() => { RunButton.IsEnabled = true; HaltButton.IsEnabled = false; });
            foreach (var mod in ModHandler.LoadedMods)
            {
                try
                {
                    mod.AfterExecution();
                }
                catch(Exception e)
                {
                    Debug.Log(StatusCode.Error, $"[{mod.Name}] "+ e.ToString(), null);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(CurrentlyExecuted is not null)
                CurrentlyExecuted.Done = true;
        }

        private void ModLister_Click(object sender, RoutedEventArgs e)
        {
            new ModList().ShowDialog();
        }
    }
}