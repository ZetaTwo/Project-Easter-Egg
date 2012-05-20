using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine.Game
{
    public abstract class GameEntityDrawable : GameEntity, IEntityDrawable
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
