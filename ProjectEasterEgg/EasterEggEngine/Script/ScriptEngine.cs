using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public class ScriptEngine : IScriptEngine
    {
        private EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        private List<IScript> scripts = new List<IScript>();


        public ScriptEngine(EggEngine _engine, IScript startScript)
        {
            engine = _engine;
            scripts.Add(startScript);
        }
    
        public void Update()
        {
            foreach (IScript script in scripts)
            {
                foreach(ScriptTask task in script)
                {

                }
            }
        }

        public void AddScript(IScript script)
        {
            scripts.Add(script);
        }
    }
}
