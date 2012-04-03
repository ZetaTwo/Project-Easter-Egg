using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine;

namespace Mindstep.EasterEgg.Commons.Game
{
    public interface IEntityUpdate
    {
        void Update(GameTime gameTime);
        void Initialize(EggEngine _engine);
    }
}
