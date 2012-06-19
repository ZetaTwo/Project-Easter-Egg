using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Input;

namespace Mindstep.EasterEgg.Commons.Graphic
{
    public static class Extensions
    {
        public static IntPtr GetHicon(this System.Drawing.Image image, Microsoft.Xna.Framework.Point hotSpot)
        {
            int left = hotSpot.X;
            int right = image.Width - hotSpot.X;
            int up = hotSpot.Y;
            int down = image.Height - hotSpot.Y;

            using (Bitmap cursorBitmap = new Bitmap(Math.Max(left, right) * 2, Math.Max(up, down) * 2))
            {
                using (Graphics graphics = Graphics.FromImage(cursorBitmap))
                {
                    graphics.DrawImage(image, new Point((right - left).lowerLimit(0), (down - up).lowerLimit(0)));
                }
                return cursorBitmap.GetHicon();
            }
        }

        public static Bitmap ToBitmap(this Texture2D texture)
        {
            return (System.Drawing.Bitmap)System.Drawing.Image.FromStream(texture.ToMemoryStream());
        }

        public static MemoryStream ToMemoryStream(this Texture2D texture)
        {
            MemoryStream memoryStream = new MemoryStream();
            texture.SaveTo(memoryStream);
            return memoryStream;
        }

        public static void SaveTo(this Texture2D texture, Stream stream)
        {
            texture.SaveAsPng(stream, texture.Width, texture.Height);
        }
    }
}
