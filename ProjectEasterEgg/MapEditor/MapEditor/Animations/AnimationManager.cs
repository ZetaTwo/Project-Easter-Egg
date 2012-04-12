using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class AnimationManager
    {
        public List<Animation> Animations = new List<Animation>();

        private Animation currentAnimation;
        public Animation CurrentAnimation { get { return currentAnimation; } }

        public void setCurrentAnimation(string animationName)
        {
            Animation animation = getAnimation(animationName);
            if (animation == null)
            {
                animation = new Animation(animationName);
                Animations.Add(animation);
            }
            Animations.First(a => a.Name == animationName);
            currentAnimation = animation;
        }

        private Animation getAnimation(string animationName)
        {
            return Animations.FirstOrDefault(animation => animation.Name == animationName);
        }
    }
}
