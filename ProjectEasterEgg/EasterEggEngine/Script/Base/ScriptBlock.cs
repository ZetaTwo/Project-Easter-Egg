using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine
{
    public enum BlockAction { INSPECT, INTERACT };

    public abstract class ScriptBlock : Script
    {
        GameBlock block;
        protected GameBlock Block
        {
            get { return block; }
        }

        BlockAction action;
        protected BlockAction Action
        {
            get { return action; }
        }

        public void Prepare(GameBlock block, BlockAction action)
        {
            this.block = block;
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
