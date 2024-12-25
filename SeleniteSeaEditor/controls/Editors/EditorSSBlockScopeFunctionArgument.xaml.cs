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
    /// Logika interakcji dla klasy EditorSSBlockScopeFunctionArgument.xaml
    /// </summary>
    public partial class EditorSSBlockScopeFunctionArgument : UserControl
    {
        public string VarName => VarNameVal.Text;
        public string VarDefault => VarDefVal.Text;
        public string VarDescription => VarDesVal.Text;
        public EditorSSBlockScopeFunctionArgument()
        {
            InitializeComponent();
        }
        public EditorSSBlockScopeFunctionArgument(string name, string def, string desc)
        {
            InitializeComponent();
            VarNameVal.Text = name;
            VarDefVal.Text = def;
            VarDesVal.Text = desc;
        }
    }
}
