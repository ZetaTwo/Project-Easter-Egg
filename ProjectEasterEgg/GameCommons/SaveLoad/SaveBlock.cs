using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons.Physics;
using Xna = Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.Graphic;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveBlock : BlockImage, IPositionable, ICloneable, Modifiable
    {
        public event EventHandler Modified;

        private Position position;
        public Position Position { get { return position; } }

        private string script;
        public string Script
        {
            get { return script; }
            set
            {
                script = value;
                if (Modified != null) Modified(this, null);
            }
        }

        private BlockType type;
        public BlockType Type
        {
            get { return type; }
            set
            {
                type = value;
                if (Modified != null) Modified(this, null);
            }
        }



        public SaveBlock(Position position)
        {
            this.position = position;
        }

        public SaveBlock(Position position, BlockType type, string script)
            : this(position)
        {
            this.Type = type;
            this.Script = script;
        }

        public object Clone()
        {
            return new SaveBlock(position, Type, Script);
        }
    }
}
