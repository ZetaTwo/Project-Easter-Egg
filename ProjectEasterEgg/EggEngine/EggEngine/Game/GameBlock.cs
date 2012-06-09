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
    public class GameBlock : Block, Child
    {
        public static GameBlock Empty = new GameBlock(BlockType.EMPTY);
        public static GameBlock OutOfBounds = new GameBlock(BlockType.OUT_OF_BOUNDS);

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

        private BlockType type;
        public BlockType Type
        {
            get { return type; }
        }

        private GameModel parent;
        public GameModel Parent { get { return parent; } }





        public GameBlock(GameModel parent, GameBlockDTO blockData)
            : this(parent, blockData.Type, blockData.Position, blockData.scriptName)
        { }

        public GameBlock(GameModel parent, BlockType type, Position position, string scriptName = null)
            : base(position)
        {
            if (scriptName != null)
            {
                this.scriptName = Constants.SCRIPT_BLOCK_PREFIX + scriptName;
            }
            if (type == BlockType.OUT_OF_BOUNDS)
            {
                throw new Exception("Can't set blocktype to OUT_OF_BOUNDS!");
            }
            else if (type == BlockType.EMPTY)
            {
                throw new Exception("Can't set blocktype to EMPTY!");
            }
            this.type = type;
            this.parent = parent;
        }

        public void Initialize(EggEngine engine)
        {
            this.engine = engine;
        }

        private GameBlock(BlockType anyBlockType)
            : base(Position.Zero)
        {
            this.type = anyBlockType;
        }





        public void Interact(BlockAction action)
        {
            ScriptBlock script = Engine.Script.Library.GetBlockScript(scriptName, this, action);
            Engine.Script.AddScript(script);
        }
    }
}
