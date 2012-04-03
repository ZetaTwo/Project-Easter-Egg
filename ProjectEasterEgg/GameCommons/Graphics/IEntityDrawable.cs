using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Engine
{
    public interface IEntityDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
