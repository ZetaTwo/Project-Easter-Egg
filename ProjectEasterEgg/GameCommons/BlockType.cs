using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Commons
{
    class BlockType
    {
        List<Texture2D> textures;
        public Texture2D GetFrame(float time)
        {
            return textures[0];
        }
    }
}
