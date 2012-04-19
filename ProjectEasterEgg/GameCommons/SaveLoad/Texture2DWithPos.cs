using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class Texture2DWithPos : ImageWithPos
    {
        public Texture2D Texture;

        private string originalPath;
        public string OriginalPath { get { return originalPath; } }

        public readonly List<SaveBlock> projectedOnto = new List<SaveBlock>();

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle(pos.X, pos.Y, Texture.Width, Texture.Height);
            }
        }

        public Texture2DWithPos(string originalPath)
        {
            this.originalPath = originalPath.Replace('\\', '/');
            this.name = this.originalPath.Split('/').Last();
        }

        public Texture2DWithPos()
        { }

        public override void SaveTo(Stream stream)
        {
            Texture.SaveAsPng(stream, Texture.Width, Texture.Height);
        }
    }
}
