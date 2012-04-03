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
        public const int TILE_WIDTH = 124; //126 - 2
        public const int TILE_HEIGHT = 62; //64 - 2

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

        public static Position fromScreen(Vector2 screen)
        {
            return new Position((int)(TILE_WIDTH * screen.Y + TILE_HEIGHT * screen.X) / (TILE_WIDTH * TILE_HEIGHT),
                               (int)(TILE_WIDTH * screen.Y - TILE_HEIGHT * screen.X) / (TILE_WIDTH * TILE_HEIGHT),
                               0);
        }
    }
}
