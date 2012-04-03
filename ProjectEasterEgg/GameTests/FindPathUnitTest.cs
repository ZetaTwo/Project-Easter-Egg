using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindstep.EasterEgg.Engine.Physics;

namespace GameTests
{
    [TestClass]
    public class FindPathUnitTest
    {
        Node[][][] testMatrix1;

        public FindPathUnitTest()
        {
             testMatrix1 = new Node[6][][];

            for(int i = 0; i < 6; i++)
            {
                testMatrix1[i] = new Node[6][];
                for(int k = 0; k < 6; k++)
                {
                    testMatrix1[i][k] = new Node[1];
                    Node n = new Node(0,i,k,0);
                    testMatrix1[i][k][0] = n;
                }
            }

        }

        [TestMethod]
        public void FindPathTestMethod()
        {
       
            Node n = new Node(testMatrix1, 0, 1, 1, 0);
            Node end = new Node(0,4,4,0);
            PhysicsManager p = new PhysicsManager();
            Path<Node> path = p.FindPath<Node>(n, end,e => estimate(n, end));
            Assert.AreEqual(null, path);
            List<Node> k = (List<Node>)n.Neighbours;
            List<Node> a = n.getNeighbours();
            Assert.AreEqual(a.ElementAt(a.Count - 1), k.ElementAt(k.Count - 1));
            a.ElementAt(a.Count - 1);
        }

        public int estimate(Node start, Node end)
        {
            return 0;
        }
    }
}
