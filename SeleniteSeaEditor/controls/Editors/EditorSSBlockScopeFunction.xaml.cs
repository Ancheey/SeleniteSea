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
            FnName.Text = toedit.Name;
            FnDesc.Text = toedit.Description;

            foreach(var i in ToEdit.RequestedVariables)
            {
                var a = new EditorSSBlockScopeFunctionArgument(i.Key,i.Value.defaultValue,i.Value.description);
                a.DeleteButton.Click += (s, e) =>
                {
                    ArgBox.Children.Remove(a);
                };
                ArgBox.Children.Add(a);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToEdit.Name = FnName.Text;
            ToEdit.Description = FnDesc.Text;
            ToEdit.RequestedVariables = [];
            foreach(var i in ArgBox.Children)
            {
                if (i is not EditorSSBlockScopeFunctionArgument arg)
                    continue;
                ToEdit.RequestedVariables.Add(arg.VarName, (arg.VarDescription, arg.VarDefault));
            }

            DialogResult = true;

        }

        private void AddArgButton_Click(object sender, RoutedEventArgs e)
        {
            var a = new EditorSSBlockScopeFunctionArgument();
            a.DeleteButton.Click += (s, e) =>
            {
                ArgBox.Children.Remove(a);
            };
            ArgBox.Children.Add(a);
        }
    }
}
