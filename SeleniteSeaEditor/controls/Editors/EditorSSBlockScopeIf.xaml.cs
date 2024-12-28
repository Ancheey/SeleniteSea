using SeleniteSeaCore.codeblocks.scopes;
using SeleniteSeaCore.variables;
using System.Windows;
using System.Windows.Controls;


namespace SeleniteSeaEditor.controls.Editors
{
    /// <summary>
    /// Logika interakcji dla klasy EditorSSBlockScopeIf.xaml
    /// </summary>
    public partial class EditorSSBlockScopeIf : Window
    {
        readonly SSBlockScopeIf ToEdit;

        public EditorSSBlockScopeIf(SSBlockScopeIf toEdit)
        {
            InitializeComponent();
            ToEdit = toEdit;
            ComparisonType.Items.Clear();
            foreach (var i in Comparers.ComparerRegistry)
                ComparisonType.Items.Add(i.Value);
            ComparisonType.SelectedItem = ToEdit.Comparer;
        }

        private void ComparisonType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComparisonType.SelectedItem is not SSValueComparerType comparer)
                return;
            ArgBox.Children.Clear();
            var Alphabet = "ABCDEFGHIJKLMNOPRSTUWXYZ";
            for (int i = 0; i < comparer.ComparedValues; i++)
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
