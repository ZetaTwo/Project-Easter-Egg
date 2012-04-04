using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Graphics
{
    public class SpriteHelper
    {
        public const int TILE_WIDTH = 125;
        public const int TILE_HEIGHT = 63;

        public const int BLOCK_HEIGHT = 77;

        public const int NUM_LAYERS = 20;

        public static Vector2 toScreen(Position map)
        {
            return toScreen(map, true);
        }

        public static Vector2 toScreen(Position map, bool transform)
        {
            if (transform)
            {
                return new Vector2(map.X * (TILE_WIDTH / 2) - map.Y * (TILE_WIDTH / 2), map.X * (TILE_HEIGHT / 2) + map.Y * (TILE_HEIGHT / 2) + map.Z);
            }
            else
            {
                return new Vector2(map.X, map.Y);
            }
        }

        public static Vector3 fromScreen(Vector2 screen, int layer = 0)
        {
            return new Vector3((TILE_WIDTH * (screen.Y - layer * BLOCK_HEIGHT) + TILE_HEIGHT * screen.X) / (TILE_WIDTH * TILE_HEIGHT),
                               (TILE_WIDTH * (screen.Y - layer * BLOCK_HEIGHT) - TILE_HEIGHT * screen.X) / (TILE_WIDTH * TILE_HEIGHT),
                               0f);
        }


    }
}
