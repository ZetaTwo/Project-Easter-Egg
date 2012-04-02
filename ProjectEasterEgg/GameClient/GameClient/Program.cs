using System;
using Mindstep.EasterEgg;
using Microsoft.Xna.Framework;

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
            using (Game game = new EggEngine())
            {
                game.Run();
            }
        }
    }
#endif
}

