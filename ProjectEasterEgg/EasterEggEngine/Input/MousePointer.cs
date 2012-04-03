using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine.Input
{
    public class MousePointer : GameEntityDrawable
    {
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            position += Engine.Input.MouseDelta;
        }
    }
}
