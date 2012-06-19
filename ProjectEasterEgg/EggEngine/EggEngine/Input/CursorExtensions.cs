using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Drawing;
using Mindstep.EasterEgg.Commons.Graphic;

namespace Mindstep.EasterEgg.Engine.Input
{
    public static class CursorExtensions
    {
        public static Cursor ToCursor(this Texture2D texture, Point hotSpot)
        {
            using (Bitmap bitmap = texture.ToBitmap()) {
                return bitmap.ToCursor(hotSpot);
            }
        }

        public static Cursor ToCursor(this System.Drawing.Image image, Point hotSpot)
        {
            return new System.Windows.Forms.Cursor(image.GetHicon(hotSpot));
        }
    }
}
