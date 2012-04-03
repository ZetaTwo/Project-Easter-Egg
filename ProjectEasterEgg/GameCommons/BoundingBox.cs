using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class BoundingBoxInt
    {
        Position min;
        public Position Min { get { return min; } }
        Position max;
        public Position Max { get { return max; } }

        public BoundingBoxInt(IEnumerable<Position> positions)
        {
            foreach (Position pos in positions)
            {
                if (min == null || max == null)
                {
                    min = pos.Clone();
                    max = pos.Clone();
                }
                else
                {
                    addPos(pos);
                }
            }
        }

        private void addPos(Position pos)
        {
            min.X = Math.Min(pos.X, min.X);
            min.Y = Math.Min(pos.Y, min.Y);
            min.Z = Math.Min(pos.Z, min.Z);
            max.X = Math.Max(pos.X, min.X);
            max.Y = Math.Max(pos.Y, min.Y);
            max.Z = Math.Max(pos.Z, min.Z);
        }

        public float getDepth(Position pos)
        {
            return 0.5f;
        }
    }
}
