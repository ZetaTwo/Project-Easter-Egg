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
        public readonly List<SaveFrame<T>> Frames = new List<SaveFrame<T>>();





        public SaveAnimation(string name = DEFAULT_ANIMATION_NAME)
        {
            this.Name = name;
        }
    }
}
