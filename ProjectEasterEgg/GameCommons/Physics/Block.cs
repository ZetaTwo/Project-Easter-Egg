using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public class Block
    {
        private Position offset;
        public Position Offset
        {
            get
            {
                return offset;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Block(Position offset)
        {
            this.offset = offset;
        }
    }
}
