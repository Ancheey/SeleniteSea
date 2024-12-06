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
        SSBlock Owned;
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
            Owned = ownedblock;
            BlockTitle.Content = Owned.Title;

            if(EditorRegistry.Actions.TryGetValue(Owned.GetType(),out var action) && action.Editable)
            {
                WrenchIcon.Width = 25;
            }

            InitializeComponent();
        }
    }
}
