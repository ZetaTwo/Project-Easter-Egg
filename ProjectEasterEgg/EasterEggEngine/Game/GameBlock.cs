using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Engine.Game
{
    public enum BlockType { WALKABLE, SOLID, STAIRS_UP, STAIRS_DOWN };
    public enum BlockFaces { LEFT, RIGHT, TOP };

    public class GameBlock : Block
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        string scriptName = null;
        public bool Interactable
        {
            get { return scriptName != null; ; }
        }

        public GameBlock(BlockType type, Position position)
            : base(position)
        {
            this.type = type;
        }

        public void Initialize(EggEngine engine)
        {
            this.engine = engine;
        }

        private BlockType type;
        public BlockType Type
        {
            get { return type; }
        }

        public void Interact(BlockAction action)
        {
            ScriptBlock script = Engine.Script.Library.GetBlockScript(scriptName, this, action);
            Engine.Script.AddScript(script);
        }

       
    }
}
