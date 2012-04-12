using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class Animation
    {
        private const int DEFAULT_FRAME_DURATION = 100;

        public List<Frame> Frames = new List<Frame>();
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
                    addNewFrame(CurrentFrame.Duration);
                }
                currentFrameIndex = value;
            }
        }

        public Frame CurrentFrame
        {
            get { return Frames[currentFrameIndex]; }
        }

        public Animation(string name)
        {
            this.Name = name;
            addNewFrame(DEFAULT_FRAME_DURATION);
        }

        private void addNewFrame(int duration)
        {
            Frames.Add(new Frame(duration));
        }
    }
}
