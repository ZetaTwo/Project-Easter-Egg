using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine;

namespace Mindstep.EasterEgg.Game.Scripts
{
    class ExampleDialogue : Dialogue
    {
        public override IEnumerator<float> ScriptContent()
        {
            foreach (float wait in Say("Hello there!", 20f, 1f)) { yield return wait; }
            foreach (float wait in Say("How are you?")) { yield return wait; }
            yield return Wait(2f);
        }
    }
}
