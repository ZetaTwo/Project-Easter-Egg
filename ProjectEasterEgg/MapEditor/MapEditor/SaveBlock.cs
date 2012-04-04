using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.MapEditor
{
    struct SaveBlock
    {
        public Position Position;
        public Texture2D Texture;
        public int id;
        public string script;
    }
}
