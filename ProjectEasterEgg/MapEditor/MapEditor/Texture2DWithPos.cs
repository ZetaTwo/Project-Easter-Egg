using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor
{
    public class Texture2DWithPos
    {
        public Texture2D Texture;
        public Point pos;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(pos.X, pos.Y, Texture.Width, Texture.Height);
            }
        }
    }
}
