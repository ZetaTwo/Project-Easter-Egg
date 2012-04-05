using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class Texture2DWithPos
    {
        public Texture2D Texture;
        public Point Coord;
        public string Name;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Coord.X, Coord.Y, Texture.Width, Texture.Height);
            }
        }
    }
}
