using Microsoft.Win32;
using SeleniteSeaEditor.controls.Others;
using SeleniteSeaExecutor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeleniteSeaEditor.controls
{
    /// <summary>
    /// Logika interakcji dla klasy ProjectSelectWindow.xaml
    /// </summary>
    public partial class ProjectSelectWindow : Window
    {
        List<(string path, DateTime date)> Projects = [];
        public string SelectedPath = "";
        public ProjectSelectWindow()
        {
            InitializeComponent();
            if (File.Exists(ExeCore.LocalDirectory + "\\projects.txt"))
            {
                using StreamReader reader = new(ExeCore.LocalDirectory + "\\projects.txt");
                string? line;
                while((line = reader.ReadLine())!= null)
                {
                    if (Directory.Exists(line))
                    {
                        Projects.Add((line, Directory.GetLastWriteTime(line)));
                    }
                }
                Projects = Projects.OrderByDescending(k => k.date).DistinctBy(k => k.path).ToList();
                foreach (var (path, date) in Projects)
                {
                    var a = new ProjectSelectOption(path,date);
                    a.Click += (s, e) => 
                    {
                        SelectedPath = path;
                        DialogResult = true;
                        Saveconfig();
                        Close();
                    };
                    ProjectList.Children.Add(a);
                }
                reader.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dialog = new()
            {
                Multiselect = false,
                Title = "New Project"
            };
            if(dialog.ShowDialog() == true)
            {
                SelectedPath = dialog.FolderName;
                DialogResult = true;
                Projects.Add((dialog.FolderName, DateTime.Now));
                Projects = Projects.OrderByDescending(k => k.date).DistinctBy(k => k.path).ToList();
                Saveconfig();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Ancheey/SeleniteSea2/tree/master-notypes");
        }
        private void Saveconfig()
        {
            using StreamWriter writer = new(ExeCore.LocalDirectory + "\\projects.txt");
            foreach (var item in Projects) 
            {
                writer.WriteLine(item.path);
            }
        }
    }
}
