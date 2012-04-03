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
    public enum BlockType { WALKABLE, SOLID, STAIRS_UP, STAIRS_DOWN };
    public enum BlockFaces { LEFT, RIGHT, TOP };

    public class Block
    {
        private Position position;
        public Position Position
        {
            get
            {
                return position;
            }
        }

        Texture2D texture;

        private BlockType type;
        public BlockType Type
        {
            get { return type; }
        }

        public Block(Position position)
            : this(BlockType.SOLID, position)
        {
        }

        public Block(BlockType type, Position position)
        {
            this.type = type;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Position origin)
        {
            spriteBatch.Draw(texture, SpriteHelper.toScreen(origin + Position), Color.White);
        }
    }
}
