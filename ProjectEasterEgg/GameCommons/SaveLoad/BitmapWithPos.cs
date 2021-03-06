﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SD = System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class BitmapWithPos : ImageWithPos
    {
        public SD.Bitmap bitmap;

        protected string originalPath;
        public string OriginalPath { get { return originalPath; } }

        public readonly List<SaveBlock> projectedOnto = new List<SaveBlock>();

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle(Position.X, Position.Y, bitmap.Width, bitmap.Height);
            }
        }

        public BitmapWithPos(string originalPath)
        {
            this.originalPath = originalPath.Replace('\\', '/');
            this.name = this.originalPath.Split('/').Last();
        }

        public BitmapWithPos()
        { }

        public override void SaveTo(Stream stream)
        {
            bitmap.SaveTo(stream);
        }
    }
}
