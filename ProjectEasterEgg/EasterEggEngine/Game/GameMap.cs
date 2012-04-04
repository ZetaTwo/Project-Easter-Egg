using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Physics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Game;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameMap : Map
    {
        EggEngine engine;

        Position origin;
        public Position Origin
        {
            get { return origin; }
        }

        private GameBlock[][][] worldMatrix;
        public GameBlock[][][] WorldMatrix
        {
            get { return worldMatrix; }
            set { worldMatrix = value; }
        }
        List<IEntityUpdate> updateObjects = new List<IEntityUpdate>();

        public GameMap(Position min, Position max)
        {
            origin = min;
            worldMatrix = CreateWorldMatrix(max - origin + new Position(1, 1, 1));
        }
        
        public void Update(GameTime gameTime)
        {
            foreach (IEntityUpdate update in updateObjects)
            {
                update.Update(gameTime);
            }
        }

        public void Initialize(EggEngine _engine)
        {
            engine = _engine;
        }

        public void AddUpdate(IEntityUpdate entity)
        {
            updateObjects.Add(entity);
        }

        public void RemoveUpdate(IEntityUpdate entity)
        {
            updateObjects.Remove(entity);
        }

        private static GameBlock[][][] CreateWorldMatrix(Position size)
        {
            GameBlock[][][] matrix = new GameBlock[size.X][][];

            for (int x = 0; x < size.X; x++)
            {
                matrix[x] = new GameBlock[size.Y][];
                for (int y = 0; y < size.Y; y++)
                {
                    matrix[x][y] = new GameBlock[size.Z];
                }
            }

            return matrix;
        }
    }
}
