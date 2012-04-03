using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine
{
    public interface IPhysicsObject
    {
        Vector3 Position
        {
            get;
            set;
        }

        List<Block> Blocks
        {
            get;
            set;
        }
    }
}
