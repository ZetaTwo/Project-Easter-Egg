using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;
using Microsoft.Xna.Framework.Graphics;
using System.Text.RegularExpressions;
using Mindstep.EasterEgg.Commons;

namespace Mindstep.EasterEgg.MapEditor
{
    public class Model
    {
        public event EventHandler<ReplacedEventArgs<string>> PathChanged;
        public event EventHandler SavedOrNeedsSaving;

        private string path = null;
        public string Path
        {
            get { return path; }
            private set
            {
                string pathBefore = Path;
                path = value;
                if (PathChanged != null) PathChanged(this, new ReplacedEventArgs<string>(pathBefore, Path));
            }
        }

        private bool changedSinceLastSave = false;
        public bool ChangedSinceLastSave
        {
            get { return changedSinceLastSave; }
            set
            {
                if (ChangedSinceLastSave != value)
                {
                    changedSinceLastSave = value;
                    if (SavedOrNeedsSaving != null) SavedOrNeedsSaving(this, null);
                }
            }
        }

        public readonly CascadingListWithSelectedElement<SaveBlock> Blocks = new CascadingListWithSelectedElement<SaveBlock>();
        public readonly CascadingListWithSelectedElement<SaveSubModel<Texture2DWithPos>> SubModels = new CascadingListWithSelectedElement<SaveSubModel<Texture2DWithPos>>();
        public readonly CascadingListWithSelectedElement<Animation> Animations = new CascadingListWithSelectedElement<Animation>();





        /// <summary>
        /// Create a new model with the default name, and no path
        /// </summary>
        public Model()
        {
            Animations.Modified += (sender, e) => ChangedSinceLastSave = true;
            Animations.SubModification += (sender, e) => ChangedSinceLastSave = true;
            
            Blocks.Modified += (sender, e) => ChangedSinceLastSave = true;
            Blocks.SubModification += (sender, e) => ChangedSinceLastSave = true;

            SubModels.Modified += (sender, e) => ChangedSinceLastSave = true;
            SubModels.SubModification += (sender, e) => ChangedSinceLastSave = true;
        }


        /// <summary>
        /// Load the model from a file
        /// </summary>
        /// <param name="path"></param>
        public Model(string path)
            : this()
        {
            this.Path = path;
            throw new NotImplementedException();
        }

        public Model(SaveModel<Texture2DWithPos> saveModel)
            : this()
        {
            Path = saveModel.Name;
            Blocks.AddRange(saveModel.Blocks);
            SubModels.AddRange(saveModel.SubModels);
            foreach (SaveAnimation<Texture2DWithPos> animation in saveModel.Animations)
            {
                Animations.Add(new Animation(animation));
            }
        }

        internal void Save(string fileName)
        {
            EggModelSaver.Save(this, fileName);
            Path = fileName;
            ChangedSinceLastSave = false;
        }
    }
}
