using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine
{
    public class GameEntitySolid : GameEntity, IPhysicsObject
    {
        public Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
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
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
