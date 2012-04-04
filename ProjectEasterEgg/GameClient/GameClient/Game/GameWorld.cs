using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine;
using Mindstep.EasterEgg.Engine.Game;
using EggEnginePipeline;

namespace Mindstep.EasterEgg.Game.Game
{
    class GameWorld : World
    {
        public override void Initialize(EggEngine _engine)
        {
            base.Initialize(_engine);

            //Add game content here

            CurrentMap = new GameMap(Engine.Content.Load<GameMapDTO>("Models/secondmodel"));
        }
    }
}
