using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Engine.Game;

namespace EggEnginePipeline
{
    public class GameBlockDTO
    {
        public string scriptName = null;
        public Position Position;
        Texture2D Texture;
        public BlockType Type;
    }
}
