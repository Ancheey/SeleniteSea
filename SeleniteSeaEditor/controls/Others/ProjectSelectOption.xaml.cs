using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeleniteSeaEditor.controls.Others
{
    /// <summary>
    /// Logika interakcji dla klasy ProjectSelectOption.xaml
    /// </summary>
    public partial class ProjectSelectOption : Button
    {
        public ProjectSelectOption(string path, DateTime date)
        {
            InitializeComponent();
            ProjectName.Content = path.Replace("\"","").Split("\\").Last();
            ProjectSource.Content = path;
            DisplayDate.Content = date.ToString("dd.MM.yyyy");
        }
    }
}
