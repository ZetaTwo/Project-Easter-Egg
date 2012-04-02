using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public class ScriptEngine : IScriptEngine
    {
        private System.Collections.Generic.List<IScript> scripts;
    
        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
