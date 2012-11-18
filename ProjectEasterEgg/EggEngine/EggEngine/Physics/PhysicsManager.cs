using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Commons.Graphic;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class PhysicsManager
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        //The direction in which we are going
        private static readonly Vector3 delta = -Vector3.Normalize(new Vector3(.5f, .5f, (float)((Math.Sqrt(2) / 2) * Math.Cos(MathHelper.ToRadians(30f)))));

        public GameMap CurrentMap
        {
            get { return Engine.World.CurrentMap; }
            //set { world = value; }
        }

        public PhysicsManager(EggEngine engine)
        {
            this.engine = engine;
        }

        public void MoveObject(GameEntitySolid character, Position endpoint, GameMap map)
        {
            throw new System.NotImplementedException();
        }

        #region Path Finding
        public int Estimate(Position start, Position end)
        {
            return (int)Math.Floor((end - start).Length());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <returns>A linked list of positions to visit on the way, or <code>null</code>
        /// if no feasible path was found.</returns>
        public LinkedList<Position> FindPath(GameModel model, Position start, Position destination)
        {
            var closed = new HashSet<Position>();
            var queue = new PriorityQueue<double, Path<Position>>();
            queue.Enqueue(0, new Path<Position>(start));
            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                {
                    continue;
                }
                if (path.LastStep == destination)
                {
                    return new LinkedList<Position>(path.Reverse().Diff());
                }
                closed.Add(path.LastStep);
                var a = GetNeighbours(model, path.LastStep);
                foreach (Position node in a)
                {
                    double d = 1; //Distance between 2 squares in the grid
                    var newPath = path.AddStep(node, d);
                    queue.Enqueue(newPath.TotalCost + Estimate(node, destination), newPath);
                }
            }
            return null;
        }

        private OffsetedMatrix<bool> neighbours = new OffsetedMatrix<bool>(Position.One, Position.One * 3);
        private IEnumerable<Position> GetNeighbours(GameModel model, Position position)
        {
            WorldMatrix worldMatrix = model.ParentMap().WorldMatrix;
            neighbours.Fill(_ => false);

            {
                bool NW, NE, SE, SW;
                neighbours[Position.NW] = worldMatrix.modelCanStandAt(model, position + Position.NW, out NW);
                neighbours[Position.NE] = worldMatrix.modelCanStandAt(model, position + Position.NE, out NE);
                neighbours[Position.SE] = worldMatrix.modelCanStandAt(model, position + Position.SE, out SE);
                neighbours[Position.SW] = worldMatrix.modelCanStandAt(model, position + Position.SW, out SW);

                neighbours[Position.N] = NW && NE && worldMatrix.modelCanStandAt(model, position + Position.N);
                neighbours[Position.E] = NE && SE && worldMatrix.modelCanStandAt(model, position + Position.E);
                neighbours[Position.S] = SE && SW && worldMatrix.modelCanStandAt(model, position + Position.S);
                neighbours[Position.W] = SW && NW && worldMatrix.modelCanStandAt(model, position + Position.W);

                //if we are standing on a stair, try to add the four neighbours a level down
                if (worldMatrix[position + Position.Down].Type == BlockType.STAIRS)
                {
                    neighbours[Position.NW + Position.Down] = NW && worldMatrix.modelCanStandAt(model, position + Position.NW + Position.Down);
                    neighbours[Position.NE + Position.Down] = NE && worldMatrix.modelCanStandAt(model, position + Position.NE + Position.Down);
                    neighbours[Position.SE + Position.Down] = SE && worldMatrix.modelCanStandAt(model, position + Position.SE + Position.Down);
                    neighbours[Position.SW + Position.Down] = SW && worldMatrix.modelCanStandAt(model, position + Position.SW + Position.Down);
                }
            }

            //check for stairs in the four directions at the same level
            if (worldMatrix.modelCanBeAt(model, position + Position.Up))
            {
                neighbours[Position.NW + Position.Up] =
                    worldMatrix[position + Position.NW].Type == BlockType.STAIRS &&
                    worldMatrix.modelCanStandAt(model, Position.NW + Position.Up);
                neighbours[Position.NE + Position.Up] =
                    worldMatrix[position + Position.NE].Type == BlockType.STAIRS &&
                    worldMatrix.modelCanStandAt(model, Position.NE + Position.Up);
                neighbours[Position.SE + Position.Up] =
                    worldMatrix[position + Position.SE].Type == BlockType.STAIRS &&
                    worldMatrix.modelCanStandAt(model, Position.SE + Position.Up);
                neighbours[Position.SW + Position.Up] =
                    worldMatrix[position + Position.SW].Type == BlockType.STAIRS &&
                    worldMatrix.modelCanStandAt(model, Position.SW + Position.Up);
            }


            for (int x = neighbours.Min.X; x < neighbours.Max.X; x++)
            {
                for (int y = neighbours.Min.Y; y < neighbours.Max.Y; y++)
                {
                    for (int z = neighbours.Min.Z; z < neighbours.Max.Z; z++)
                    {
                        if (neighbours[x, y, z])
                        {
                            yield return position + new Position(x, y, z);
                        }
                    }
                }
            }
        }
        #endregion

        public IEnumerable<Tuple<GameBlock, BlockFaces>> GetBlocksUnderPoint(Point pointInProjSpace, GameTime gameTime)
        {
            Position position = CoordinateTransform.ProjToObjectSpace(pointInProjSpace,
                CurrentMap.WorldMatrix.Max.Z+1).ToPosition();
            BlockFaces face = BlockFaces.TOP;
            pointInProjSpace = pointInProjSpace.Subtract(Constants.blockDrawOffset.ToXnaPoint());

            while (position.Z >= CurrentMap.Bounds.Min.Z && // so that we return even if the mouse wasn't
                position.X >= CurrentMap.Bounds.Min.X &&    // above the WorldMatrix at all
                position.Y >= CurrentMap.Bounds.Min.Y)
            {
                GameBlock currentBlock = CurrentMap.WorldMatrix[position];
                if (currentBlock != GameBlock.OutOfBounds &&
                    currentBlock != GameBlock.Empty)
                {
                    //TODO:1: only return if it hits even with move offset taken into account
                    //      possibly also check that the pixel hit (or a few around it) isn't transparent.
                    yield return Tuple.Create(currentBlock, face);
                }

                else if (BlockRegions.WholeBlock.IsVisible(pointInProjSpace.Subtract(CoordinateTransform
                    .ObjectToProjectionSpace(position + Position.Down).ToXnaPoint()).ToSDPoint()))
                {
                    position = position + Position.Down;
                    face = BlockFaces.TOP;
                }
                else if (BlockRegions.WholeBlock.IsVisible(pointInProjSpace.Subtract(CoordinateTransform
                    .ObjectToProjectionSpace(position + Position.NE).ToXnaPoint()).ToSDPoint()))
                {
                    position = position + Position.NE;
                    face = BlockFaces.LEFT;
                }
                else if (BlockRegions.WholeBlock.IsVisible(pointInProjSpace.Subtract(CoordinateTransform
                    .ObjectToProjectionSpace(position + Position.NW).ToXnaPoint()).ToSDPoint()))
                {
                    position = position + Position.NW;
                    face = BlockFaces.RIGHT;
                }
                else
                {
                    throw new Exception("This should never happen.");
                }
            }
        }

        private void ClickBlock(Position currentPosition, BlockFaces entry)
        {
            if (entry == BlockFaces.LEFT && //If left side was clicked
                currentPosition.X < CurrentMap.WorldMatrix.Size.X - 1 && //and there could be a block in front
                CurrentMap.WorldMatrix[currentPosition + new Position(1,0,0)].Type != BlockType.SOLID) //and we can stand there
            {
                //MoveTo(currentBlock + X - k*Z)
            }

            if (entry == BlockFaces.RIGHT && //If left side was clicked
                currentPosition.Y < CurrentMap.WorldMatrix.Size.Y - 1 && //and there could be a block in front
                CurrentMap.WorldMatrix[currentPosition + new Position(0,1,0)].Type != BlockType.SOLID) //and we can stand there
            {
                //MoveTo(currentBlock + Y - k*Z)
            }

            //MoveTo(currentBlock + 1*Z)
        }

        private static Vector3 AdvanceNextBlock(Vector3 position, ref BlockFaces entry)
        {
            //Proceed to next Block
            //calculate step lengths in multiples of delta
            //TODO: Vector3 steps = (position.Floor() - position)/delta;
            float stepsX = (float)(position.X.Floor() - position.X) / delta.X;
            float stepsY = (float)(position.Y.Floor() - position.Y) / delta.Y;
            float stepsZ = (float)(position.Z.Floor() - position.Z) / delta.Z;

            //Check which is closest
            if (stepsX != 0 && stepsX < stepsY && stepsX < stepsZ) //X is closest
            {
                position += delta * stepsX;
                entry = BlockFaces.LEFT;
            }
            else if (stepsY != 0 && stepsY < stepsX && stepsY < stepsZ) //Y is closest
            {
                position += delta * stepsY;
                entry = BlockFaces.RIGHT;
            }
            else //Z is closest
            {
                if (stepsZ == 0)
                {
                    stepsZ = 1f;
                }
                position += delta * stepsZ;
                entry = BlockFaces.TOP;
            }

            return position;
        }

        private GameBlock FindFloor(Position position)
        {
            do
            {
                GameBlock current = CurrentMap.WorldMatrix[position];
                GameBlock below = CurrentMap.WorldMatrix[position - new Position(0,0,-1)];
                position.Z--;

                if (below.Type == BlockType.SOLID)
                {
                    return current;
                }
            }
            while (position.Z >= CurrentMap.Bounds.Min.Z);

            return null;
        }
    }
}
