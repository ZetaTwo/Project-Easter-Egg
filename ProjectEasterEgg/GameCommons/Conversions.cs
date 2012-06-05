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
            return new Point(point.X.Floor(), point.Y.Floor());
        }


        public static System.Drawing.Point ToSDPoint(this Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }
        public static System.Drawing.Point ToSDPoint(this Vector2 point)
        {
            return new System.Drawing.Point(point.X.Floor(), point.Y.Floor());
        }

        public static Vector3 ToVector3(this Position pos)
        {
            return new Vector3(pos.X, pos.Y, pos.Z);
        }
        public static Position ToPosition(this Vector3 pos)
        {
            return pos.Floor();
        }
        public static Position Floor(this Vector3 pos)
        {
            return new Position(pos.X.Floor(), pos.Y.Floor(), pos.Z.Floor());
        }
        public static Position Ceiling(this Vector3 pos)
        {
            return new Position(pos.X.Ceiling(), pos.Y.Ceiling(), pos.Z.Ceiling());
        }

        public static Color ToXnaColor(this System.Drawing.Color c)
        {
            return new Color(c.R, c.G, c.B, c.A);
        }
    }
}
