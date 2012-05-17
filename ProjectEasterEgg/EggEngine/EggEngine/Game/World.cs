using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Input;
using Mindstep.EasterEgg.Engine.Physics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Game;

namespace Mindstep.EasterEgg.Engine
{
    public class World
    {
        EggEngine engine;
        protected EggEngine Engine
        {
            get { return engine; }
        }

        protected MousePointer pointer;
        public MousePointer Pointer
        {
            get { return pointer; }
        }

        List<IEntityDrawable> drawables = new List<IEntityDrawable>();
        List<IEntityUpdate> update = new List<IEntityUpdate>();

        //private Camera camera;
        SamplerState samplerState;

        private List<GameMap> maps = new List<GameMap>();
        
        private GameMap currentMap;
        public GameMap CurrentMap
        {
            get { return currentMap; }
            set
            {
                if (!maps.Contains(value))
                {
                    maps.Add(value);
                }

                currentMap = value;
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentMap.Update(gameTime);

            foreach (IEntityUpdate entity in update)
            {
                entity.Update(gameTime);
            }
        }

        public virtual void Initialize(EggEngine _engine)
        {
            engine = _engine;

            samplerState = new SamplerState();
            samplerState.Filter = TextureFilter.PointMipLinear;
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentMap.Camera.PrepareForDraw();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, samplerState, null, null, null, CurrentMap.Camera.ZoomAndOffsetMatrix);
            CurrentMap.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            DrawWorld(spriteBatch);
            spriteBatch.End();
        }

        private void DrawWorld(SpriteBatch spriteBatch)
        {
            foreach (IEntityDrawable entity in drawables)
            {
                entity.Draw(spriteBatch);
            }
        }

        public void AddUpdate(IEntityUpdate entity)
        {
            update.Add(entity);
        }

        public void RemoveUpdate(IEntityUpdate entity)
        {
            update.Remove(entity);
        }

        public void AddDraw(IEntityDrawable entity)
        {
            drawables.Add(entity);
        }

        public void RemoveDraw(IEntityDrawable entity)
        {
            drawables.Remove(entity);
        }
    }
}
