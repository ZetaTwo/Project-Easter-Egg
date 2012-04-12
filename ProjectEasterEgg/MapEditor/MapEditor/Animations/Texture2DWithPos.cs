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

        public readonly List<SaveBlock> projectedOnto = new List<SaveBlock>();

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Coord.X, Coord.Y, Texture.Width, Texture.Height);
            }
        }

        public Texture2DWithPos(string originalPath)
        {
            this.originalPath = originalPath.Replace('\\', '/');
            this.RelativePath = this.originalPath.Split('/').Last();
        }

        public Texture2DWithPos()
        { }

        public override int GetHashCode()
        {
            return RelativePath.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Texture2DWithPos texture2DWithPos = obj as Texture2DWithPos;
            if ((object)texture2DWithPos == null)
            {
                return false;
            }

            // Return true if the fields match:
            return RelativePath == texture2DWithPos.RelativePath;
        }
    }
}
