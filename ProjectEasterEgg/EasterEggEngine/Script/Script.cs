using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class Script : IScript
    {
        IScriptEngine engine;
        public IScriptEngine Engine
        {
            get { return engine; }
            set { engine = value; }
        }

        public IEnumerator<float> GetEnumerator()
        {
            return ScriptContent();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<float> ScriptContent();
    }
}
