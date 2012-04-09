using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.DTO
{
    public class AnimationDTO
    {
        public readonly List<FrameDTO> Frames = new List<FrameDTO>();
        public string Name;

        public AnimationDTO() { }

        public AnimationDTO(string name)
        {
            this.Name = name;
        }
    }
}
