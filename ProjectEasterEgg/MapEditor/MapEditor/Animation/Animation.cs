using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class Animation
    {
        public const int DEFAULT_ANIMATION_DURATION = 100;

        public SortedDictionary<int, Frame> Frames = new SortedDictionary<int, Frame>();
        private string name;
        public string Name { get { return name; } }


        private int currentFrameIndex = -1;
        public int CurrentFrameIndex
        {
            get { return currentFrameIndex; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Frame index cannot be negative");
                }

                if (!Frames.ContainsKey(value))
                {
                    Frame newFrame = new Frame(currentFrameIndex == -1 ? DEFAULT_ANIMATION_DURATION : CurrentFrame.Duration);
                    Frames.Add(value, newFrame);
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
            setName(name);
            CurrentFrameIndex = 0;
        }

        public void setName(string name)
        {
            this.name = name;
        }
    }
}
