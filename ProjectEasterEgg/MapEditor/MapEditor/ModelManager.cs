using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;
using System.Threading;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor
{
    public class ModelManager
    {
        public event EventHandler<ContentsModifiedEventArgs<Model>> ModelModified;
        public event EventHandler<ReplacedEventArgs<Model>> SelectedModelChanged;
        public event EventHandler<ReplacedEventArgs<string>> ModelNameChanged;
        public event EventHandler ModelNeedsSaving;

        public event EventHandler<ContentsModifiedEventArgs<Animation>> AnimationModified;
        public event EventHandler<ReplacedEventArgs<Animation>> SelectedAnimationChanged;

        public event EventHandler<ContentsModifiedEventArgs<SaveFrame<Texture2DWithPos>>> FrameModified;
        public event EventHandler<ReplacedEventArgs<SaveFrame<Texture2DWithPos>>> SelectedFrameChanged;


        private EventHandler<ReplacedEventArgs<string>> Model_NameChangedEventHandler;
        private EventHandler<ReplacedEventArgs<bool>> Model_SavedOrNeedsSavingEventHandler;

        private EventHandler<ContentsModifiedEventArgs<Animation>> Animations_ModifiedEventHandler;
        private EventHandler<ReplacedEventArgs<Animation>> Animations_SelectedChangedEventHandler;

        private EventHandler<ContentsModifiedEventArgs<SaveFrame<Texture2DWithPos>>> Frames_ModifiedEventHandler;
        private EventHandler<ReplacedEventArgs<SaveFrame<Texture2DWithPos>>> Frames_SelectedChangedEventHandler;

        public readonly ListWithSelectedElement<Model> Models = new ListWithSelectedElement<Model>();





        public ModelManager()
        {
            Models.Modified += new EventHandler<ContentsModifiedEventArgs<Model>>(Models_Modified);
            Models.SelectedChanged += new EventHandler<ReplacedEventArgs<Model>>(Models_SelectedChanged);
            Model_NameChangedEventHandler = new EventHandler<ReplacedEventArgs<string>>(Model_NameChanged);
            Model_SavedOrNeedsSavingEventHandler = Model_SavedOrNeedsSaving;

            Animations_ModifiedEventHandler = new EventHandler<ContentsModifiedEventArgs<Animation>>(Animations_Modified);
            Animations_SelectedChangedEventHandler = new EventHandler<ReplacedEventArgs<Animation>>(Animations_SelectedChanged);

            Frames_ModifiedEventHandler = new EventHandler<ContentsModifiedEventArgs<SaveFrame<Texture2DWithPos>>>(Frames_Modified);
            Frames_SelectedChangedEventHandler = new EventHandler<ReplacedEventArgs<SaveFrame<Texture2DWithPos>>>(Frames_SelectedChanged);
        }





        void Models_Modified(object sender, ContentsModifiedEventArgs<Model> e)
        {
            if (ModelModified != null) ModelModified(this, e);
        }
        private void Models_SelectedChanged(object sender, ReplacedEventArgs<Model> model)
        {
            Animation animationBefore = null, animationAfter = null;

            if (model.Before != null)
            {
                animationBefore = model.Before.Animations.Selected;
                model.Before.Animations.SelectedChanged -= Animations_SelectedChangedEventHandler;
                model.Before.Animations.Modified -= Animations_ModifiedEventHandler;
                model.Before.PathChanged -= Model_NameChangedEventHandler;
                model.Before.SavedOrNeedsSaving -= Model_SavedOrNeedsSaving;
            }
            if (model.After != null)
            {
                animationAfter = model.After.Animations.Selected;
                model.After.Animations.SelectedChanged += Animations_SelectedChangedEventHandler;
                model.After.Animations.Modified += Animations_ModifiedEventHandler;
                model.After.PathChanged += Model_NameChangedEventHandler;
                model.After.SavedOrNeedsSaving += Model_SavedOrNeedsSaving;
            }
            if (animationBefore != animationAfter)
            {
                Animations_SelectedChanged(sender, new ReplacedEventArgs<Animation>(animationBefore, animationAfter));
            }
            if (SelectedModelChanged != null) SelectedModelChanged(this, model);
        }
        void Model_NameChanged(object sender, ReplacedEventArgs<string> e)
        {
            if (ModelNameChanged != null) ModelNameChanged(this, e);
        }
        void Model_SavedOrNeedsSaving(object sender, EventArgs e)
        {
            if (ModelNeedsSaving != null) ModelNeedsSaving(this, e);
        }





        void Animations_Modified(object sender, ContentsModifiedEventArgs<Animation> e)
        {
            if (AnimationModified != null) AnimationModified(this, e);
        }
        private void Animations_SelectedChanged(object sender, ReplacedEventArgs<Animation> animation)
        {
            SaveFrame<Texture2DWithPos> frameBefore = null, frameAfter = null;

            if (animation.Before != null)
            {
                frameBefore = animation.Before.Frames.Selected;
                animation.Before.Frames.SelectedChanged -= Frames_SelectedChangedEventHandler;
                animation.Before.Frames.Modified -= Frames_ModifiedEventHandler;
            }
            if (animation.After != null)
            {
                frameAfter = animation.After.Frames.Selected;
                animation.After.Frames.SelectedChanged += Frames_SelectedChangedEventHandler;
                animation.After.Frames.Modified += Frames_ModifiedEventHandler;
            }
            if (frameBefore != frameAfter)
            {
                Frames_SelectedChanged(sender, new ReplacedEventArgs<SaveFrame<Texture2DWithPos>>(frameBefore, frameAfter));
            }
            if (SelectedAnimationChanged != null) SelectedAnimationChanged(this, animation);
        }

        void Frames_Modified(object sender, ContentsModifiedEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            if (FrameModified != null) FrameModified(this, e);
        }
        private void Frames_SelectedChanged(object sender, ReplacedEventArgs<SaveFrame<Texture2DWithPos>> e)
        {
            if (SelectedFrameChanged != null) SelectedFrameChanged(this, e);
        }





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
