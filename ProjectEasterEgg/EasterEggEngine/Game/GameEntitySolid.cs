using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class GameEntitySolid : GameEntity, IPhysicsObject
    {
        public Vector3 Position
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

        public GameEntitySolid(EggEngine engine)
            : base(engine)
        { }
    }
}
