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

        public NodeUnitTest()
        {
            physics = new PhysicsManager();
            GameMap map = new GameMap(new Position(0, 0, 0), new Position(5, 5, 0));
            physics.CurrentMap = map;

            for(int x = 0; x < 6; x++)
            {
                for(int y = 0; y < 6; y++)
                {
                    GameBlock n = new GameBlock(BlockType.SOLID, new Position(x, y, 0));
                    physics.CurrentMap.WorldMatrix[x][y][0] = n;
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
            GameBlock node = new GameBlock(0, new Position(1, 1, 0));
            List<GameBlock> test = physics.GetNeighbours(node);
            Assert.AreEqual(0, test.Count);
            
        }
    }
}
