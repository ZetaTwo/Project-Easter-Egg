using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Commons
{
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

        public Block(Position position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Position origin)
        {
            spriteBatch.Draw(texture, CoordinateTransform.ObjectToProjectionSpace(origin + Position), Color.White);
        }
    }
}
