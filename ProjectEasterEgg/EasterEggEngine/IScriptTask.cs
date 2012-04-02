using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public interface IScriptTask
    {
        bool Done
        {
            get;
            set;
        }

        void Update(GameTime gameTime);
    }
}
