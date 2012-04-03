using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Physics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Game;

namespace Mindstep.EasterEgg.Engine.Game
{
    class GameMap : Map
    {
        EggEngine engine;

        List<IEntityUpdate> updateObjects = new List<IEntityUpdate>();

        public void Update(GameTime gameTime)
        {
            foreach (IEntityUpdate update in updateObjects)
            {
                update.Update(gameTime);
            }
        }

        public void Initialize(EggEngine _engine)
        {
            engine = _engine;
        }

        public void AddUpdate(IEntityUpdate entity)
        {
            updateObjects.Add(entity);
        }

        public void RemoveUpdate(IEntityUpdate entity)
        {
            updateObjects.Remove(entity);
        }
    }
}
