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
        Point position = Point.Zero;
        public Point Position
        {
            get { return position; }
        }

        Texture2D texture;
        SpriteFont font;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position.ToVector2(), Color.White);
            Vector3 position = CoordinateTransform.ScreenToObjectSpace(Position, Engine.World.CurrentMap.Camera, 0);
            spriteBatch.DrawString(font, position.ToString(), Position.Add(new Point(35, 0)).ToVector2(), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            position = position.Add(Engine.Input.MouseDelta.ToXnaPoint());
            position.X = Extensions.Clamp(position.X, 0, Engine.GraphicsDevice.Viewport.Width - texture.Width);
            position.Y = Extensions.Clamp(position.Y, 0, Engine.GraphicsDevice.Viewport.Height - texture.Height);

            if (Engine.Input.ClickLeft)
            {
                Engine.Physics.ClickWorld(Position, Engine.World.CurrentMap.Camera, BlockAction.INTERACT);
            }

            if (Engine.Input.ClickRight)
            {
                Engine.Physics.ClickWorld(Position, Engine.World.CurrentMap.Camera, BlockAction.INSPECT);
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
