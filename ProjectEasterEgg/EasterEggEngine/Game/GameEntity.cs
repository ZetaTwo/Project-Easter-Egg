using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class GameEntity : IGameEntity, IDrawable
    {
        public abstract void Draw();

        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        public GameEntity(EggEngine _engine)
        {
            engine = _engine;
        }
    }
}
