using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveFrame<T> where T : ImageWithPos
    {
        public int Duration;
        public ImageManager<T> Images = new ImageManager<T>();

        public SaveFrame(int initialDuration)
        {
            this.Duration = initialDuration;
        }
    }
}
