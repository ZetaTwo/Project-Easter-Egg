using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class Frame
    {
        public int Duration;
        public TextureManager Textures = new TextureManager();

        public Frame() { }

        public Frame(int initialDuration)
        {
            this.Duration = initialDuration;
        }
    }
}
