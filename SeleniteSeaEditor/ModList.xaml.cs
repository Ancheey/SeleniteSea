﻿using System;
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

namespace SeleniteSeaEditor
{
    /// <summary>
    /// Logika interakcji dla klasy ModList.xaml
    /// </summary>
    public partial class ModList : Window
    {
        public ModList()
        {
            InitializeComponent();
            foreach (var item in EditorCore.LoadedMods)
                ModListBox.Items.Add(item);
        }
    }
}
