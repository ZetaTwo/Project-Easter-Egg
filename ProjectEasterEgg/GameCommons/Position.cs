using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class Position
    {
        public int X;
        public int Y;
        public int Z;

        public Position(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Position p = obj as Position;
            if ((System.Object)p == null)
            {
                return false;
            }

            return p.X == X && p.Y == Y && p.Z == Z;
        }
    }
}
