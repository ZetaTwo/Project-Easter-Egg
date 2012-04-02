using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public interface IScript : IEnumerable<ScriptTask>
    {
        bool Active
        {
            get;
            set;
        }
    }
}
