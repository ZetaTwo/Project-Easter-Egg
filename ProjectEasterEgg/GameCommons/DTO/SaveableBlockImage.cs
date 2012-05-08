using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.Graphic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class SaveBlockImage : BlockImage
    {
        public byte[] BitmapBytes;

        internal void updateDataToBeSaved()
        {
            graphics.Flush(FlushIntention.Sync);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapBytes = new byte[data.Height * data.Stride];
            Marshal.Copy(data.Scan0, BitmapBytes, 0, BitmapBytes.Length);
            bitmap.UnlockBits(data);
        }
    }
}
