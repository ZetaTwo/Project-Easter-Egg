using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine;

namespace Mindstep.EasterEgg.Game
{
    public class Character : GameEntitySolid
    {
        public Character(EggEngine engine)
            : base(engine)
        {
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
