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

namespace SeleniteSeaEditor.controls
{
    /// <summary>
    /// Logika interakcji dla klasy ScopeAddButton.xaml
    /// </summary>
    public partial class ScopeAddButton : UserControl
    {
        private MouseButtonEventHandler? _onclick;
        public Action? OnClick
        {
            set
            {
                if (_onclick is not null)
                    Hitbox.MouseLeftButtonUp -= _onclick;
                _onclick = (o,e) => { value?.Invoke(); };
                Hitbox.MouseLeftButtonUp += _onclick;
            }
        }
        private Color _AnimTargetColor = Colors.Yellow;
        public Color Color
        {
            get
            {
                return TailColor.Color;
            }
            set
            {
                Diamond.BorderBrush = new SolidColorBrush(value);
                TailColor.Color = value;
                Plus.Foreground = new SolidColorBrush(value);
                _AnimTargetColor = value;
            }
        }
        public ScopeAddButton()
        {
            InitializeComponent();
            Opacity = 0.05;
        }

        private void Hitbox_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new()
            {
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            BeginAnimation(OpacityProperty, da);
        }

        private void Hitbox_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new()
            {
                To = 0.05,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            BeginAnimation(OpacityProperty, da);
        }
    }
}
