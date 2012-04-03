using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindstep.EasterEgg.Engine.Physics;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;

namespace GameTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class NodeUnitTest
    {
        PhysicsManager physics;
        Block[][][] testMatrix1;

        public NodeUnitTest()
        {
            physics = new PhysicsManager();
            GameMap map = new GameMap();
            physics.CurrentMap = map;

            testMatrix1 = new Block[6][][];

            for(int i = 0; i < 6; i++)
            {
                testMatrix1[i] = new Block[6][];
                for(int k = 0; k < 6; k++)
                {
                    testMatrix1[i][k] = new Block[1];
                    Block n = new Block(BlockType.SOLID, new Position(i, k, 0));
                    testMatrix1[i][k][0] = n;
                }
            }

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetNeighbours()
        {
            physics.CurrentMap.WorldMatrix = testMatrix1;

            Block node = new Block(0, new Position(1, 1, 0));
            List<Block> test = physics.GetNeighbours(node);
            Assert.AreEqual(0, test.Count);
            
        }
    }
}
