using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public class ModelManager
    {
        public event EventHandler<ModificationEventArgs<SaveModelWithInfo>> ModelChanged;
        public event EventHandler<ModificationEventArgs<SaveAnimation<Texture2DWithPos>>> AnimationChanged;
        public event EventHandler<ModificationEventArgs<SaveFrame<Texture2DWithPos>>> FrameChanged;



        public readonly List<SaveModelWithInfo> Models = new List<SaveModelWithInfo>();
        private SaveModelWithInfo currentModel;
        public SaveModelWithInfo CurrentModel
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

                    ModificationEventArgs<SaveModelWithInfo> e = new ModificationEventArgs<SaveModelWithInfo>(currentModel, value);
                    currentModel = value;
                    if (ModelChanged != null) ModelChanged(this, e);
                }
            }
        }

        private SaveAnimation<Texture2DWithPos> currentAnimation;
        public SaveAnimation<Texture2DWithPos> CurrentAnimation
        {
            get
            {
                if (CurrentModel.animations.Count == 0)
                {
                    CurrentModel.animations.Add(new SaveAnimation<Texture2DWithPos>());
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
                    ModificationEventArgs<SaveAnimation<Texture2DWithPos>> e =
                        new ModificationEventArgs<SaveAnimation<Texture2DWithPos>>(currentAnimation, value);
                    currentAnimation = value;
                    if (AnimationChanged != null) AnimationChanged(this, e);
                }
            }
        }

        private SaveFrame<Texture2DWithPos> currentFrame;
        public SaveFrame<Texture2DWithPos> CurrentFrame
        {
            get
            {
                if (CurrentAnimation.Frames.Count == 0)
                {
                    CurrentAnimation.Frames.Add(new SaveFrame<Texture2DWithPos>());
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
                    ModificationEventArgs<SaveFrame<Texture2DWithPos>> e =
                        new ModificationEventArgs<SaveFrame<Texture2DWithPos>>(currentFrame, value);
                    currentFrame = value;
                    if (FrameChanged != null) FrameChanged(this, e);
                }
            }
        }
    }
}
