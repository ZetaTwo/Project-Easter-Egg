using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mindstep.EasterEgg.MapEditor
{
    class Helper
    {
        public static ClickOperation getClickOperation()
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                return ClickOperation.Toggle;
            }
            if (Control.ModifierKeys == Keys.Control)
            {
                return ClickOperation.Copy;
            }
            return ClickOperation.Replace;
        }
    }
}
