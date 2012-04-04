using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine
{
    public enum BlockAction { INSPECT, INTERACT };

    public abstract class BlockScript : Script
    {
        BlockAction action;

        public BlockScript(BlockAction action, string name) : base(name)
        {
            this.action = action;
        }

        public override IEnumerator<float> ScriptContent()
        {
            switch (action)
            {
                case BlockAction.INSPECT:
                    return Inspect();
                case BlockAction.INTERACT:
                    return Interact();
                default:
                    return Inspect();
            }
        }

        public abstract IEnumerator<float> Inspect();

        public abstract IEnumerator<float> Interact();
    }
}
