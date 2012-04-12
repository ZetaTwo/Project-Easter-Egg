using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace EggEnginePipeline
{
    class ImageWithPos
    {
        public readonly System.Drawing.Bitmap bitmap;
        public readonly Point pos;

        public Rectangle bounds { get { return new Rectangle(pos.X, pos.Y, bitmap.Width, bitmap.Height); } }

        public ImageWithPos(Stream stream, Point pos)
            : this(new System.Drawing.Bitmap(stream), pos)
        { }

        public ImageWithPos(System.Drawing.Bitmap image, Point pos)
        {
            this.bitmap = image;
            this.pos = pos;
        }
    }
}
