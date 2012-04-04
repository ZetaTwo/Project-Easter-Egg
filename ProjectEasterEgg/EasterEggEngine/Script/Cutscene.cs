using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    abstract class Cutscene : Script
    {
        public Cutscene(string name)
            : base(name)
        {
        }

        void BeginCutscene()
        {
        }

        void EndCutscene()
        {
        }
    }
}
