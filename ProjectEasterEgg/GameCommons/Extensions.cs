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
        public static IEnumerable<Position> ToPositions(this IEnumerable<IPositionable> positionables)
        {
            foreach (IPositionable positionable in positionables)
            {
                yield return positionable.Position;
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

        public static IEnumerable<Position> Diff(this IEnumerable<Position> list)
        {
            Position previous = null;

            foreach (Position pos in list)
            {
                if (previous != null)
                {
                    yield return pos - previous;
                }
                previous = pos;
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

        public static Point Location(this MouseState mouseState)
        {
            return new Point(mouseState.X, mouseState.Y);
        }
    }
}
