using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public static class Conversions
    {
        public static Vector2 ToVector2(this System.Drawing.Point point)
        {
            return new Vector2(point.X, point.Y);
        }
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point ToXnaPoint(this System.Drawing.Point point)
        {
            return new Point(point.X, point.Y);
        }
        public static Point ToXnaPoint(this Vector2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }


        public static System.Drawing.Point ToSDPoint(this Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }
        public static System.Drawing.Point ToSDPoint(this Vector2 point)
        {
            return new System.Drawing.Point((int)point.X, (int)point.Y);
        }

        public static Position ToPosition(this Vector3 pos)
        {
            return new Position((int)pos.X, (int)pos.Y, (int)pos.Z);
        }
        public static Vector3 ToVector3(this Position pos)
        {
            return new Vector3(pos.X, pos.Y, pos.Z);
        }
    }
}
