using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine.Game;
using System.Collections;
using Mindstep.EasterEgg.Engine.Physics;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public class PhysicsManager : IPhysicsManager
    {
        private GameMap currentMap;
        public GameMap CurrentMap
        {
            get { return currentMap; }
            set { currentMap = value; }
        }

        public void MoveObject(GameEntitySolid character, Vector3 endpoint, Map map)
        {
            throw new System.NotImplementedException();
        }

        public int estimate(Node start, Node end)
        {
            return (int)Math.Floor((end.Position - start.Position).Length());
        }

        public Path FindPath(Node start, Node destination)
        {
            var closed = new HashSet<Node>();
            var queue = new PriorityQueue<double, Path>();
            queue.Enqueue(0, new Path(start));
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
                foreach (Node node in path.LastStep.getNeighbours(CurrentMap.WorldMatrix))
                {
                    double d = 1; //Distance between 2 squares in the grid
                    var newPath = path.AddStep(node, d);
                    queue.Enqueue(newPath.TotalCost + estimate(node, destination), newPath);
                }
            }
            return null;
        }
    }
}