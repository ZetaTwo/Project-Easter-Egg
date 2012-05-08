using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Physics;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class GameModelDTO
    {
        public List<GameBlockDTO> blocks = new List<GameBlockDTO>();
        public List<AnimationDTO> animations = new List<AnimationDTO>();
        public List<GameModelDTO> subModels = new List<GameModelDTO>();
        public Dictionary<string, Position> spanwLocations = new Dictionary<string, Position>();

        public Position min;
        public Position max;
    }
}
