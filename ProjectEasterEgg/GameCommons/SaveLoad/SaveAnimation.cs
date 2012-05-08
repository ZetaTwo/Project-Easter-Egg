using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveAnimation<T> where T : ImageWithPos
    {
        public const string DEFAULT_ANIMATION_NAME = "still";

        public string Name;
        public Facing Facing = Facing.POSITIVE_Y;
        public List<SaveFrame<T>> Frames = new List<SaveFrame<T>>();

        private SaveFrame<T> currentFrame;
        public SaveFrame<T> CurrentFrame
        {
            get
            {
                if (this.Frames.Count == 0)
                {
                    this.Frames.Add(new SaveFrame<T>());
                }
                if (currentFrame == null)
                {
                    currentFrame = this.Frames[0];
                }
                return currentFrame;
            }
            set
            {
                if (!Frames.Contains(value))
                {
                    throw new ArgumentException("Can't set CurrentFrame to a frame that isn't in the frames list");
                }
                currentFrame = value;
            }
        }




        public SaveAnimation(string name = DEFAULT_ANIMATION_NAME)
        {
            this.Name = name;
        }
    }
}
