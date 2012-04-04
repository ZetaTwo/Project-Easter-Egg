using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Input;
using Mindstep.EasterEgg.Engine.Physics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine
{
    public class World
    {
        EggEngine engine;
        protected EggEngine Engine
        {
            get { return engine; }
        }

        private MousePointer pointer;
        public MousePointer Pointer
        {
            get { return pointer; }
        }

        //private Camera camera;
        SamplerState samplerState;

        private List<GameMap> maps = new List<GameMap>();
        private GameMap currentMap;
        protected GameMap CurrentMap
        {
            get { return currentMap; }
            set
            {
                if (!maps.Contains(value))
                {
                    value.Initialize(Engine);
                    maps.Add(value);
                }

                currentMap = value;
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentMap.Update(gameTime);
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, samplerState, null, null, null, Matrix.Identity);
            CurrentMap.Draw(spriteBatch);
            spriteBatch.End();

            DrawWorld(spriteBatch);
        }

        private void DrawWorld(SpriteBatch spriteBatch)
        {

        }
    }
}
