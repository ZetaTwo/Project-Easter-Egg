using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.Physics;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class WorldMatrix : OffsetedMatrix<GameBlock>
    {
        public WorldMatrix(BoundingBoxInt bounds)
            : base(-bounds.Min, bounds.Max - bounds.Min,
                    GameBlock.OutOfBounds,
                    new Exception("Can't set a block outside the bounds of the matrix!"),
                    new Exception("Collision in world matrix, trying to set a block that isn't null"))
        { }

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
            if (modelCanBeAt(model, destination))
            {
                //place it at its destination
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

        public bool modelCanBeAt(GameModel model, Position position)
        {
            return model.getAllRelativeBlockPositions().Offset(position).All(
                pos =>
                    this[pos] == null ||
                    //this[pos].Type == BlockType.LADDER || //TODO: support ladder
                    this[pos].hasParent(model));
        }

        public bool modelCanStandAt(GameModel model, Position position)
        {
            bool _;
            return modelCanStandAt(model, position, out _);
        }

        public bool modelCanStandAt(GameModel model, Position position, out bool canBeAt)
        {
            canBeAt = modelCanBeAt(model, position);
            return canBeAt &&
                this[position + Position.Down] != null &&
                (
                    this[position + Position.Down].Type == BlockType.WALKABLE ||
                    this[position + Position.Down].Type == BlockType.STAIRS
                );
        }
    }
}
