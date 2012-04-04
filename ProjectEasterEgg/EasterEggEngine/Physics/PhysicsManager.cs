using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Engine.Graphics;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class PhysicsManager : IPhysicsManager
    {
        //The direction in which we are going
        Vector3 delta = -Vector3.Normalize(new Vector3(.5f, .5f, (float)((Math.Sqrt(2) / 2) * Math.Cos(MathHelper.ToRadians(30f)))));

        int[][] possibleNeighbours = null;

        private GameMap currentMap;
        public GameMap CurrentMap
        {
            get { return currentMap; }
            set { currentMap = value; }
        }

        public PhysicsManager()
        {
            possibleNeighbours = new int[][] {
                                           new int[] {-1, -1},
                                           new int[] {-1, 0},
                                           new int[] {-1, 1},
                                           new int[] {0, 1},
                                           new int[] {1, 1},
                                           new int[] {1, 0},
                                           new int[] {1, -1},
                                           new int[] {0, -1}
            };
        }

        public void MoveObject(GameEntitySolid character, Vector3 endpoint, GameMap map)
        {
            throw new System.NotImplementedException();
        }

        #region Path Finding
        public int Estimate(GameBlock start, GameBlock end)
        {
            return (int)Math.Floor((end.Position - start.Position).Length());
        }

        public Path<GameBlock> FindPath(GameBlock start, GameBlock destination)
        {
            var closed = new HashSet<GameBlock>();
            var queue = new PriorityQueue<double, Path<GameBlock>>();
            queue.Enqueue(0, new Path<GameBlock>(start));
            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                {
                    continue;
                }
                if (path.LastStep.Position == destination.Position)
                {
                    return path;
                }
                closed.Add(path.LastStep);
                foreach (GameBlock node in GetNeighbours(path.LastStep))
                {
                    double d = 1; //Distance between 2 squares in the grid
                    var newPath = path.AddStep(node, d);
                    queue.Enqueue(newPath.TotalCost + Estimate(node, destination), newPath);
                }
            }
            return null;
        }

        public List<GameBlock> GetNeighbours(GameBlock node)
        {
            List<GameBlock> neighbours = new List<GameBlock>();
            int width = CurrentMap.WorldMatrix.Length - 1;
            int height = CurrentMap.WorldMatrix[0].Length - 1;
            int currentLevel = node.Position.Z;

            for (int i = 0; i < 8; i++)
            {
                //check for out of bounds
                if ((node.Position.X == 0 && possibleNeighbours[i][0] < 0) ||
                    (node.Position.X == width && possibleNeighbours[i][0] > 0) ||
                    (node.Position.Y == 0 && possibleNeighbours[i][1] < 0) ||
                    (node.Position.Y == height && possibleNeighbours[i][1] > 0))
                    continue;

                Position neighbourPosition = new Position(node.Position.X + possibleNeighbours[i][0], node.Position.Y + possibleNeighbours[i][1], currentLevel);
                Position nextPosition = null;
                //Check if the current possible is available, it is only available if the next one is free.
                GameBlock possibleNeighbour = CurrentMap.WorldMatrix[neighbourPosition.X][neighbourPosition.Y][neighbourPosition.Z];
                //Base case for a node.
                GameBlock possibleNext = new GameBlock(BlockType.SOLID, new Position(-1, -1, -1));
                if (i < 7)
                {
                    
                    if (!((node.Position.X == 0 && possibleNeighbours[i + 1][0] < 0) ||
                        (node.Position.X == width && possibleNeighbours[i + 1][0] > 0) ||
                        (node.Position.Y == 0 && possibleNeighbours[i + 1][1] < 0) ||
                        (node.Position.Y == height && possibleNeighbours[i + 1][1] > 0)))
                    {
                        nextPosition = new Position(node.Position.X + possibleNeighbours[i + 1][0], node.Position.Y + possibleNeighbours[i + 1][1], currentLevel);
                        possibleNext = CurrentMap.WorldMatrix[nextPosition.X][nextPosition.Y][nextPosition.Z];
                    }
                    else if (!(node.Position.X == 0 || node.Position.Y == 0))
                    {
                        nextPosition = new Position(node.Position.X + possibleNeighbours[0][0], node.Position.Y + possibleNeighbours[0][1], currentLevel);
                        possibleNext = CurrentMap.WorldMatrix[nextPosition.X][nextPosition.Y][nextPosition.Z];
                    }
                }

                if (possibleNeighbour == null)
                {
                    possibleNeighbour = new GameBlock(BlockType.WALKABLE, neighbourPosition);
                }

                if (possibleNext == null)
                {
                    possibleNext = new GameBlock(BlockType.WALKABLE, nextPosition);
                }

                if (possibleNeighbour.Type != BlockType.SOLID && possibleNext.Type != BlockType.SOLID)
                {
                    neighbours.Add(possibleNeighbour);
                }
            }

            return neighbours;
        }
        #endregion

        public void ClickWorld(Vector2 screen, BlockAction action)
        {
            //The entry position
            Vector3 position = SpriteHelper.fromScreen(screen, CurrentMap.Origin.Z + CurrentMap.WorldMatrix[0][0].Length);

            BlockFaces entry = BlockFaces.TOP;
            while (position.Z >= CurrentMap.Origin.Z)
            {
                //Choose the current Block
                Position currentPosition = new Position(position);
                GameBlock currentBlock = CurrentMap.WorldMatrix[currentPosition.X][currentPosition.Y][currentPosition.Z];

                if (currentBlock.Interactable)
                {
                    currentBlock.Interact(action);
                    return;
                }

                if (currentBlock.Type == BlockType.SOLID)
                {
                    ClickSolidBlock(currentPosition, entry);
                    return;
                }

                position = AdvanceNextBlock(position, ref entry);
            }
        }

        private void ClickSolidBlock(Position currentPosition, BlockFaces entry)
        {
            if (entry == BlockFaces.LEFT && //If left side was clicked
                currentPosition.X < CurrentMap.WorldMatrix.Length - 1 && //and there could be a block in front
                CurrentMap.WorldMatrix[currentPosition.X + 1][currentPosition.Y][currentPosition.Z].Type != BlockType.SOLID) //and we can stand there
            {
                //MoveTo(currentBlock + X - k*Z)
            }

            if (entry == BlockFaces.RIGHT && //If left side was clicked
                currentPosition.Y < CurrentMap.WorldMatrix[0].Length - 1 && //and there could be a block in front
                CurrentMap.WorldMatrix[currentPosition.X][currentPosition.Y + 1][currentPosition.Z].Type != BlockType.SOLID) //and we can stand there
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
            if (stepsX < stepsY && stepsX < stepsZ) //X is closest
            {
                position += delta * stepsX;
                entry = BlockFaces.LEFT;
            }
            else if (stepsY < stepsX && stepsY < stepsZ) //Y is closest
            {
                position += delta * stepsY;
                entry = BlockFaces.RIGHT;
            }
            else //Z is closest
            {
                position += delta * stepsZ;
                entry = BlockFaces.TOP;
            }

            return position;
        }

        private GameBlock FindFloor(Position position)
        {
            do
            {
                GameBlock current = CurrentMap.WorldMatrix[position.X][position.Y][position.Z];
                GameBlock below = CurrentMap.WorldMatrix[position.X][position.Y][position.Z - 1];
                position.Z--;

                if (below.Type == BlockType.SOLID)
                {
                    return current;
                }
            }
            while (position.Z >= CurrentMap.Origin.Z);

            return null;
        }
    }
}
