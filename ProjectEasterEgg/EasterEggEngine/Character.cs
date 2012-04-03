using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public class Character : GameEntitySolid
    {
        public String Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Character(EggEngine engine)
            : base(engine)
        { }
    
        public void Shout()
        {
            throw new System.NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
