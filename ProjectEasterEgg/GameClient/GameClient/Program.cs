using System;
using Mindstep.EasterEgg;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine;
using Mindstep.EasterEgg.Game.Scripts;
using Mindstep.EasterEgg.Game.Game;

namespace GameClient
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game game = new EggEngine(new GameWorld()))
            {
                game.Run();
            }
        }
    }
#endif
}

