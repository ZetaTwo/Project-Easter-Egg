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
        public static Texture2D DefaultCursorTexture;
        SpriteFont font;
        private Control windowControl;

        public Cursor Cursor
        {
            get { return windowControl.Cursor; }
            set { windowControl.Cursor = value; }
        }





        public override void Initialize(EggEngine engine)
        {
            base.Initialize(engine);
            DefaultCursorTexture = Engine.Content.Load<Texture2D>("cursor-default");
            font = Engine.Content.Load<SpriteFont>("DefaultFont");
            windowControl = Form.FromHandle(Engine.Window.Handle);

            Cursor = DefaultCursorTexture.ToCursor(Point.Zero);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Engine.Input.Mouse.StealMouse)
            {
                spriteBatch.Draw(DefaultCursorTexture, Engine.Input.Mouse.Location.ToVector2(), Color.White);
                Vector3 position = CoordinateTransform.ScreenToObjectSpace(Engine.Input.Mouse.Location, Engine.World.CurrentMap.Camera, 0);
                spriteBatch.DrawString(font, position.ToPosition().ToString(), Engine.Input.Mouse.Location.Add(new Point(35, 0)).ToVector2(), Color.White);
            }
        }
    }
}
