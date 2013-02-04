using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor
{
    public class Animation : Modifiable
    {
        public event EventHandler Modified;

        private string name = SaveAnimation<Texture2DWithPos>.DEFAULT_ANIMATION_NAME;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (Modified != null) Modified(this, null);
            }
        }
        private Facing facing = Facing.POSITIVE_Y;
        public Facing Facing
        {
            get { return facing; }
            set
            {
                facing = value;
                if (Modified != null) Modified(this, null);
            }
        }
        public readonly CascadingListWithSelectedElement<SaveFrame<Texture2DWithPos>> Frames =
            new CascadingListWithSelectedElement<SaveFrame<Texture2DWithPos>>();

        public Animation()
        {
            Frames.Modified += (sender, e) => { if (Modified != null) Modified(sender, e); };
            Frames.SubModification += (sender, e) => { if (Modified != null) Modified(sender, e); };
        }

        public Animation(SaveAnimation<Texture2DWithPos> animation)
            : this()
        {
            Frames.AddRange(animation.Frames);
        }
    }
}
