using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine.Graphics;
using Mindstep.EasterEgg.Commons.DTO;
using Mindstep.EasterEgg.Commons.Physics;

namespace Mindstep.EasterEgg.Engine.Game
{
    public class GameBlock : Block
    {
        protected EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        string scriptName = null;
        public bool Interactable
        {
            get { return scriptName != null; }
        }




        public GameBlock(GameBlockDTO blockData)
            : this(blockData.Type, blockData.Position, blockData.scriptName)
        { }

        public GameBlock(BlockType blockType, Position position)
            : this(blockType, position, null)
        { }

        public GameBlock(BlockType type, Position position, string scriptName)
            : base(position)
        {
            if (scriptName != null)
            {
                this.scriptName = "ScriptBlock" + scriptName;
            }
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
