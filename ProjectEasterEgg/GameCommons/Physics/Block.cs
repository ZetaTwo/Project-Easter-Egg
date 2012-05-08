using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Commons.Physics
{
    public abstract class Block : IPositionable
    {
        private Position position;
        public Position Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Block(Position position)
        {
            this.position = position;
        }
    }
}
