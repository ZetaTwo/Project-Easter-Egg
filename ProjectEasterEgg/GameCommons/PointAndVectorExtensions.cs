using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public static class PointAndVectorExtensions
    {
        public static float Dot(this Vector3 v, Vector3 u)
        {
            return v.X * u.X + v.Y * u.Y + v.Z * u.Z;
        }

        public static Vector3 Project(this Vector3 v, Vector3 on)
        {
            return v.Dot(on) / on.LengthSquared() * on;
        }

        public static Point Add(this Point p, Point q)
        {
            return p.Add(q.X, q.Y);
        }

        public static Point Add(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }

        public static Point Subtract(this Point p, Point q)
        {
            return new Point(p.X - q.X, p.Y - q.Y);
        }

        public static Point Multiply(this Point p, float f)
        {
            return new Point((int)(p.X * f), (int)(p.Y * f));
        }

        public static Point Divide(this Point p, float f)
        {
            return new Point((int)(p.X / f), (int)(p.Y / f));
        }
    }
}
