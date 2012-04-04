using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public static class Transform
    {
        public static Point ObjectToProjectionSpace(Position map, int tileHeight, int tileWidth, int blockHeight)
        {
            tileWidth /= 2;
            tileHeight /= 2;
            int x = -map.X * tileWidth + map.Y * tileWidth;
            int y = map.X * tileHeight + map.Y * tileHeight - map.Z * blockHeight;

            return new Point(x, y);
        }

        public static Vector2 toVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static IEnumerable<Position> ToPositions(this IEnumerable<Block> blocks) {
            foreach (Block block in blocks)
            {
                yield return block.Position;
            }
        }
    }
}
