using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public static class GameTimeExtensions
    {
        public static long TotalMsLong(this GameTime gameTime)
        {
            return (long)gameTime.TotalGameTime.TotalMilliseconds;
        }
    }
}
