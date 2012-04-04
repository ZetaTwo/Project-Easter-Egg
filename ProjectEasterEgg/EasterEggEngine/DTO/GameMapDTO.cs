﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;

namespace EggEnginePipeline
{
    public class GameMapDTO
    {
        public Position Min;
        public Position Max;
        public GameBlockDTO[][][] WorldMatrix;
    }
}
