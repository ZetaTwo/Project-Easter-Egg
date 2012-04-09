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
        public Point Coord;
        public Texture2D Texture;

        private string originalPath;
        public string OriginalPath { get { return originalPath; } }

        public string RelativePath;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Coord.X, Coord.Y, Texture.Width, Texture.Height);
            }
        }

        public Texture2DWithPos() { }

        public Texture2DWithPos(string originalPath)
        {
            this.originalPath = originalPath.Replace('\\', '/');
            this.RelativePath = this.originalPath.Split('/').Last();
        }
    }
}
