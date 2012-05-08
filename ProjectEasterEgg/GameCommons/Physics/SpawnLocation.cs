using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.Physics
{
    public class SpawnLocation : IPositionable
    {
        private string name;

        private Position position;
        public Position Position
        {
            get { return position; }
        }

        public SpawnLocation(Position position, string name)
        {
            this.position = position;
            this.name = name;
        }
    }
}
