using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public class AnimationManager<T> where T : ImageWithPos
    {
        public List<SaveAnimation<T>> Animations = new List<SaveAnimation<T>>();

        private SaveAnimation<T> currentAnimation;
        public SaveAnimation<T> CurrentAnimation { get { return currentAnimation; } }

        public void setCurrentAnimation(string animationName)
        {
            SaveAnimation<T> animation = getAnimation(animationName);
            if (animation == null)
            {
                animation = new SaveAnimation<T>(animationName);
                Animations.Add(animation);
            }
            Animations.First(a => a.Name == animationName);
            currentAnimation = animation;
        }

        private SaveAnimation<T> getAnimation(string animationName)
        {
            return Animations.FirstOrDefault(animation => animation.Name == animationName);
        }
    }
}
