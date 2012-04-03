using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Engine.Graphics;

namespace Mindstep.EasterEgg.Commons
{
    public class Block
    {
        BlockType type;
        internal BlockType Type
        {
            get { return type; }
        }

        private Position offset;
        public Position Offset
        {
            get
            {
                return offset;
            }
        }

        public Block(Position offset)
        {
            this.type = null;
            this.offset = offset;
        }

        internal Block(BlockType type, Position offset)
        {
            this.type = type;
            this.offset = offset;
        }

        public void Draw(SpriteBatch spriteBatch, Position position)
        {
            spriteBatch.Draw(Type.GetFrame(0f), SpriteHelper.toScreen(position + Offset), Color.White);
        }
    }
}
