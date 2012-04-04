using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mindstep.EasterEgg.Engine.Physics;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Engine;

namespace GameTests
{
    [TestClass]
    public class FindPathUnitTest
    {
        EggEngine engine;

        GameMap map1;
        GameMap map2;

        public FindPathUnitTest()
        {
            engine = new EggEngine(new TestWorld());

            map1 = new GameMap(new Position(0, 0, 0), new Position(5, 5, 0));
            map2 = new GameMap(new Position(0, 0, 0), new Position(5, 5, 0));

            map2.WorldMatrix[2][2][0] = new GameBlock(BlockType.SOLID, new Position(2, 2, 0));
            map2.WorldMatrix[3][2][0] = new GameBlock(BlockType.SOLID, new Position(3, 2, 0));
            map2.WorldMatrix[3][3][0] = new GameBlock(BlockType.SOLID, new Position(3, 3, 0));
            map2.WorldMatrix[3][4][0] = new GameBlock(BlockType.SOLID, new Position(3, 4, 0));
        }

        [TestMethod]
        public void FindPathTestMethod1()
        {
            engine.World.CurrentMap = map1;

            GameBlock n = new GameBlock(0, new Position(1, 1, 0));
            GameBlock end = new GameBlock(0, new Position(4, 4, 0));
            Path<GameBlock> path = engine.Physics.FindPath(n, end);

            Assert.AreEqual(end.Position, path.LastStep.Position);
        }

        [TestMethod]
        public void FindPathTestMethod2()
        {
            engine.World.CurrentMap = map2;

            GameBlock n = new GameBlock(0, new Position(1, 1, 0));
            GameBlock end = new GameBlock(0, new Position(4, 4, 0));
            Path<GameBlock> path = engine.Physics.FindPath(n, end);

            Assert.AreEqual(end.Position, path.LastStep.Position);
        }
    }
}
