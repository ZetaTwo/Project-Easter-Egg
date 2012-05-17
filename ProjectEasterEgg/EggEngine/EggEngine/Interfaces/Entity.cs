using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public class Entity
    {
        private EggEngine engine;
        public EggEngine Engine { get { return engine; } }

        public Entity(EggEngine engine)
        {
            this.engine = engine;
        }
    }
}
