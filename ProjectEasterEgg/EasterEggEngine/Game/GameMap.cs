using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Physics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Game;
using Mindstep.EasterEgg.Commons;
using EggEnginePipeline;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameMap : IEntityDrawable
    {
        EggEngine engine;

        Position origin;
        public Position Origin
        {
            get { return origin; }
        }

        private List<IEntityDrawable> drawableObjects = new List<IEntityDrawable>();

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
            worldMatrix = CreateWorldMatrix<GameBlock>(max - origin + new Position(1, 1, 1));
        }

        public GameMap(GameMapDTO data)
        {
            origin = data.Origin;
            worldMatrix = CreateWorldMatrix<GameBlock>(new Position(data.WorldMatrix.Length,
                                                                    data.WorldMatrix[0].Length,
                                                                    data.WorldMatrix[0][0].Length));

            for (int x = 0; x < worldMatrix.Length; x++)
            {
                for (int y = 0; y < worldMatrix[0].Length; y++)
                {
                    for (int z = 0; z < worldMatrix[0][0].Length; z++)
                    {
                        if (data.WorldMatrix[x][y][z] != null)
                        {
                            worldMatrix[x][y][z] = new GameBlock(data.WorldMatrix[x][y][z].Type, data.WorldMatrix[x][y][z].Position);
                        }
                    }
                }
            }
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

        public static T[][][] CreateWorldMatrix<T>(Position size)
        {
            T[][][] matrix = new T[size.X][][];

            for (int x = 0; x < size.X; x++)
            {
                matrix[x] = new T[size.Y][];
                for (int y = 0; y < size.Y; y++)
                {
                    matrix[x][y] = new T[size.Z];
                }
            }

            return matrix;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IEntityDrawable drawable in drawableObjects)
            {
                drawable.Draw(spriteBatch);
            }

            foreach (IEntityDrawable drawable in drawableObjects)
            {
                drawable.Draw(spriteBatch);
            }
        }

        public void AddUpdate(IEntityDrawable entity)
        {
            drawableObjects.Add(entity);
        }
    }
}
