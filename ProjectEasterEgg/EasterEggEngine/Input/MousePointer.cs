using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Input
{
    public class MousePointer : GameEntityDrawable
    {
        Vector2 position = Vector2.Zero;
        public Vector2 Position
        {
            get { return position; }
        }

        Texture2D texture;
        SpriteFont font;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
            Vector3 position = CoordinateTransform.FromScreen(Position, 0);
            spriteBatch.DrawString(font, position.ToString(), Position + Vector2.UnitX * 35, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            position += Engine.Input.MouseDelta;
            position.X = MathHelper.Clamp(position.X, 0, Engine.GraphicsDevice.Viewport.Width - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, Engine.GraphicsDevice.Viewport.Height - texture.Height);

            if (Engine.Input.ClickLeft)
            {
                Engine.Physics.ClickWorld(Position, BlockAction.INTERACT);
            }

            if (Engine.Input.ClickRight)
            {
                Engine.Physics.ClickWorld(Position, BlockAction.INSPECT);
            }
        }

        public override void Initialize(EggEngine _engine)
        {
            base.Initialize(_engine);

            texture = Engine.Content.Load<Texture2D>("cursor");
            font = Engine.Content.Load<SpriteFont>("DefaultFont");
        }

    }
}
