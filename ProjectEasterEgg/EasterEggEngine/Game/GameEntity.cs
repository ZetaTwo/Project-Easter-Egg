using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Game;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Game;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class GameEntity : IEntityUpdate
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        public virtual void Initialize(EggEngine _engine)
        {
            engine = _engine;
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
