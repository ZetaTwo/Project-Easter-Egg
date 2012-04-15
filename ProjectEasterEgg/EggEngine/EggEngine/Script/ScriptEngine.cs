using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public class ScriptEngine : IScriptEngine
    {
        private EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        private ScriptFactory scriptLibrary = new ScriptFactory();
        public ScriptFactory Library
        {
            get { return scriptLibrary; }
        }

        private List<ScriptState> scripts = new List<ScriptState>();


        public ScriptEngine(EggEngine _engine)
        {
            engine = _engine;
        }
    
        public void Update(GameTime gameTime)
        {
            // execute all of our scripts
            foreach (var scriptState in scripts)
            {
                scriptState.Execute(gameTime);
            }

            // remove any completed scripts
            scripts.RemoveAll(s => s.IsComplete);
        }

        public void AddScript(IScript script)
        {
            script.Engine = this;
            scripts.Add(new ScriptState(script));
        }
    }
}
