using SeleniteSeaCore.codeblocks.scopes;
using SeleniteSeaCore.variables;
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
    /// Logika interakcji dla klasy SSBlockScopeWhile.xaml
    /// </summary>
    public partial class EditorSSBlockScopeWhile : Window
    {
        SSBlockScopeWhile ToEdit;
        
        public EditorSSBlockScopeWhile(SSBlockScopeWhile toEdit)
        {
            InitializeComponent();
            ToEdit = toEdit;
            ComparisonType.Items.Clear();
            foreach(var i in Comparers.ComparerRegistry)
                ComparisonType.Items.Add(i.Value);
            ComparisonType.SelectedItem = ToEdit.Comparer;
        }

        private void ComparisonType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComparisonType.SelectedItem is not SSValueComparerType comparer)
                return;
            ArgBox.Children.Clear();
            var Alphabet = "ABCDEFGHIJKLMNOPRSTUWXYZ";
            for(int i = 0; i < comparer.ComparedValues; i++)
            {
                var sign = $"{Alphabet[i % Alphabet.Length]}{(i > Alphabet.Length ? i / Alphabet.Length : "")}";
                var value = (ToEdit.Values.Count > i ? ToEdit.Values[i].Data : "");
                ArgBox.Children.Add(new SSValueEditor(sign, value));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToEdit.Values.Clear();
            ToEdit.Comparer = ComparisonType.SelectedItem as SSValueComparerType;
            foreach (var i in ArgBox.Children)
            {
                if (i is not SSValueEditor val)
                    continue;
                ToEdit.Values.Add(new(val.Value));
            }
            DialogResult = true;
        }
    }
}
