using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class Node //: IHasNeighbours<Node>
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

        int[][][] worldMatrix;

        public IEnumerable<int> Neighbours { get { return this.getNeighbours(worldMatrix); } }

        public Node(int[][][] _worldMatrix, int _type)
        {
            //worldMatrix = _worldMatrix;
            type = _type;
        }

        public static void main(String[] args)
        {
           int[][][] testMatrix = new int[][][] { new int[][] { new int[] {0,0,0,0,0,0},
                                                                    new int[] {0,0,0,0,0,0},
                                                                    new int[] {0,0,0,0,0,0},
                                                                    new int[] {0,0,0,0,0,0},
                                                                    new int[] {0,0,0,0,0,0}}};
            Node n = new Node(testMatrix, 0);
            n.getNeighbours(testMatrix);
            Console.WriteLine("hej");

        }

        private IEnumerable<int> getNeighbours(int[][][] worldMatrix)
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
            List<int> neighbours = new List<int>();

            int width = worldMatrix[X].GetLength(0);
            int bredth = worldMatrix[0].GetLength(Y);
            int currentLevel = Z;

            for (int i = 0; i < 9; i++)
            {
                //Check for out of bounds
                if ((this.X == 0 && possibleNeighbours[i][0] < 0) ||
                    (this.X == width && possibleNeighbours[i][0] > 0) ||
                    (Y == 0 && possibleNeighbours[i][1] < 0) ||
                    (Y == bredth && possibleNeighbours[i][0] > 0))
                    continue;
                if(type == 0)
                    neighbours.Add(worldMatrix[possibleNeighbours[i][0]][possibleNeighbours[i][1]][currentLevel]);
            }

            return neighbours;
        }
    }
}
