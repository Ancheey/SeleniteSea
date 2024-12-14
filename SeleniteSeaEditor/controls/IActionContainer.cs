using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaEditor.controls
{
    public interface IActionContainer
    {
        public void AddAction(DisplayBlock action, int index);
        public bool RemoveAction(DisplayBlock action);
    }
}
