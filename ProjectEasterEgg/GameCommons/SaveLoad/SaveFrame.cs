using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveFrame<T> : Modifiable
        where T : ImageWithPos
    {
        public const int DEFAULT_FRAME_DURATION = 100;

        public event EventHandler Modified;

        public int Duration;
        public readonly ImageManager<T> Images = new ImageManager<T>();





        public SaveFrame(int initialDuration = DEFAULT_FRAME_DURATION)
        {
            this.Duration = initialDuration;
            Images.Modified += new EventHandler(Images_Modified);
        }

        void Images_Modified(object sender, EventArgs e)
        {
            if (Modified != null) Modified(sender, e);
        }
    }
}
