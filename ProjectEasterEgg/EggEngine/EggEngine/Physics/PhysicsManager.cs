using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;

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
        Vector3 delta = -Vector3.Normalize(new Vector3(.5f, .5f, (float)((Math.Sqrt(2) / 2) * Math.Cos(MathHelper.ToRadians(30f)))));

        private readonly Position[] possibleNeighbourOffsets = new Position[] {
                Position.N,
                Position.NE,
                Position.E,
                Position.SE,
                Position.S,
                Position.SW,
                Position.W,
                Position.NW,
            };

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

        private IEnumerable<Position> GetNeighbours(GameModel model, Position position)
        {

            List<Position> neighbours = new List<Position>();
            bool NW, NE, SE, SW;

            GameBlock blockBelow = model.ParentMap().WorldMatrix[position + Position.Down];
            if (blockBelow.Type == BlockType.STAIRS)
            {
                neighbours.AddRange(GetNeighbours(model, position + Position.Down));
            }
            if (blockBelow.Type == BlockType.LADDER)
            {
                neighbours.Add(position + Position.Down);
            }

            if (NW = model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.NW))
            {
                neighbours.Add(position + Position.NW);
            }
            if (NE = model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.NE))
            {
                neighbours.Add(position + Position.NE);
            }
            if (SE = model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.SE))
            {
                neighbours.Add(position + Position.SE);
            }
            if (SW = model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.SW))
            {
                neighbours.Add(position + Position.SW);
            }
            if (NW && NE && model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.N))
            {
                neighbours.Add(position + Position.N);
            }
            if (NE && SE && model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.E))
            {
                neighbours.Add(position + Position.E);
            }
            if (SE && SW && model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.S))
            {
                neighbours.Add(position + Position.S);
            }
            if (SW && NW && model.ParentMap().WorldMatrix.modelCanStandAt(model, position + Position.W))
            {
                neighbours.Add(position + Position.W);
            }

            //int width = CurrentMap.WorldMatrix.sizeX - 1;
            //int height = CurrentMap.WorldMatrix.sizeY - 1;
            //int currentLevel = node.Position.Z;

            //for (int i = 0; i < 8; i++)
            //{
            //    //check for out of bounds
            //    if ((node.Position.X == 0 && possibleNeighbours[i][0] < 0) ||
            //        (node.Position.X == width && possibleNeighbours[i][0] > 0) ||
            //        (node.Position.Y == 0 && possibleNeighbours[i][1] < 0) ||
            //        (node.Position.Y == height && possibleNeighbours[i][1] > 0))
            //        continue;

            //    Position neighbourPosition = new Position(node.Position.X + possibleNeighbours[i][0], node.Position.Y + possibleNeighbours[i][1], currentLevel);
            //    Position nextPosition = null;
            //    //Check if the current possible is available, it is only available if the next one is free.
            //    GameBlock possibleNeighbour = CurrentMap.WorldMatrix[neighbourPosition.X,neighbourPosition.Y,neighbourPosition.Z];
            //    //Base case for a node.
            //    GameBlock possibleNext = new GameBlock(BlockType.SOLID, -Position.One);
            //    if (i < 7)
            //    {
                    
            //        if (!((node.Position.X == 0 && possibleNeighbours[i + 1][0] < 0) ||
            //            (node.Position.X == width && possibleNeighbours[i + 1][0] > 0) ||
            //            (node.Position.Y == 0 && possibleNeighbours[i + 1][1] < 0) ||
            //            (node.Position.Y == height && possibleNeighbours[i + 1][1] > 0)))
            //        {
            //            nextPosition = new Position(node.Position.X + possibleNeighbours[i + 1][0], node.Position.Y + possibleNeighbours[i + 1][1], currentLevel);
            //            possibleNext = CurrentMap.WorldMatrix[nextPosition];
            //        }
            //        else if (!(node.Position.X == 0 || node.Position.Y == 0))
            //        {
            //            nextPosition = new Position(node.Position.X + possibleNeighbours[0][0], node.Position.Y + possibleNeighbours[0][1], currentLevel);
            //            possibleNext = CurrentMap.WorldMatrix[nextPosition];
            //        }
            //    }

            //    if (possibleNeighbour == null)
            //    {
            //        possibleNeighbour = new GameBlock(BlockType.WALKABLE, neighbourPosition);
            //    }

            //    if (possibleNext == null)
            //    {
            //        possibleNext = new GameBlock(BlockType.WALKABLE, nextPosition);
            //    }

            //    if (possibleNeighbour.Type != BlockType.SOLID && possibleNext.Type != BlockType.SOLID)
            //    {
            //        neighbours.Add(possibleNeighbour);
            //    }
            //}

            return neighbours;
        }
        #endregion

        public void ClickWorld(Point pointInProjSpace, EasterEgg.Commons.Graphic.Camera camera, BlockAction action)
        {
            //The entry position
            //screen.Y *= 1;
            //screen.X *= -1;
            //GameBlock hitBlock = getBlockUnder(pointInProjSpace, b => true);
            Vector3 position = CoordinateTransform.ProjToObjectSpace(pointInProjSpace, CurrentMap.WorldMatrix.maxZ);
            if (position.X < CurrentMap.Bounds.Min.X || position.X > CurrentMap.Bounds.Max.X ||
               position.Y < CurrentMap.Bounds.Min.Y || position.Y > CurrentMap.Bounds.Max.Y ||
               position.Z < CurrentMap.Bounds.Min.Z || position.Z > CurrentMap.Bounds.Max.Z)
            {
                return;
            }

            BlockFaces entry = BlockFaces.TOP;
            while (position.Z >= CurrentMap.Bounds.Min.Z)
            {
                //Choose the current Block
                Position currentPosition = new Position(position) - CurrentMap.Bounds.Min;
                GameBlock currentBlock = CurrentMap.WorldMatrix[currentPosition];

                if (currentBlock != null && currentBlock.Interactable)
                {
                    currentBlock.Interact(action);
                    return;
                }

                if (currentBlock != null && currentBlock.Type == BlockType.SOLID)
                {
                    ClickSolidBlock(currentPosition, entry);
                    return;
                }

                position = AdvanceNextBlock(position, ref entry);
            }
        }

        private GameBlock getBlockUnder(Point pointInProjSpace, Predicate<GameBlock> condition)
        {
            for (int layer = CurrentMap.WorldMatrix.maxZ; layer > CurrentMap.WorldMatrix.minZ; layer--)
            {
                {//above
                    Position positionAbove = CoordinateTransform.ProjToObjectSpace(pointInProjSpace, layer + 1).ToPosition();
                    positionAbove.Z--;
                    GameBlock block = CurrentMap.WorldMatrix[positionAbove];
                    if (block != null && condition(block))
                    {
                        return block;
                    }
                }

            }
            return null;
        }

        private void ClickSolidBlock(Position currentPosition, BlockFaces entry)
        {
            if (entry == BlockFaces.LEFT && //If left side was clicked
                currentPosition.X < CurrentMap.WorldMatrix.sizeX - 1 && //and there could be a block in front
                CurrentMap.WorldMatrix[currentPosition + new Position(1,0,0)].Type != BlockType.SOLID) //and we can stand there
            {
                //MoveTo(currentBlock + X - k*Z)
            }

            if (entry == BlockFaces.RIGHT && //If left side was clicked
                currentPosition.Y < CurrentMap.WorldMatrix.sizeY - 1 && //and there could be a block in front
                CurrentMap.WorldMatrix[currentPosition + new Position(0,1,0)].Type != BlockType.SOLID) //and we can stand there
            {
                //MoveTo(currentBlock + Y - k*Z)
            }

            //MoveTo(currentBlock + 1*Z)
        }

        private Vector3 AdvanceNextBlock(Vector3 position, ref BlockFaces entry)
        {
            //Proceed to next Block
            //calculate step lengths in multiples of delta
            float stepsX = (float)(Math.Floor(position.X) - position.X) / delta.X;
            float stepsY = (float)(Math.Floor(position.Y) - position.Y) / delta.Y;
            float stepsZ = (float)(Math.Floor(position.Z) - position.Z) / delta.Z;

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
