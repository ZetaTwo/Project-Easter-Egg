using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class GameBlockDTO
    {
        public string scriptName = null;
        public Position Position;
        public readonly Dictionary<string, AnimationDTO> Animations = new Dictionary<string, AnimationDTO>();
        public BlockType Type;
    }
}
