using SeleniteSeaCore.codeblocks.scopes;
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
    /// Logika interakcji dla klasy EditorSSBlockScopeIterator.xaml
    /// </summary>
    public partial class EditorSSBlockScopeIterator : Window
    {
        readonly SSBlockScopeIterate ToEdit;
        public EditorSSBlockScopeIterator(SSBlockScopeIterate toEdit)
        {
            InitializeComponent();
            ToEdit = toEdit;

            IterateFrom.Text = toEdit.StartingPoint.Data;
            IterateTo.Text = toEdit.EndingPoint.Data;
            IterateBy.Text = toEdit.Jump.Data;
            VarName.Text = toEdit.VariableName.Data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToEdit.StartingPoint.Data = IterateFrom.Text;
            ToEdit.EndingPoint.Data = IterateTo.Text;
            ToEdit.Jump.Data = IterateBy.Text;
            ToEdit.VariableName.Data = VarName.Text;
            DialogResult = true;
        }
    }
}
