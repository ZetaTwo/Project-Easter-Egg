using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class Script : IScript
    {
        string name;
        public string Name
        {
            get { return name; }
        }

        IScriptEngine engine;
        public IScriptEngine Engine
        {
            get { return engine; }
            set { engine = value; }
        }

        public Script(string name)
        {
            this.name = name;
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

        protected float Wait(float duration)
        {
            return duration;
        }
    }
}
