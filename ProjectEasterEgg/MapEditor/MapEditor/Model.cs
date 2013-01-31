using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.MapEditor
{
    public class Model
    {
        public static string UNTITELED_MODEL_NAME = "untitled";

        public string Name;
        public readonly ListWithSelectedElement<SaveBlock> Blocks = new ListWithSelectedElement<SaveBlock>();
        public readonly ListWithSelectedElement<SaveSubModel<Texture2DWithPos>> SubModels = new ListWithSelectedElement<SaveSubModel<Texture2DWithPos>>();
        public readonly ListWithSelectedElement<Animation> Animations = new ListWithSelectedElement<Animation>();

        public string path = null;
        public bool changedSinceLastSave = false;
        



        /// <summary>
        /// Create a new model with the default name, and no path
        /// </summary>
        public Model()
        {
            this.Name = UNTITELED_MODEL_NAME;
        }


        /// <summary>
        /// Load the model from a file
        /// </summary>
        /// <param name="path"></param>
        public Model(string path)
            : this()
        {
            this.path = path;
        }

        public Model(SaveModel<Texture2DWithPos> saveModel)
            : this()
        {
            Name = saveModel.Name;
            Blocks.AddRange(saveModel.Blocks);
            SubModels.AddRange(saveModel.SubModels);
            foreach (SaveAnimation<Texture2DWithPos> animation in saveModel.Animations)
            {
                Animations.Add(new Animation(animation));
            }
        }
    }
}
