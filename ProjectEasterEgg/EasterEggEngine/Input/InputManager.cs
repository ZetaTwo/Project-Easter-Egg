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

        public Vector2 MouseDelta
        {
            get
            {
                return new Vector2(currentMouseState.X - previousMouseState.X,
                                   currentMouseState.Y - previousMouseState.Y);
            }
        }

        public bool ClickLeft { get { return currentMouseState.LeftButton == ButtonState.Pressed; } }
        public bool ClickRight { get { return currentMouseState.RightButton == ButtonState.Pressed; } }

        public InputManager(EggEngine _engine)
        {
            engine = _engine;
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Viewport view = Engine.GraphicsDevice.Viewport;
            Mouse.SetPosition(view.Width, view.Height);
        }
    }
}
