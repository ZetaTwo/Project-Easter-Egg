using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public interface IScriptEngine
    {
        void Update();
        void AddScript(IScript script);
    }
}
