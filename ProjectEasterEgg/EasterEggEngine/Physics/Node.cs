using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class Node
    {
        private Position position;
        public Position Position
        {
            get { return position; }
        }

        //0=walkable
        //1=not walkable
        //2=stair up
        //3=stair down
        public int type;

        public Node(int _type, Position position)
        {
            type = _type;
            this.position = position;
        }

        public List<Node> getNeighbours(Node[][][] worldMatrix)
        {
            int[][] possibleNeighbours = new int[][] {
                new int[] {-1, -1},
                new int[] {-1, 0},
                new int[] {0, -1},
                new int[] {1, 0},
                new int[] {0, 1},
                new int[] {1, 1},
                new int[] {-1, 1},
                new int[] {1, -1}
            };
            List<Node> neighbours = new List<Node>();
            int width = worldMatrix.Length - 1;
            int height = worldMatrix[0].Length - 1;
            int currentLevel = Position.Z;

            for (int i = 0; i < 8; i++)
            {
                //check for out of bounds
                if ((Position.X == 0 && possibleNeighbours[i][0] < 0) ||
                    (Position.X == width && possibleNeighbours[i][0] > 0) ||
                    (Position.Y == 0 && possibleNeighbours[i][1] < 0) ||
                    (Position.Y == height && possibleNeighbours[i][1] > 0))
                    continue;
                Node possibleNeighbour = worldMatrix[Position.X + possibleNeighbours[i][0]][Position.Y + possibleNeighbours[i][1]][currentLevel];
                if(possibleNeighbour.type != 1)
                    neighbours.Add(possibleNeighbour);
            }

            return neighbours;
        }
    }
}
