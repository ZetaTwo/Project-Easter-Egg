using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public class ModelManager<I> where I : ImageWithPos
    {
        public event EventHandler<ModificationEventArgs<SaveModel<I>>> ModelChanged;
        public event EventHandler<ModificationEventArgs<SaveAnimation<I>>> AnimationChanged;
        public event EventHandler<ModificationEventArgs<SaveFrame<I>>> FrameChanged;



        public readonly List<SaveModel<I>> Models = new List<SaveModel<I>>();
        private SaveModel<I> currentModel;
        public SaveModel<I> CurrentModel
        {
            get
            {
                return currentModel;
            }
            set
            {
                if (currentModel != value)
                {
                    if (!Models.Contains(value))
                    {
                        throw new ArgumentException("Can't set CurrentModel to a model that isn't in the list");
                    }

                    ModificationEventArgs<SaveModel<I>> e = new ModificationEventArgs<SaveModel<I>>(currentModel, value);
                    currentModel = value;
                    if (ModelChanged != null) ModelChanged(this, e);
                }
            }
        }

        private SaveAnimation<I> currentAnimation;
        public SaveAnimation<I> CurrentAnimation
        {
            get
            {
                if (CurrentModel.animations.Count == 0)
                {
                    CurrentModel.animations.Add(new SaveAnimation<I>());
                }
                if (currentAnimation == null)
                {
                    currentAnimation = CurrentModel.animations[0];
                }
                return currentAnimation;
            }
            set
            {
                if (currentAnimation != value)
                {
                    if (!CurrentModel.animations.Contains(value))
                    {
                        throw new ArgumentException("Can't set CurrentAnimation to an animation that isn't in the list");
                    }
                    ModificationEventArgs<SaveAnimation<I>> e = new ModificationEventArgs<SaveAnimation<I>>(currentAnimation, value);
                    currentAnimation = value;
                    if (AnimationChanged != null) AnimationChanged(this, e);
                }
            }
        }

        private SaveFrame<I> currentFrame;
        public SaveFrame<I> CurrentFrame
        {
            get
            {
                if (CurrentAnimation.Frames.Count == 0)
                {
                    CurrentAnimation.Frames.Add(new SaveFrame<I>());
                }
                if (currentFrame == null)
                {
                    currentFrame = currentAnimation.Frames[0];
                }
                return currentFrame;
            }
            set
            {
                if (currentFrame != value)
                {
                    if (!currentAnimation.Frames.Contains(value))
                    {
                        throw new ArgumentException("Can't set CurrentFrame to a frame that isn't in the list");
                    }
                    ModificationEventArgs<SaveFrame<I>> e = new ModificationEventArgs<SaveFrame<I>>(currentFrame, value);
                    currentFrame = value;
                    if (FrameChanged != null) FrameChanged(this, e);
                }
            }
        }
    }
}
