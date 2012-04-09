using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Physics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Game;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Mindstep.EasterEgg.Commons.DTO;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameMap : IEntityDrawable, IEnumerable<GameBlock>
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        BoundingBoxInt bounds;
        public BoundingBoxInt Bounds
        {
            get { return bounds; }
        }

        public Mindstep.EasterEgg.Commons.Graphics.Camera Camera;

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
            bounds = new BoundingBoxInt(new Position[] {min, max});
            worldMatrix = Creators.CreateWorldMatrix<GameBlock>(max - min + new Position(1, 1, 1));
            Camera = new Mindstep.EasterEgg.Commons.Graphics.Camera(new Point(50, 50));
        }

        public GameMap(GameMapDTO data)
        {
            bounds = new BoundingBoxInt(new Position[] {Position.Zero, data.Max});
            worldMatrix = Creators.CreateWorldMatrix<GameBlock>(new Position(data.WorldMatrix.Length,
                                                                    data.WorldMatrix[0].Length,
                                                                    data.WorldMatrix[0][0].Length));
            Camera = new Mindstep.EasterEgg.Commons.Graphics.Camera(new Point(200, 200));

            for (int x = 0; x < worldMatrix.Length; x++)
            {
                for (int y = 0; y < worldMatrix[0].Length; y++)
                {
                    for (int z = 0; z < worldMatrix[0][0].Length; z++)
                    {
                        if (data.WorldMatrix[x][y][z] != null)
                        {
                            GameBlockDTO blockData = data.WorldMatrix[x][y][z];
                            worldMatrix[x][y][z] = new GameBlock(blockData);
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

            foreach (GameBlock block in this)
            {
                block.Initialize(Engine);
            }
        }

        public void AddUpdate(IEntityUpdate entity)
        {
            updateObjects.Add(entity);
        }

        public void RemoveUpdate(IEntityUpdate entity)
        {
            updateObjects.Remove(entity);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameBlock block in this)
            {
                block.Draw(spriteBatch, Bounds);
            }
            /*
            foreach (IEntityDrawable drawable in drawableObjects)
            {
                drawable.Draw(spriteBatch);
            }*/
        }

        public void AddUpdate(IEntityDrawable entity)
        {
            drawableObjects.Add(entity);
        }

        public IEnumerator<GameBlock> GetEnumerator()
        {
            foreach (GameBlock[][] yList in WorldMatrix)
            {
                foreach (GameBlock[] zList in yList)
                {
                    foreach (GameBlock block in zList)
                    {
                        if (block != null)
                        {
                            yield return block;
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
