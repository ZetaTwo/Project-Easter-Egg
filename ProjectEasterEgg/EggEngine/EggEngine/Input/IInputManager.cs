using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public interface IInputManager
    {
        void Update(GameTime gameTime);

        Vector2 MouseDelta { get; }
        bool ClickLeft { get; }
        bool ClickRight { get; }

        void Initialize(EggEngine eggEngine);
    }
}
