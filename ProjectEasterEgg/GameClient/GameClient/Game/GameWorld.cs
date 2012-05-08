using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Engine.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.DTO;

namespace Mindstep.EasterEgg.Game.Game
{
    class GameWorld : World
    {
        public override void Initialize(EggEngine _engine)
        {
            base.Initialize(_engine);

            //Add game content here

            GameModelDTO mapDTO = Engine.Content.Load<GameModelDTO>("Models/seventhmodel");
            CurrentMap = new GameMap(mapDTO);
            
            pointer = new MousePointer();
            pointer.Initialize(Engine);
            AddUpdate(pointer);
            AddDraw(pointer);

            //CurrentMap.WorldMatrix.ToList().ForEach(block => block.ForEach(b => b.ToList().ForEach(bl => AddDraw(bl))));
        }
    }
}
