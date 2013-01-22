using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public class SaveAnimationWithInfo : SaveAnimation<Texture2DWithPos>
    {
        new public readonly ListWithSelectedElement<SaveFrame<Texture2DWithPos>> Frames =
            new ListWithSelectedElement<SaveFrame<Texture2DWithPos>>();
    }
}
