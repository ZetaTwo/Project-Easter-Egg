using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveAnimation<T> where T : ImageWithPos
    {
        private const int DEFAULT_FRAME_DURATION = 100;

        public List<SaveFrame<T>> Frames = new List<SaveFrame<T>>();
        public string Name;
        public Facing Facing;

        private int currentFrameIndex = 0;
        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Frame index cannot be negative");
                }

                while (value > Frames.Count)
                {
                    Frames.Add(new SaveFrame<T>(CurrentFrame.Duration));
                }
                currentFrameIndex = value;
            }
        }

        public SaveFrame<T> CurrentFrame
        {
            get { return Frames[currentFrameIndex]; }
        }

        public SaveAnimation(string name)
        {
            this.Name = name;
            Frames.Add(new SaveFrame<T>(DEFAULT_FRAME_DURATION));
        }
    }
}
