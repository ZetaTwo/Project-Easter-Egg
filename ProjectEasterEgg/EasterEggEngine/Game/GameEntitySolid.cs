using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class GameEntitySolid : GameEntityDrawable, IPhysicsObject
    {
        public Position Position
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public List<Block> Blocks
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
