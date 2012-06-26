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
        private EggEngine engine;
        public EggEngine Engine { get { return engine; } }
        public GameMap CurrentMap { get { return Engine.World.CurrentMap; } }





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

        public Path<Position> FindPath(GameModel model, Position start, Position destination)
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
                    return path;
                }
                closed.Add(path.LastStep);
                foreach (Position node in GetNeighbours(model, path.LastStep))
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

        public IEnumerable<GameBlock> GetBlocksUnderPoint(Point pointInProjSpace)
        {
            var positions = CurrentMap.getAllRelativeBlockPositions();
            System.Drawing.Point sdPoint = pointInProjSpace.ToSDPoint();

            foreach (Position pos in positions.OrderBy(pos => CurrentMap.Bounds.getRelativeDepthOf(pos)))
            {
                System.Drawing.Region blockRegion = BlockRegions.WholeBlock.Offset(
                    CoordinateTransform.ObjectToProjectionSpace(pos).ToXnaPoint());
                if (blockRegion.IsVisible(sdPoint))
                {
                    yield return CurrentMap.WorldMatrix[pos];
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
