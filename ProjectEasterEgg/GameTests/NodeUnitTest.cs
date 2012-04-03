using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindstep.EasterEgg.Engine.Physics;

namespace GameTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class NodeUnitTest
    {
        int[][][] testMatrix1;

        public NodeUnitTest()
        {
             testMatrix1 = new int[][][] { new int[][] { new int[] {0,0,0,0,0,0},
                                                         new int[] {0,0,0,0,0,0},
                                                         new int[] {0,0,0,0,0,0},
                                                         new int[] {0,0,0,0,0,0},
                                                         new int[] {0,0,0,0,0,0}}};
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
            Node n = new Node(testMatrix1, 0);
            n.getNeighbours(testMatrix1);
            Console.WriteLine("hej");
        }
    }
}
