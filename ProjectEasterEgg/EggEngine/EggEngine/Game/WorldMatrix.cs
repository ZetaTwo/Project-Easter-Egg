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
        private GameBlock[, ,] matrix;
        private Position offset;

        /// <summary>
        /// Elements in the matrix must be set to null before setting them to something else.
        /// They are initialized as null.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>null if the space is empty or outside the matrix</returns>
        public GameBlock this[int x, int y, int z]
        {
            get
            {
                if (!insideBounds(x, y, z))
                {
                    return GameBlock.OutOfBounds;
                }
                return matrix[x + offset.X, y + offset.Y, z + offset.Z];
            }
            set
            {
                if (!insideBounds(x, y, z))
                {
                    throw new Exception("Can't set a block outside the bounds of the matrix!");
                }
                if (value != null && matrix[x + offset.X, y + offset.Y, z + offset.Z] != null)
                {
                    throw new Exception("Collision in world matrix, trying to set a block that isn't null");
                }
                matrix[x + offset.X, y + offset.Y, z + offset.Z] = value;
            }
        }

        private bool insideBounds(int x, int y, int z)
        {
            return x.BetweenInclusive(minX, maxX) &&
                   y.BetweenInclusive(minY, maxY) &&
                   z.BetweenInclusive(minZ, maxZ);
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
            get { return this[pos.X, pos.Y, pos.Z]; }
            set { this[pos.X, pos.Y, pos.Z] = value; }
        }

        public int minX { get { return -offset.X; } }
        public int minY { get { return -offset.Y; } }
        public int minZ { get { return -offset.Z; } }
        public int sizeX { get { return matrix.GetLength(0); } }
        public int sizeY { get { return matrix.GetLength(1); } }
        public int sizeZ { get { return matrix.GetLength(2); } }
        public int maxX { get { return minX + sizeX - 1; } }
        public int maxY { get { return minY + sizeY - 1; } }
        public int maxZ { get { return minZ + sizeZ - 1; } }





        public WorldMatrix(BoundingBoxInt bounds)
        {
            offset = -bounds.Min;
            Position size = bounds.Max + offset + Position.One;
            matrix = (GameBlock[, ,])Array.CreateInstance(typeof(GameBlock), size.X, size.Y, size.Z);
        }

        public WorldMatrix(GameMap gameMap)
            : this(gameMap.Bounds)
        {
            tryToPlaceModel(gameMap, Position.Zero);
        }





        public bool tryToPlaceModel(GameModel model, Position absolutePosition)
        {
            if (!allEmpty(model, absolutePosition))
            {
                return false;
            }

            forcePlaceModel(model, absolutePosition);
            return true;
        }

        private void forcePlaceModel(GameModel model, Position absolutePosition)
        {
            foreach (GameBlock b in model.blocks)
            {
                this[b.Position + absolutePosition] = null;
                this[b.Position + absolutePosition] = b;
            }

            foreach (GameModel subModel in model.subModels)
            {
                forcePlaceModel(subModel, absolutePosition + subModel.Position);
            }
        }

        /// <summary>
        /// Checks whether the positions of all the blocks and submodels' blocks in a model are empty
        /// </summary>
        /// <param name="model"></param>
        /// <param name="absolutePosition"></param>
        /// <returns>true if all the positions are null in the world matrix</returns>
        public bool allEmpty(GameModel model, Position absolutePosition)
        {
            return model.getAllRelativeBlockPositions().Offset(absolutePosition).All(pos => this[pos] == null);
        }

        /// <summary>
        /// Begins moving a model by reserving space for it at its destination.
        /// </summary>
        /// <param name="model">Model to be moved</param>
        /// <param name="destination">Absolute position it should be moved to</param>
        /// <returns>True if the model could be moved</returns>
        public bool beginMoveModel(GameModel model, Position destination)
        {
            // if space is empty or belongs to the model itself
            if (canBeAt(model, destination))
            {
                //place it at end position
                forcePlaceModel(model, destination);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ends moving a model by nulling all blocks in the physics matrix
        /// no longer occupied by the model, also sets the model.position to the destination
        /// </summary>
        /// <param name="model"></param>
        /// <param name="destination"></param>
        public void endMoveModel(GameModel model, Position destination)
        {
            if (!isAt(model, destination))
            {
                throw new ArgumentException("Trying to finish a model move which hasn't been properly begun, " +
                        " or someone has tampered with the matrix directly");
            }

            // remove model from start position
            foreach (Position blockPos in model.getAllRelativeBlockPositions().Offset(model.Position))
            {
                this[blockPos] = null;
            }
            //place it at end position
            forcePlaceModel(model, destination);
            model.position = destination;
        }

        public bool isAt(GameModel model, Position position)
        {
            return model.getAllRelativeBlockPositions().Offset(position).All(
                pos => this[pos].hasParent(model));
        }

        public bool canBeAt(GameModel model, Position position)
        {
            return model.getAllRelativeBlockPositions().Offset(position).All(
                pos => this[pos] == null || this[pos].hasParent(model));
        }

        public bool modelCanStandAt(GameModel model, Position position)
        {
            return canBeAt(model, position) &&
                this[position + Position.Down] != null &&
                this[position + Position.Down].Type == BlockType.WALKABLE;
        }
    }
}
