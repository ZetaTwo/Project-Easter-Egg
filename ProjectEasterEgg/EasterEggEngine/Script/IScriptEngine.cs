﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public interface IScriptEngine
    {
        EggEngine Engine
        {
            get;
        }
    
        void Update(GameTime gameTime);
        void AddScript(IScript script);


    }
}
