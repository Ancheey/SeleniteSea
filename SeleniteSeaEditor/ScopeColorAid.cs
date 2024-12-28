using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SeleniteSeaEditor
{
    public static class ScopeColorAid
    {
        static readonly List<Color> colors =
            [Colors.Yellow, Colors.GreenYellow, Colors.Green, Colors.Pink, Colors.LightBlue, Colors.IndianRed, Colors.Ivory,
            Colors.LightGoldenrodYellow, Colors.LimeGreen, Colors.HotPink, Colors.Lavender, Colors.OrangeRed, Colors.Wheat];
        static int pointer = 0;
        public static Color GetNext()=>colors[(pointer++)%colors.Count];
    }
}
