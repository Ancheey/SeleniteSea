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
using SeleniteSeaCore.codeblocks;
using SeleniteSeaEditor.controls;

namespace SeleniteSeaEditor.controls.Displays
{
    /// <summary>
    /// Logika interakcji dla klasy DisplaySSBlockScope.xaml
    /// </summary>
    public partial class DisplaySSBlockScope : DisplayBlock, IActionContainer
    {
        private SSBlockScope ownedScope;
        public override Color Color
        {
            get => _color;
            set
            {
                _color = value;
                PrimaryBorder.Color = value;
                ActionTitle.Foreground = new SolidColorBrush(value);
                Background = new SolidColorBrush(value)
                {
                    Opacity = 0.02
                };
                foreach (var i in ScopeActionsContainer.Children)
                    if (i is ScopeAddButton a)
                        a.Color = value;
            }
        }
        public DisplaySSBlockScope(SSBlockScope ownedScope)
        {
            this.ownedScope = ownedScope;
            InitializeComponent();
            var additionbutton = new ScopeAddButton((o, e) => { ClickAddButton(0); });
            ScopeActionsContainer.Children.Add(additionbutton);
        }
        public void AddAction(DisplayBlock action, int index)
        {
            ScopeActionsContainer.Children.Insert(index + 1, action);
            var additionbutton = new ScopeAddButton((o, e) => { ClickAddButton(index + 2); });
            ScopeActionsContainer.Children.Insert(index + 2, additionbutton);
        }
        //This method is added as an onClick for all Add buttons
        public void ClickAddButton(int targetindex)
        {
            //New selection dialog
            NewBlockWindow dialog = new();
            if(dialog.ShowDialog()== true)
            {
                var type = dialog.Result;
                if(type is not null)
                {
                    //Instantiate and add the display if it isn't null
                    var block = EditorCore.InstantiateTryEditAndGetDisplay(ownedScope, type);
                    if (block is not null)
                        AddAction(block, targetindex);
                }    
            }
        }
    }
}
