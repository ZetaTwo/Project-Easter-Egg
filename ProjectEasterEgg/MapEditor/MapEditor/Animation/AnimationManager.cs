using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor.Animations
{
    public class AnimationManager
    {
        private List<Animation> animations = new List<Animation>();

        private Animation currentAnimation;
        public Animation CurrentAnimation { get { return currentAnimation; } }

        public Frame CurrentFrame { get { return CurrentAnimation.CurrentFrame; } }

        public void setCurrentAnimation(string animationName)
        {
            Animation animation = getAnimation(animationName);
            if (animation == null)
            {
                animation = new Animation(animationName);
            }
            animations.Add(animation);
            currentAnimation = animation;
        }

        private Animation getAnimation(string animationName)
        {
            foreach (Animation animation in (List<Animation>)this)
            {
                if (animation.Name == animationName)
                {
                    return animation;
                }
            }
            return null;
        }

        public static implicit operator List<Animation>(AnimationManager manager)
        {
            return manager.animations;
        }
    }
}
