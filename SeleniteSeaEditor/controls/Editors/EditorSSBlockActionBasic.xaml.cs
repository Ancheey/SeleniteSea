using SeleniteSeaCore.codeblocks.actions;
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
    /// Logika interakcji dla klasy EditorSSBlockActionBasic.xaml
    /// </summary>
    public partial class EditorSSBlockActionBasic : Window
    {
        private readonly SSBlockActionBasic Edited;
        readonly Dictionary<string, SSValueEditor> keyValuePairs = [];
        public EditorSSBlockActionBasic(SSBlockActionBasic basic)
        {
            Edited = basic;
            InitializeComponent();
            if(EditorRegistry.Actions.TryGetValue(basic.GetType(), out var item))
            {
                Title = $"{item.Name} editor";
                ItemName.Content = item.Name;
                ItemDescription.Text = item.Description;
            }
            foreach(var value in basic.PublicValues)
            {
                var editor = new SSValueEditor(value.Key,value.Value?.Data);
                Values.Children.Add(editor);
                keyValuePairs.Add(value.Key, editor);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(var pair in keyValuePairs)
                Edited.PublicValues[pair.Key] = new(pair.Value.Value);
            DialogResult = true;
        }
    }
}
