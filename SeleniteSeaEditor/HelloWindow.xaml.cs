using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace SeleniteSeaEditor
{
    /// <summary>
    /// Logika interakcji dla klasy HelloWindow.xaml
    /// </summary>
    public partial class HelloWindow : Window
    {
        public HelloWindow()
        {
            InitializeComponent();
            DispatcherTimer tmr = new()
            {
                Interval = TimeSpan.FromSeconds(2.5)
            };
            tmr.Tick += TimerTick;
            tmr.Start();
        }
        private void TimerTick(object? sender, EventArgs e)
        {
            DispatcherTimer? timer = (DispatcherTimer?)sender;
            timer?.Stop();
            if(timer is not null)
                timer.Tick -= TimerTick;
            Close();
        }
    }
}
