using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Input;
using SysMouse = Microsoft.Xna.Framework.Input.Mouse;

namespace Mindstep.EasterEgg.Engine.Input
{
    public class InputManager
    {
        private EggEngine engine;
        public EggEngine Engine { get { return engine; } }

        private MouseInfo mouse;
        public MouseInfo Mouse { get { return mouse; } }

        private KeyboardInfo keyboard;
        public KeyboardInfo Keyboard { get { return keyboard; } }





        public InputManager()
        { }

        public void Initialize(EggEngine engine)
        {
            this.engine = engine;
            mouse = new MouseInfo(Engine);
            keyboard = new KeyboardInfo(Engine);

            Engine.Activated += new EventHandler<EventArgs>(WindowFocusGained);
            Engine.Deactivated += new EventHandler<EventArgs>(WindowFocusLost);
        }





        public void WindowFocusGained(object sender, EventArgs e)
        {
            Mouse.Freeze(SysMouse.GetState().Location());
            Engine.IsMouseVisible = false;
        }

        public void WindowFocusLost(object sender, EventArgs e)
        {
            Mouse.Unfreeze();
            Engine.IsMouseVisible = true;
        }

        internal void Update(GameTime gameTime)
        {
            Mouse.Update(gameTime);
            Keyboard.Update(gameTime);
        }
    }
}
