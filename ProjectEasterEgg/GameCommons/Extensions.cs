using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Physics;
using Mindstep.EasterEgg.Commons.DTO;
using Microsoft.Xna.Framework.Input;

namespace Mindstep.EasterEgg.Commons
{
    public static class Extensions
    {
        public static int Clamp(this int i, int min, int max)
        {
            return Math.Max(Math.Min(i, max), min);
        }

        public static float Dot(this Vector3 v, Vector3 u)
        {
            return v.X * u.X + v.Y * u.Y + v.Z * u.Z;
        }

        public static Vector3 Project(this Vector3 v, Vector3 on)
        {
            return v.Dot(on) / on.LengthSquared() * on;
        }

        // remove this method.. sometime
        public static Vector4 matrixMul(Vector4 v, Matrix m)
        {
            return new Vector4(
                v.X * m.M11 + v.Y * m.M21 + v.Z * m.M31 + v.W * m.M41,
                v.X * m.M12 + v.Y * m.M22 + v.Z * m.M32 + v.W * m.M42,
                v.X * m.M13 + v.Y * m.M23 + v.Z * m.M33 + v.W * m.M43,
                v.X * m.M14 + v.Y * m.M24 + v.Z * m.M34 + v.W * m.M44
                );
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

        public static IEnumerable<Position> ToPositions(this IEnumerable<IPositionable> blocks)
        {
            foreach (IPositionable block in blocks)
            {
                yield return block.Position;
            }
        }

        public static IEnumerable<Position> Offset(this IEnumerable<Position> positions, Position by)
        {
            foreach (Position pos in positions)
            {
                yield return pos + by;
            }
        }

        public static IEnumerable<Position> ToPositions(this IEnumerable<GameBlockDTO> blocks)
        {
            foreach (GameBlockDTO block in blocks)
            {
                yield return block.Position;
            }
        }

        public static IEnumerable<System.Drawing.Point> ToSDPoints(this IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                yield return point.ToSDPoint();
            }
        }

        public static void Swap<T>(this IList<T> list, T e1, T e2)
        {
            int index1 = list.IndexOf(e1);
            int index2 = list.IndexOf(e2);
            if (index1 == -1 || index2 == -1)
            {
                throw new ArgumentException("Both elements were not in the list");
            }
            list[index1] = e2;
            list[index2] = e1;
        }

        public static Point RelativeCenter(this Rectangle r)
        {
            return new Point(r.Width / 2, r.Height / 2);
        }

        /// <summary>
        /// Casts a float to an int, rounding it down even if it has a negative value.
        /// </summary>
        /// <param name="f">float to cast</param>
        /// <returns></returns>
        public static int RoundDown(this float f)
        {
            if (f < 0 && f % 1 != 0)
            {
                return (int)f - 1;
            }
            else
            {
                return (int)f;
            }
        }

        public static bool BetweenInclusive(this int i, int above, int below)
        {
            return above <= i && i <= below;
        }

        public static bool BetweenExclusive(this int i, int above, int below)
        {
            return above < i && i < below;
        }

        public static Point Location(this MouseState mouseState)
        {
            return new Point(mouseState.X, mouseState.Y);
        }
    }
}
