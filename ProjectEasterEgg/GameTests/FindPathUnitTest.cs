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
    [TestClass]
    public class FindPathUnitTest
    {
        PhysicsManager physics;

        GameMap map1;
        GameMap map2;

        public FindPathUnitTest()
        {
            map1 = new GameMap(new Position(0, 0, 0), new Position(5, 5, 0));
            map2 = new GameMap(new Position(0, 0, 0), new Position(5, 5, 0));
            physics = new PhysicsManager();

            map2.WorldMatrix[2][2][0] = new GameBlock(BlockType.SOLID, new Position(2, 2, 0));
            map2.WorldMatrix[3][2][0] = new GameBlock(BlockType.SOLID, new Position(3, 2, 0));
            map2.WorldMatrix[3][3][0] = new GameBlock(BlockType.SOLID, new Position(3, 3, 0));
            map2.WorldMatrix[3][4][0] = new GameBlock(BlockType.SOLID, new Position(3, 4, 0));
        }

        [TestMethod]
        public void FindPathTestMethod1()
        {
            physics.CurrentMap = map1;

            GameBlock n = new GameBlock(0, new Position(1, 1, 0));
            GameBlock end = new GameBlock(0, new Position(4, 4, 0));
            Path<GameBlock> path = physics.FindPath(n, end);

            Assert.AreEqual(end.Position, path.LastStep.Position);
        }

        [TestMethod]
        public void FindPathTestMethod2()
        {
            physics.CurrentMap = map2;

            GameBlock n = new GameBlock(0, new Position(1, 1, 0));
            GameBlock end = new GameBlock(0, new Position(4, 4, 0));
            Path<GameBlock> path = physics.FindPath(n, end);

            Assert.AreEqual(end.Position, path.LastStep.Position);
        }
    }
}
