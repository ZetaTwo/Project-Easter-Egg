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

        private List<GameMap> maps;
        private GameMap currentMap;
        protected GameMap CurrentMap
        {
            get { return currentMap; }
            set { currentMap = value; }
        }

        public void Update(GameTime gameTime)
        {
            CurrentMap.Update(gameTime);
        }

        public virtual void Initialize(EggEngine _engine)
        {
            engine = _engine;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentMap.Draw(spriteBatch);

            DrawWorld(spriteBatch);
        }

        private void DrawWorld(SpriteBatch spriteBatch)
        {

        }
    }
}
