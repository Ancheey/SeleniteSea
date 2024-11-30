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
            //Adding types to the drop-down
            foreach (var i in EditorRegistry.Variables)
                TypePicker.Items.Add(i.Value.Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }
    }
}
