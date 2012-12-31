using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor
{
    class Texture2DWithDoublePos
    {
        public Texture2DWithPos t;
        public Point CoordAtMouseDown;

        public Texture2DWithDoublePos(Texture2DWithPos t)
        {
            this.t = t;
        }
    }
}
