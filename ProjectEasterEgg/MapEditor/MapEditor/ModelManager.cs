using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public class ModelManager
    {
        public event EventHandler<AddedEventArgs<Model>> ModelAdded;
        public event EventHandler<RemovedEventArgs<Model>> ModelRemoved;
        public event EventHandler<ModificationEventArgs<Model>> ModelChanged;

        public event EventHandler<AddedEventArgs<Animation>> AnimationAdded;
        public event EventHandler<RemovedEventArgs<Animation>> AnimationRemoved;
        public event EventHandler<ModificationEventArgs<Animation>> AnimationChanged;

        public event EventHandler<AddedEventArgs<SaveFrame<Texture2DWithPos>>> FrameAdded;
        public event EventHandler<RemovedEventArgs<SaveFrame<Texture2DWithPos>>> FrameRemoved;
        public event EventHandler<ModificationEventArgs<SaveFrame<Texture2DWithPos>>> FrameChanged;



        private EventHandler<AddedEventArgs<Animation>> addedAnimationEventHandler;
        private EventHandler<RemovedEventArgs<Animation>> removedAnimationEventHandler;
        private EventHandler<ModificationEventArgs<Animation>> selectedAnimationEventHandler;

        private EventHandler<AddedEventArgs<SaveFrame<Texture2DWithPos>>> addedFrameEventHandler;
        private EventHandler<RemovedEventArgs<SaveFrame<Texture2DWithPos>>> removedFrameEventHandler;
        private EventHandler<ModificationEventArgs<SaveFrame<Texture2DWithPos>>> selectedFrameEventHandler;





        public ModelManager()
        {
            Models.Added += new EventHandler<AddedEventArgs<Model>>(Model_Added);
            Models.Removed += new EventHandler<RemovedEventArgs<Model>>(Model_Removed);
            Models.SelectedChanged += new EventHandler<ModificationEventArgs<Model>>(Model_SelectedChanged);

            addedAnimationEventHandler = new EventHandler<AddedEventArgs<Animation>>(Animation_Added);
            removedAnimationEventHandler = new EventHandler<RemovedEventArgs<Animation>>(Animation_Removed);
            selectedAnimationEventHandler = new EventHandler<ModificationEventArgs<Animation>>(Animation_SelectedChanged);

            addedFrameEventHandler = new EventHandler<AddedEventArgs<SaveFrame<Texture2DWithPos>>>(Frame_Added);
            removedFrameEventHandler = new EventHandler<RemovedEventArgs<SaveFrame<Texture2DWithPos>>>(Frame_Removed);
            selectedFrameEventHandler = new EventHandler<ModificationEventArgs<SaveFrame<Texture2DWithPos>>>(Frame_SelectedChanged);
        }





        void Model_Added(object sender, AddedEventArgs<Model> e)
        {
            if (ModelAdded != null) ModelAdded(this, e);
        }
        void Model_Removed(object sender, RemovedEventArgs<Model> e)
        {
            if (ModelRemoved != null) ModelRemoved(this, e);
        }
        private void Model_SelectedChanged(object sender, ModificationEventArgs<Model> model)
        {
            Animation animationBefore = null, animationAfter = null;

            if (model.Before != null)
            {
                animationBefore = model.Before.Animations.Selected;
                model.Before.Animations.SelectedChanged -= selectedAnimationEventHandler;
                model.Before.Animations.Added -= addedAnimationEventHandler;
                model.Before.Animations.Removed -= removedAnimationEventHandler;
            }
            if (model.After != null)
            {
                animationAfter = model.After.Animations.Selected;
                model.After.Animations.SelectedChanged += selectedAnimationEventHandler;
                model.After.Animations.Added += addedAnimationEventHandler;
                model.After.Animations.Removed += removedAnimationEventHandler;
            }
            if (animationBefore != animationAfter)
            {
                Animation_SelectedChanged(sender, new ModificationEventArgs<Animation>(animationBefore, animationAfter));
            }
            if (ModelChanged != null) ModelChanged(this, model);
        }

        void Animation_Added(object sender, AddedEventArgs<Animation> e)
        {
            if (AnimationAdded != null) AnimationAdded(this, e);
        }
        void Animation_Removed(object sender, RemovedEventArgs<Animation> e)
        {
            if (AnimationRemoved != null) AnimationRemoved(this, e);
        }
        private void Animation_SelectedChanged(object sender, ModificationEventArgs<Animation> animation)
        {
            SaveFrame<Texture2DWithPos> frameBefore = null, frameAfter = null;

            if (animation.Before != null)
            {
                frameBefore = animation.Before.Frames.Selected;
                animation.Before.Frames.SelectedChanged -= selectedFrameEventHandler;
                animation.Before.Frames.Added -= addedFrameEventHandler;
                animation.Before.Frames.Removed -= removedFrameEventHandler;
            }
            if (animation.After != null)
            {
                frameAfter = animation.After.Frames.Selected;
                animation.After.Frames.SelectedChanged += selectedFrameEventHandler;
                animation.After.Frames.Added += addedFrameEventHandler;
                animation.After.Frames.Removed += removedFrameEventHandler;
            }
            if (frameBefore != frameAfter)
            {
                Frame_SelectedChanged(sender, new ModificationEventArgs<SaveFrame<Texture2DWithPos>>(frameBefore, frameAfter));
            }
            if (AnimationChanged != null) AnimationChanged(this, animation);
        }

        void Frame_Added(object sender, AddedEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            if (FrameAdded != null) FrameAdded(this, e);
        }
        void Frame_Removed(object sender, RemovedEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            if (FrameRemoved != null) FrameRemoved(this, e);
        }
        private void Frame_SelectedChanged(object sender, ModificationEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            if (FrameChanged != null) FrameChanged(this, e);
        }





        public readonly ListWithSelectedElement<Model> Models = new ListWithSelectedElement<Model>();

        public Model SelectedModel
        {
            get { return Models.Selected; }
            set
            {
                Models.Selected = value;
            }
        }

        public ListWithSelectedElement<Animation> Animations
        {
            get { return SelectedModel == null ? null : SelectedModel.Animations; }
        }

        public Animation SelectedAnimation
        {
            get { return Animations == null ? null : Animations.Selected; }
            set
            {
                if (Animations == null)
                {
                    throw new ArgumentException("Can't select an animation without having selected a model first.");
                }
                else
                {
                    Animations.Selected = value;
                }
            }
        }

        public ListWithSelectedElement<SaveFrame<Texture2DWithPos>> Frames
        {
            get { return SelectedAnimation == null ? null : SelectedAnimation.Frames; }
        }

        public SaveFrame<Texture2DWithPos> SelectedFrame
        {
            get { return Frames == null ? null : Frames.Selected; }
            set
            {
                if (Frames == null)
                {
                    throw new ArgumentException("Can't select a frame without having selected an animation first.");
                }
                else
                {
                    Frames.Selected = value;
                }
            }
        }
    }
}
