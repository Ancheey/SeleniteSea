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
        readonly EditorRegistryActionItem RegistryData;
        public override Color Color
        {
            get => _color;
            set
            {
                _color = value;
                PrimaryBorder.Color = value;
                ActionTitle.Foreground = new SolidColorBrush(value);
                BinIcon.Foreground = new SolidColorBrush(value);
                WrenchIcon.Foreground = new SolidColorBrush(value);
                Background = new SolidColorBrush(value)
                {
                    Opacity = 0.02
                };
                
                foreach (var i in ScopeActionsContainer.Children)
                {
                    
                    if (i is ScopeAddButton a)
                        a.Color = value;
                    else if(i is DisplayBlock db && i is not IActionContainer)
                        db.Color = value;
                }
            }
        }
        public DisplaySSBlockScope(SSBlockScope ownedScope)
        {
            this.ownedScope = ownedScope;
            InitializeComponent();
            Color = ScopeColorAid.GetNext();

            Refresh();

            RegistryData = EditorRegistry.Actions[ownedScope.GetType()];
            if (RegistryData.Editable)
            {
                WrenchIcon.Width = 25;
                WrenchIcon.MouseDoubleClick += WrenchIcon_MouseDoubleClick;
            }
            if (RegistryData.Deletable)
            {
                BinIcon.Width = 25;
                BinIcon.MouseDoubleClick += BinIcon_MouseDoubleClick;
            }

            var additionbutton = new ScopeAddButton();
            additionbutton.OnClick = () => ClickAddButton(ScopeActionsContainer.Children.IndexOf(additionbutton) / 2);
            additionbutton.Color = Color;
            ScopeActionsContainer.Children.Add(additionbutton);
        }
        public void AddAction(DisplayBlock action, int index)
        {
            ScopeActionsContainer.Children.Insert((index * 2) + 1, action);
            var additionbutton = new ScopeAddButton();
            action.Container = this;
            additionbutton.OnClick = () => ClickAddButton(ScopeActionsContainer.Children.IndexOf(additionbutton) / 2);
            additionbutton.Color = Color;
            ScopeActionsContainer.Children.Insert((index * 2) + 2, additionbutton);
            if (action is not IActionContainer)
                action.Color = Color;
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
                    var block = EditorCore.InstantiateTryEditAndGetDisplay(ownedScope, type, targetindex);
                    if (block is not null)
                        AddAction(block, targetindex);
                }    
            }
        }

        public bool RemoveAction(DisplayBlock action)
        {
            var index = ScopeActionsContainer.Children.IndexOf(action);
            if (index == -1)
                return false;
            action.Container = null;
            ScopeActionsContainer.Children.RemoveRange(index, 2);
            return true;
        }
        public void Refresh()
        {
            ActionTitle.Content = ownedScope.Title;
        }
        private void WrenchIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditorCore.TryOpenEditor(ownedScope, out var i);
            if (i)
                Refresh();
        }
        private void BinIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to delete this block and all of it's children?:\n{ownedScope.Title}\nThis action cannot be undone!", "Deletion notice", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            ownedScope.Parent?.RemoveChild(ownedScope);
            Container?.RemoveAction(this);

        }
    }
}
