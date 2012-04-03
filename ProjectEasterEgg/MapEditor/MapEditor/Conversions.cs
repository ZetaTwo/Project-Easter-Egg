using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Conversions
    {
        public static Point toXnaPoint(this System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }
    }
}
