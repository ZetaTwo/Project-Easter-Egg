using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameBlock : Block
    {
        bool interactable = false;
        public bool Interactable
        {
            get { return interactable; }
        }

        public GameBlock(BlockType type, Position position)
            : base(position)
        {
            this.type = type;
        }

        private BlockType type;
        public BlockType Type
        {
            get { return type; }
        }

        public BlockScript GetScript(BlockAction action)
        {
            return null;
        }

       
    }
}
