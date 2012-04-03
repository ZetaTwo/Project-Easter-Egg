using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class Node : IHasNeighbours<Node>
    {
        int x;
        public int X { get { return x; } }

        int y;
        public int Y { get { return y; } }

        int z;
        public int Z { get { return z; } }

        //0=walkable
        //1=not walkable
        //2=stair up
        //3=stair down
        public int type;

        Node[][][] worldMatrix;

        public IEnumerable<Node> Neighbours { get { return this.getNeighbours(); } }

        public Node(Node[][][] _worldMatrix, int _type, int _x , int _y , int _z)
        {
            worldMatrix = _worldMatrix;
            type = _type;
            x = _x;
            y = _y;
            z = _z;
        }

        public Node(int _type, int _x, int _y, int _z)
        {
            type = _type;
            x = _x;
            y = _y;
            z = _z;
        }

        public List<Node> getNeighbours()
        {
            int[][] possibleNeighbours = new int[][] {
                new int[] {-1,-1},
                new int[] {-1,0},
                new int[] {0,-1},
                new int[] {1,0},
                new int[] {0,1},
                new int[] {1,1},
                new int[] {-1,1},
                new int[] {1,-1}
            };
            List<Node> neighbours = new List<Node>();
            int width = worldMatrix.Length;
            int height = worldMatrix[0].Length;
            int currentLevel = this.z;

            for (int i = 0; i < 8; i++)
            {
                //check for out of bounds
                if ((this.x == 0 && possibleNeighbours[i][0] < 0) ||
                    (this.x == width && possibleNeighbours[i][0] > 0) ||
                    (this.y == 0 && possibleNeighbours[i][1] < 0) ||
                    (this.y == height && possibleNeighbours[i][0] > 0))
                    continue;
                Node possibleNeighbour = worldMatrix[this.X+possibleNeighbours[i][0]][this.Y+possibleNeighbours[i][1]][currentLevel];
                if(possibleNeighbour.type != 1)
                    neighbours.Add(possibleNeighbour);
            }

            return neighbours;
        }
    }
}
