using SeleniteSeaCore.codeblocks;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SeleniteSeaEditor.controls.Editors
{
    /// <summary>
    /// Logika interakcji dla klasy EditorSSBlockScopeFunction.xaml
    /// </summary>
    public partial class EditorSSBlockScopeFunction : Window
    {
        SSBlockScopeFunction ToEdit;
        public EditorSSBlockScopeFunction(SSBlockScopeFunction toedit)
        {
            InitializeComponent();
            ToEdit = toedit;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToEdit.Name = FnName.Text;
            ToEdit.Description = FnDesc.Text;
            DialogResult = true;

        }
    }
}
