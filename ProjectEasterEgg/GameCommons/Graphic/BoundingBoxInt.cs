using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public class BoundingBoxInt
    {
        Position min;
        public Position Min { get { return min; } }
        Position max;
        private float fullDepth;
        public Position Max { get { return max; } }





        public BoundingBoxInt()
        { }

        public BoundingBoxInt(IEnumerable<Position> positions)
        {
            addPos(positions);
        }

        public BoundingBoxInt(Position min, Position max)
        {
            addPos(min);
            addPos(max);
        }





        public void addPos(IEnumerable<Position> positions)
        {
            foreach (Position pos in positions)
            {
                if (min == null)
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

        public void addPos(Position pos)
        {
            if (min == null)
            {
                min = pos.Clone();
                max = pos.Clone();
            }
            else
            {
                min.X = Math.Min(pos.X, min.X);
                min.Y = Math.Min(pos.Y, min.Y);
                min.Z = Math.Min(pos.Z, min.Z);
                max.X = Math.Max(pos.X, max.X);
                max.Y = Math.Max(pos.Y, max.Y);
                max.Z = Math.Max(pos.Z, max.Z);
            }

            updateFullDepth();
        }

        private void updateFullDepth()
        {
            Vector3 v = Max.ToVector3()-Min.ToVector3();
            fullDepth = v.Project(new Vector3(0.62f, 0.608f, 0.5f)).Length();
        }

        /// <summary>
        /// Calculates the distance from the camera plane,
        /// assuming the camera it is an isometric camera tilted down 30 degrees.
        /// </summary>
        /// <param name="pos">Position to calculate distance to camera plane of</param>
        /// <returns>A value between 0.1 and 0.9, where higher is farther away</returns>
        public float getRelativeDepthOf(Position pos)
        {
            /* Moving forward one step in X when viewed from directly above
             * moves you sqrt(2)/2 closer to the "bottom corner line".
             * However, when viewed from an angle (60 degrees tilted)
             * you actually moved (sqrt(2)/2)*cos(30) closer to the screen.
             * (sqrt(2)/2)*cos(30) = 0.6123724356957945245493210186764728479914868701641675
             *  
             *      / \
             *    / \ / \
             *    \ / \ /
             *   ___\_/_______ <- bottom corner line
             * 
             * The same goes for movement in Y.
             * 
             * Simlilarly movement in Z moves you 1*cos(60) closer to the screen
             * 1*cos(60) = 0.5
             * 
             */
            float depth = (pos.X - Min.X) * 0.62f; //prioritize X over Y
            depth += (pos.Y - Min.Y) * 0.608f;
            depth += (pos.Z - Min.Z) * 0.5f;
            return 0.9f - depth/fullDepth*0.8f;
        }
    }
}
