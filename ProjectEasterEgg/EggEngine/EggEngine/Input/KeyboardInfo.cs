using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mindstep.EasterEgg.Engine.Input
{
    public class KeyboardInfo : Entity
    {
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;





        public KeyboardInfo(EggEngine engine)
            : base(engine)
        { }




        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }
        public bool KeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) &
                previousKeyboardState.IsKeyUp(key);
        }
        public bool KeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) &
                previousKeyboardState.IsKeyDown(key);
        }

        internal void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
    }
}
