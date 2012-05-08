using System;
using System.Collections.Generic;
using Mindstep.EasterEgg.Engine;

namespace Mindstep.EasterEgg.Scripts
{
    class ScriptBlockExample : ScriptBlock
    {
        public override IEnumerator<float> Inspect()
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<float> Interact()
        {
            System.Console.WriteLine(this.Block.Position);
            Engine.Engine.World.CurrentMap.WorldMatrix[Block.Position] = null;

            yield break;
        }
    }
}
