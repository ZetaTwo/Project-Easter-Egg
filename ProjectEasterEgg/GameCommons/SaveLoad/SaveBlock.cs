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
    public class SaveBlock : BlockImage, IPositionable, ICloneable
    {
        private Position position;
        public Position Position { get { return position; } }

        public string script;
        public BlockType type;

        public SaveBlock(Position position)
        {
            this.position = position;
        }

        public SaveBlock(Position position, BlockType type, string script)
            : this(position)
        {
            this.type = type;
            this.script = script;
        }

        public object Clone()
        {
            return new SaveBlock(position, type, script);
        }
    }
}
