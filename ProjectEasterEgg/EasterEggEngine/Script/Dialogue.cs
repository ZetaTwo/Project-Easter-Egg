using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public abstract class Dialogue : Script
    {
        public Dialogue(string name)
            : base(name)
        {
        }

        Character currentSpeaker;
        public Character CurrentSpeaker
        {
            get
            {
                return currentSpeaker;
            }
        }

        string currentText;
        public String CurrentText
        {
            get
            {
                return currentText;
            }
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void NewPage()
        {
            currentText = "";
        }

        public void NewSpeaker(Character speaker)
        {
            NewPage();
            currentSpeaker = speaker;

        }

        public IEnumerable<float> Say(string text, float speed = 20f, float pause = 0f)
        {
            foreach (char character in text)
            {
                currentText += character;
                yield return 1f / speed;
            }
            
            yield return pause;
        }

        public void ShowMenu(List<String> alternatives)
        {
            throw new System.NotImplementedException();
        }
    }
}
