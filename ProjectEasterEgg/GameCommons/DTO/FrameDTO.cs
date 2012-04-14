using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Mindstep.EasterEgg.Commons.Graphics;
using System.Drawing.Drawing2D;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class FrameDTO
    {
        public int Duration;
        public byte[] BitmapBytes;
        private Bitmap bitmap;
        private System.Drawing.Graphics graphics;

        public FrameDTO(int duration)
            : this()
        {
            this.Duration = duration;
        }

        public FrameDTO()
        {
            bitmap = new Bitmap(Constants.PROJ_WIDTH, Constants.PROJ_HEIGHT);
            graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.SetClip(BlockRegions.WholeBlock, CombineMode.Replace);
            graphics.FillRegion(Brushes.White, BlockRegions.Top);
            graphics.FillRegion(Brushes.LightGray, BlockRegions.Left);
            graphics.FillRegion(Brushes.Gray, BlockRegions.Right);
            graphics.FillRegion(Brushes.Black, BlockRegions.OuterBorder);
            graphics.FillRegion(Brushes.Pink, BlockRegions.InnerBorder);
        }

        public System.Drawing.Graphics getGraphics()
        {
            return graphics;
        }

        public void updateDataToBeSaved()
        {
            graphics.Flush(FlushIntention.Sync);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapBytes = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0, BitmapBytes, 0, BitmapBytes.Length);
            bitmap.UnlockBits(data);
        }
    }
}
