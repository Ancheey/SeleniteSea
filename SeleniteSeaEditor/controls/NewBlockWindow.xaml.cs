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

namespace SeleniteSeaEditor.controls
{
    /// <summary>
    /// Logika interakcji dla klasy NewBlockWindow.xaml
    /// </summary>
    public partial class NewBlockWindow : Window
    {
        public Type? Result { get; private set; } = null; 
        public NewBlockWindow()
        {
            InitializeComponent();
            Deactivated += (o, e) =>
            {
                try
                {
                    Close();
                }
                catch
                {
                    //Window was closing anyway
                }
            };
            foreach(var action in EditorRegistry.Actions.Where(a=>a.Value.Createable).OrderBy(k=>k.Key.IsAssignableTo(typeof(SSBlockScope))))
            {
                var item = new ListBoxItem()
                {
                    Content = $"{action.Value.Name}: {action.Value.Description}"
                };
                item.Foreground = new SolidColorBrush(action.Key.IsAssignableTo(typeof(SSBlockScope))?Colors.Yellow:Colors.OrangeRed);
                item.MouseDoubleClick += (o, e) => { Result = action.Key; DialogResult = true; Close(); };
                ActionSelectionListbox.Items.Add(item);
                
            }
        }
    }
}
