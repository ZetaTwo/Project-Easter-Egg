using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Mindstep.EasterEgg.Engine
{
    public class SoundManager : ISoundManager
    {
        EggEngine engine;
        public EggEngine Engine
        {
            get { return engine; }
        }

        SoundManager(EggEngine _engine)
        {
            engine = _engine;
        }

        public void PlaySound(string soundName)
        {
            SoundEffect soundEffect = Engine.Content.Load<SoundEffect>(soundName);
            soundEffect.Play();
        }

        public SoundEffect GetSound(string soundName)
        {
            return Engine.Content.Load<SoundEffect>(soundName);
        }
    }
}
