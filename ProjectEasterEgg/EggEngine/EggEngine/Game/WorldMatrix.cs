using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.Physics;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class WorldMatrix
    {
        private GameBlock[,,] matrix;
        private Position offset;

        /// <summary>
        /// Elements in the matrix must be set to null before setting them to something else.
        /// They are initialized as null.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public GameBlock this[int x, int y, int z]
        {
            get
            {
                return matrix[x + offset.X, y + offset.Y, z + offset.Z];
            }
            set
            {
                if (value != null && matrix[x + offset.X, y + offset.Y, z + offset.Z] != null)
                {
                    throw new Exception("Collision in world matrix, trying to change an element that isn't null");
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
        public GameBlock this[Position pos]
        {
            get { return this[pos.X,pos.Y,pos.Z]; }
            set { this[pos.X, pos.Y, pos.Z] = value; }
        }

        public int sizeX { get { return matrix.GetLength(0); } }
        public int sizeY { get { return matrix.GetLength(1); } }
        public int sizeZ { get { return matrix.GetLength(2); } }

        public WorldMatrix(BoundingBoxInt bounds)
        {
            offset = -bounds.Min;
            Position size = bounds.Max + offset + Position.One;
            matrix = (GameBlock[,,])Array.CreateInstance(typeof(GameBlock), size.X, size.Y, size.Z);
        }

        /// <summary>
        /// Place the model at it's model.position
        /// </summary>
        /// <param name="model"></param>
        public void placeModel(GameModel model)
        {
            placeModel(model, model.position);
        }

        public void placeModel(GameModel model, Position pos)
        {
            foreach (GameBlock b in model.blocks)
            {
                this[b.Position.X + pos.X, b.Position.Y + pos.Y, b.Position.Z + pos.Z] = b;
            }

            foreach (GameModel subModel in model.subModels)
            {
                placeModel(subModel, pos+subModel.position);
            }
        }
    }
}
