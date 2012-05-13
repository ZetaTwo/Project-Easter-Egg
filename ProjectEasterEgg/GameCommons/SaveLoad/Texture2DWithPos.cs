using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using SD = System.Drawing;
using System.Drawing.Imaging;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class Texture2DWithPos : ImageWithPos
    {
        public readonly SD.Bitmap originalBitmap;
        private SD.Bitmap bitmap;

        private Texture2D texture;
        public Texture2D Texture { get { return texture; } }

        private SD.Graphics graphics;
        public SD.Graphics Graphics { get { return graphics; } }

        protected string originalPath;
        public string OriginalPath { get { return originalPath; } }

        public readonly List<SaveBlock> projectedOnto = new List<SaveBlock>();

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle(pos.X, pos.Y, originalBitmap.Width, originalBitmap.Height);
            }
        }





        public Texture2DWithPos()
        { }

        public Texture2DWithPos(SD.Bitmap originalBitmap, GraphicsDevice graphicsDevice)
        {
            this.originalBitmap = originalBitmap;
            restoreBitmapFromOriginal();
            updateTexture2D(graphicsDevice);
        }

        public Texture2DWithPos(SD.Bitmap originalBitmap, GraphicsDevice graphicsDevice, string originalPath)
            : this (originalBitmap, graphicsDevice)
        {
            this.originalPath = originalPath.Replace('\\', '/');
            this.name = this.originalPath.Split('/').Last();
        }





        public void restoreBitmapFromOriginal()
        {
            bitmap = originalBitmap.CloneFix();
            graphics = SD.Graphics.FromImage(bitmap);
        }

        public void updateTexture2D(GraphicsDevice graphicsDevice)
        {
            graphics.Flush(SD.Drawing2D.FlushIntention.Sync);
            texture = bitmap.ToTexture2D(graphicsDevice);
        }

        public override void SaveTo(Stream stream)
        {
            Texture.SaveAsPng(stream, Texture.Width, Texture.Height);
        }
    }
}
