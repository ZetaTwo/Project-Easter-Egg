using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class Script : IScript
    {
        public bool Active
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public System.Collections.Generic.IEnumerable<ScriptTask> GetTask()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<ScriptTask> GetEnumerator()
        {
            return ScriptContent();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<ScriptTask> ScriptContent();
    }
}
