using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mindstep.EasterEgg.Commons
{
    public static class Constants
    {
        public const int N = 31;
        public const int PROJ_WIDTH = 4*N+1;
        public const int PROJ_HEIGHT = PROJ_WIDTH + (int)(N/2);

        public const int TILE_WIDTH = PROJ_WIDTH;
        public const int TILE_HEIGHT = 2*N + 1;
        public const int BLOCK_HEIGHT = PROJ_HEIGHT - TILE_HEIGHT;

        public static readonly Vector2 blockDrawOffset = new Vector2(-(int)(Constants.PROJ_WIDTH / 2), -Constants.BLOCK_HEIGHT);

        public const string SCRIPT_BLOCK_PREFIX = "ScriptBlock";
    }
}
