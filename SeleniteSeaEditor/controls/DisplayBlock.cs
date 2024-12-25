using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeleniteSeaEditor.controls
{
    public class DisplayBlock : UserControl
    {
        public IActionContainer? Container { get; set; }
        protected Color _color;
        public virtual Color Color { get; set; }
        protected DisplayBlock() { }
    }
}
