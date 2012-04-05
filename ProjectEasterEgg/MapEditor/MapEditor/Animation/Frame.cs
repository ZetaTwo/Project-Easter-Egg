using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class Frame
    {
        public int Duration;
        public List<Texture2DWithPos> Textures = new List<Texture2DWithPos>();

        public Frame() { }

        public Frame(int initialDuration)
        {
            this.Duration = initialDuration;
        }
    }
}
