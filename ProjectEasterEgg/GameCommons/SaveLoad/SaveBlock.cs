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
    public class SaveBlock : BlockImage, IPositionable
    {
        private Position position;
        public Position Position { get { return position; } }

        public string script;
        public BlockType type;

        public SaveBlock(Position position)
        {
            this.position = position;
        }
    }
}
