using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class GameMapDTO
    {
        public Position Max;
        public GameBlockDTO[][][] WorldMatrix;
    }
}
