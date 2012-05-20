using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class OffsetedMatrix<T>
    {
        private T[, ,] matrix;
        private Position offset;
        private Position size;
        private T getOutOfBounds;
        private Exception setOutOfBounds;
        private Exception setCollision;

        /// <summary>
        /// Elements in the matrix must be set to null before setting them to something else.
        /// They are initialized as null.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>null if the space is empty or outside the matrix</returns>
        public T this[int x, int y, int z]
        {
            get
            {
                if (!insideBounds(x, y, z))
                {
                    return getOutOfBounds;
                }
                return matrix[x + offset.X, y + offset.Y, z + offset.Z];
            }
            set
            {
                if (!insideBounds(x, y, z))
                {
                    throw setOutOfBounds;
                }
                if (value != null && matrix[x + offset.X, y + offset.Y, z + offset.Z] != null)
                {
                    throw setCollision;
                }
                matrix[x + offset.X, y + offset.Y, z + offset.Z] = value;
            }
        }

        /// <summary>
        /// Elements in the matrix must be set to null before setting them to something else.
        /// They are initialized as null.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public T this[Position pos]
        {
            get { return this[pos.X, pos.Y, pos.Z]; }
            set { this[pos.X, pos.Y, pos.Z] = value; }
        }

        public Position Min { get { return -offset; } }
        public Position Size { get { return size; } }
        public Position Max { get { return Min+Size; } }





        public OffsetedMatrix(Position offset, Position size,
            T getOutOfBounds, Exception setOutOfBounds, Exception setCollision)
        {
            this.offset = offset;
            this.size = size;
            this.getOutOfBounds = getOutOfBounds;
            this.setOutOfBounds = setOutOfBounds;
            this.setCollision = setCollision;

            matrix = (T[, ,])Array.CreateInstance(typeof(T), size.X+1, size.Y+1, size.Z+1);
        }





        private bool insideBounds(int x, int y, int z)
        {
            return x.BetweenInclusive(Min.X, Max.X) &&
                   y.BetweenInclusive(Min.Y, Max.Y) &&
                   z.BetweenInclusive(Min.Z, Max.Z);
        }

        public void Fill(Func<Position, T> fillFunction)
        {
            for (int x = Min.X; x < Max.X; x++)
            {
                for (int y = Min.Y; y < Max.Y; y++)
                {
                    for (int z = Min.Z; z < Max.Z; z++)
                    {
                        this[x, y, z] = fillFunction(new Position(x, y, z));
                    }
                }
            }
        }
    }
}
