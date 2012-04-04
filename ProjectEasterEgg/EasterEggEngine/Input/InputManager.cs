using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Engine
{
    public class InputManager : IInputManager
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }
        
        MouseState currentMouseState;
        MouseState previousMouseState;

        Vector2 Origin;
        public Vector2 MouseDelta
        {
            get
            {
                return new Vector2(currentMouseState.X, currentMouseState.Y) - Origin;
            }
        }

        public bool ClickLeft { get { return currentMouseState.LeftButton == ButtonState.Pressed; } }
        public bool ClickRight { get { return currentMouseState.RightButton == ButtonState.Pressed; } }

        public InputManager()
        {
        }

        public void Initialize(EggEngine _engine)
        {
            engine = _engine;

            Viewport view = Engine.GraphicsDevice.Viewport;
            Origin = new Vector2(view.Width / 2, view.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            Mouse.SetPosition((int)Origin.X, (int)Origin.Y);
        }
    }
}
