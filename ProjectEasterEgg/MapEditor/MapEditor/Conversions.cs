using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using System.IO;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Conversions
    {
        public static Point toXnaPoint(this System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }

        public static Vector2 toVector2(this System.Drawing.Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static void Write(this Stream stream, string stringToWrite)
        {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(stringToWrite);
            stream.Write(bytes, 0, bytes.Length);
        }

        public static Point toPoint(this Vector2 p)
        {
            return new Point((int)(p.X + .5f), (int)(p.Y + .5f));
        }
    }
}
