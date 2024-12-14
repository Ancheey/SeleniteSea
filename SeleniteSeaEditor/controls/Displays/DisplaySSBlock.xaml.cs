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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeleniteSeaEditor.controls.Displays
{
    /// <summary>
    /// Logika interakcji dla klasy DisplaySSBlock.xaml
    /// </summary>
    public partial class DisplaySSBlock : DisplayBlock
    {
        readonly SSBlock Owned;
        readonly EditorRegistryActionItem RegistryData;

        private Color color;
        public override Color Color
        {
            get => color;
            set
            {
                var brush = new SolidColorBrush(value);
                color = value;
                PointerIcon.Foreground = brush;
                WrenchIcon.Foreground = brush;
                BlockTitle.Foreground = brush;
            }
        }
        public DisplaySSBlock(SSBlock ownedblock)
        {
            
            InitializeComponent();


            Owned = ownedblock;
            BlockTitle.Content = Owned.Title;

            RegistryData = EditorRegistry.Actions[Owned.GetType()];

            if (RegistryData.Editable)
            {
                WrenchIcon.Width = 25;
                WrenchIcon.MouseDoubleClick += WrenchIcon_MouseDoubleClick;
            }
            if (RegistryData.Deletable)
            {
                BinIcon.Width = 25;
                BinIcon.MouseDoubleClick += WrenchIcon_MouseDoubleClick;
            }


        }

        private void WrenchIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditorCore.TryOpenEditor(Owned, out var i);
            if(i)
                BlockTitle.Content = Owned.Title;
        }
        private void BinIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Deletion notice", $"Are you sure you want to delete a block\n{Owned.Title}\nThis action cannot be undone!", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            Owned.Parent?.RemoveChild(Owned);
            if(Parent is not null && Parent is IActionContainer cont)
            {
                cont.RemoveAction(this);
            }
            
        }
    }
}
