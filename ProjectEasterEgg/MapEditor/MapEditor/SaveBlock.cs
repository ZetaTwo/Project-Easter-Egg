using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons.Physics;

namespace Mindstep.EasterEgg.MapEditor
{
    public class SaveBlock : Block
    {
        public string script;
        public BlockType type;

        public SaveBlock(Position pos)
            : base(pos)
        { }
    }
}
