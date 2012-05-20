using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public interface IEntityDrawable
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
