using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Mindstep.EasterEgg.Engine
{
    public interface ISoundManager
    {
        void PlaySound(string soundName);

        SoundEffect GetSound(string soundName);
    }
}
