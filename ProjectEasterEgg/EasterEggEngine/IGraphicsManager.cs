﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public interface IGraphicsManager
    {
        void AddObject();

        void RemoveObject();

        void Draw();
    }
}
