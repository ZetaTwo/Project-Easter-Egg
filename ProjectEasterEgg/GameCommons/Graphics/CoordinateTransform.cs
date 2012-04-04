using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public static class CoordinateTransform
    {
        public const int TILE_WIDTH = 125;
        public const int TILE_HEIGHT = 63;

        public const int BLOCK_HEIGHT = 77;

        public const int NUM_LAYERS = 20;

        public static Vector2 ObjectToProjectionSpace(Position map)
        {
            float x = map.Y * TILE_WIDTH / 2 - map.X * TILE_WIDTH / 2;
            float y = map.X * TILE_HEIGHT / 2 + map.Y * TILE_HEIGHT / 2 - map.Z * BLOCK_HEIGHT;

            return new Vector2(x, y);
        }

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 ToVector2(this System.Drawing.Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static System.Drawing.Point ToPoint(this Vector2 point)
        {
            return new System.Drawing.Point((int)point.X, (int)point.Y);
        }

        public static Point ToXnaPoint(this Vector2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        public static Vector3 fromScreen(Vector2 screen, int layer = 0)
        {
            return new Vector3((TILE_WIDTH * (screen.Y - layer * BLOCK_HEIGHT) + TILE_HEIGHT * screen.X) / (TILE_WIDTH * TILE_HEIGHT),
                               (TILE_WIDTH * (screen.Y - layer * BLOCK_HEIGHT) - TILE_HEIGHT * screen.X) / (TILE_WIDTH * TILE_HEIGHT),
                               0f);
        }

        public static IEnumerable<Position> ToPositions(this IEnumerable<Block> blocks) {
            foreach (Block block in blocks)
            {
                yield return block.Position;
            }
        }
    }
}
