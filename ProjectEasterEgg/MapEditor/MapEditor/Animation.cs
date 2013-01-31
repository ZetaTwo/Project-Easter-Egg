using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor
{
    public class Animation
    {
        public string Name = SaveAnimation<Texture2DWithPos>.DEFAULT_ANIMATION_NAME;
        public Facing Facing = Facing.POSITIVE_Y;
        public readonly ListWithSelectedElement<SaveFrame<Texture2DWithPos>> Frames =
            new ListWithSelectedElement<SaveFrame<Texture2DWithPos>>();

        public Animation() {}

        public Animation(SaveAnimation<Texture2DWithPos> animation)
        {
            Frames.AddRange(animation.Frames);
        }
    }
}
