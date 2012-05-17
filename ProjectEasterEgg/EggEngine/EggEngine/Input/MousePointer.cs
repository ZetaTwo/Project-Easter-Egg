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
        Texture2D texture;
        SpriteFont font;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Engine.Input.Mouse.Location.ToVector2(), Color.White);
            Vector3 position = CoordinateTransform.ScreenToObjectSpace(Engine.Input.Mouse.Location, Engine.World.CurrentMap.Camera, 0);
            spriteBatch.DrawString(font, position.ToString(), Engine.Input.Mouse.Location.Add(new Point(35, 0)).ToVector2(), Color.White);
        }

        public override void Initialize(EggEngine engine)
        {
            base.Initialize(engine);

            texture = Engine.Content.Load<Texture2D>("cursor");
            font = Engine.Content.Load<SpriteFont>("DefaultFont");
        }

    }
}
