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
        public int Z  { get { return z; } }

        Node[][][] worldMatrix;

        public IEnumerable<Node> Neighbours { get { return this.getNeighbours(worldMatrix); } }

        public Node(Node[][][] _worldMatrix)
        {
            worldMatrix = _worldMatrix;
        }

        private IEnumerable<Node> getNeighbours(Node[][][] worldMatrix)
        {
            List<Node> neighbours = new List<Node>();

            if(X != 0 && Y != 0)
            {
                neighbours.Add(worldMatrix[X-1][Y-1][Z]);
                neighbours.Add(worldMatrix[X-1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y-1][Z]);
                neighbours.Add(worldMatrix[X+1][Y-1][Z]);
                neighbours.Add(worldMatrix[X-1][Y+1][Z]);
                neighbours.Add(worldMatrix[X+1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y + 1][Z]);
                neighbours.Add(worldMatrix[X+1][Y + 1][Z]);
            }
            else if (X == 0)
            {
                neighbours.Add(worldMatrix[X][Y - 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y - 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y + 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y + 1][Z]);
            }
            else if (Y == 0)
            {
                neighbours.Add(worldMatrix[X - 1][Y][Z]);
                neighbours.Add(worldMatrix[X - 1][Y + 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y + 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y + 1][Z]);
            }
            else if (X == neighbours.Count)
            {
                neighbours.Add(worldMatrix[X - 1][Y - 1][Z]);
                neighbours.Add(worldMatrix[X - 1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y - 1][Z]);
                neighbours.Add(worldMatrix[X - 1][Y + 1][Z]);
                neighbours.Add(worldMatrix[X][Y + 1][Z]);
            }
            else if (Y == neighbours.Count)
            {
                neighbours.Add(worldMatrix[X - 1][Y - 1][Z]);
                neighbours.Add(worldMatrix[X - 1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y - 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y - 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y][Z]);
            }
            else if (X == 0 && Y == 0)
            {
                neighbours.Add(worldMatrix[X + 1][Y + 1][Z]);
                neighbours.Add(worldMatrix[X + 1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y + 1][Z]);
            }
            else if (X == neighbours.Count && Y == neighbours.Count)
            {
                neighbours.Add(worldMatrix[X - 1][Y - 1][Z]);
                neighbours.Add(worldMatrix[X - 1][Y][Z]);
                neighbours.Add(worldMatrix[X][Y - 1][Z]);
            }

            return neighbours;
        }
    }
}
