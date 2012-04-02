using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public class ScriptState
    {
        IScript script;
        public bool IsComplete { get { return script == null; } }
        float sleep = 0f;
        IEnumerator<float> scriptEnumerator;

        public ScriptState(IScript _script)
        {
            script = _script;
        }

        public void Execute(GameTime gameTime)
        {
            if (scriptEnumerator == null)
            {
                scriptEnumerator = script.GetEnumerator();
                sleep = scriptEnumerator.Current;
            }

            if (sleep > 0)
            {
                sleep -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                bool finished = true;

                finished = !scriptEnumerator.MoveNext();
                sleep = scriptEnumerator.Current;

                if (finished)
                {
                    script = null;
                    scriptEnumerator = null;
                }
            }
        }
    }
}
