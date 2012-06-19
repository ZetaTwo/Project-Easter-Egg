using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Engine.Game;
using Mindstep.EasterEgg.Commons;
using System.Windows.Forms;

namespace Mindstep.EasterEgg.Engine.Input
{
    public class MousePointer : GameEntityDrawable
    {
        Texture2D cursorTexture;
        SpriteFont font;

        public override void Initialize(EggEngine engine)
        {
            base.Initialize(engine);
            cursorTexture = Engine.Content.Load<Texture2D>("cursor");
            font = Engine.Content.Load<SpriteFont>("DefaultFont");

            SetUpCursor();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Engine.Input.Mouse.StealMouse)
            {
                spriteBatch.Draw(cursorTexture, Engine.Input.Mouse.Location.ToVector2(), Color.White);
                Vector3 position = CoordinateTransform.ScreenToObjectSpace(Engine.Input.Mouse.Location, Engine.World.CurrentMap.Camera, 0);
                spriteBatch.DrawString(font, position.ToPosition().ToString(), Engine.Input.Mouse.Location.Add(new Point(35, 0)).ToVector2(), Color.White);
            }
        }

        protected void SetUpCursor()
        {
            Form.FromHandle(Engine.Window.Handle).Cursor = cursorTexture.ToCursor(System.Drawing.Point.Empty);
        }
    }
}
