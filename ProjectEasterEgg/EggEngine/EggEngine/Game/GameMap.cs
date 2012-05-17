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
    public class GameMap : GameModel, IEntityDrawable
    {
        protected EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        public BoundingBoxInt Bounds
        {
            get { return relativeBounds; }
        }

        public GameCamera Camera;

        private List<IEntityDrawable> drawableObjects = new List<IEntityDrawable>();

        private WorldMatrix worldMatrix;
        public WorldMatrix WorldMatrix
        {
            get { return worldMatrix; }
        }

        List<IEntityUpdate> updateObjects = new List<IEntityUpdate>();

        public GameMap(GameModelDTO data)
            : base(data)
        {
            Camera = new GameCamera();

            worldMatrix = new WorldMatrix(this);
        }

        public GameMap(GameModelDTO data, EggEngine engine)
            : this(data)
        {
            Initialize(engine);
        }
        
        public void Update(GameTime gameTime)
        {
            foreach (IEntityUpdate update in updateObjects)
            {
                update.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch, Bounds);
            //    foreach (GameBlock block in this)
            //    {
            //        block.Draw(spriteBatch, Bounds);
            //    }
            //    /*
            //    foreach (IEntityDrawable drawable in drawableObjects)
            //    {
            //        drawable.Draw(spriteBatch);
            //    }*/
        }

        internal new void Initialize(EggEngine engine)
        {
            this.engine = engine;
            base.Initialize(engine);
        }

        public bool Spawn(GameModel model, string at)
        {
            Position location;
            if (spawnLocations.TryGetValue(at, out location))
            {
                if (worldMatrix.tryToPlaceModel(model, location))
                {
                    model.position = location;
                    model.Parent = this;
                    subModels.Add(model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentException("No known spawn location '" + at + "'.");
            }
        }

        public void AddDraw(IEntityDrawable entity)
        {
            drawableObjects.Add(entity);
        }
        public void AddUpdate(IEntityUpdate entity)
        {
            updateObjects.Add(entity);
        }
        public void RemoveUpdate(IEntityUpdate entity)
        {
            updateObjects.Remove(entity);
        }
    }
}
