using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine.Physics
{
    class Node : IHasNeighbours<Node>
    {
        private int x { get; }
        private int y { get; }
        private int z { get; }

        Node[][][] worldMatrix;

        IEnumerable<Node> Neighbours { get { return this.getNeighbours(worldMatrix); } }

        public void Node(Node[][][] _worldMatrix)
        {
            worldMatrix = _worldMatrix;
        }

        private IEnumerable<Node> getNeighbours(Node[][][] worldMatrix)
        {
            List<Node> neighbours = new List<Node>();

            if(x != 0 && y != 0)
            {
                neighbours.Add(worldMatrix[x-1][y-1][z]);
                neighbours.Add(worldMatrix[x-1][y][z]);
                neighbours.Add(worldMatrix[x][y-1][z]);
                neighbours.Add(worldMatrix[x+1][y-1][z]);
                neighbours.Add(worldMatrix[x-1][y+1][z]);
                neighbours.Add(worldMatrix[x+1][y][z]);
                neighbours.Add(worldMatrix[x][y + 1][z]);
                neighbours.Add(worldMatrix[x+1][y + 1][z]);
            }
            else if (x == 0)
            {
                neighbours.Add(worldMatrix[x][y - 1][z]);
                neighbours.Add(worldMatrix[x + 1][y - 1][z]);
                neighbours.Add(worldMatrix[x + 1][y][z]);
                neighbours.Add(worldMatrix[x][y + 1][z]);
                neighbours.Add(worldMatrix[x + 1][y + 1][z]);
            }
            else if (y == 0)
            {
                neighbours.Add(worldMatrix[x - 1][y][z]);
                neighbours.Add(worldMatrix[x - 1][y + 1][z]);
                neighbours.Add(worldMatrix[x + 1][y][z]);
                neighbours.Add(worldMatrix[x][y + 1][z]);
                neighbours.Add(worldMatrix[x + 1][y + 1][z]);
            }
            else if (x == neighbours.Count)
            {
                neighbours.Add(worldMatrix[x - 1][y - 1][z]);
                neighbours.Add(worldMatrix[x - 1][y][z]);
                neighbours.Add(worldMatrix[x][y - 1][z]);
                neighbours.Add(worldMatrix[x - 1][y + 1][z]);
                neighbours.Add(worldMatrix[x][y + 1][z]);
            }
            else if (y == neighbours.Count)
            {
                neighbours.Add(worldMatrix[x - 1][y - 1][z]);
                neighbours.Add(worldMatrix[x - 1][y][z]);
                neighbours.Add(worldMatrix[x][y - 1][z]);
                neighbours.Add(worldMatrix[x + 1][y - 1][z]);
                neighbours.Add(worldMatrix[x + 1][y][z]);
            }
            else if (x == 0 && y == 0)
            {
                neighbours.Add(worldMatrix[x + 1][y + 1][z]);
                neighbours.Add(worldMatrix[x + 1][y][z]);
                neighbours.Add(worldMatrix[x][y + 1][z]);
            }
            else if (x == neighbours.Count && y == neighbours.Count)
            {
                neighbours.Add(worldMatrix[x - 1][y - 1][z]);
                neighbours.Add(worldMatrix[x - 1][y][z]);
                neighbours.Add(worldMatrix[x][y - 1][z]);
            }

            return neighbours;
        }
    }
}
