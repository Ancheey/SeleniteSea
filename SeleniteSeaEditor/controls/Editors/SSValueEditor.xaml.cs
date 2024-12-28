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

namespace SeleniteSeaEditor.controls.Editors
{
    /// <summary>
    /// Logika interakcji dla klasy SSValueEditor.xaml
    /// </summary>
    public partial class SSValueEditor : Border
    {
        public SSValueEditor(string title, string? value = "")
        {
            InitializeComponent();
            ValueName.Content = title;
            ValueText.Text = value;
        }
        public string Value => ValueText.Text;
        public string Title => ValueName.Content.ToString()??"";
    }
}
