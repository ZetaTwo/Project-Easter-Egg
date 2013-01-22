using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Commons.SaveLoad;

namespace Mindstep.EasterEgg.MapEditor
{
    public class SaveModelWithInfo : SaveModel<Texture2DWithPos>
    {
        public static string UNTITELED_MODEL_NAME = "untitled";

        public string path = null;
        public bool changedSinceLastSave = false;
        new public readonly ListWithSelectedElement<SaveAnimationWithInfo> Animations =
            new ListWithSelectedElement<SaveAnimationWithInfo>();


        /// <summary>
        /// Create a new model with the default name, and no path
        /// </summary>
        public SaveModelWithInfo()
            : base(UNTITELED_MODEL_NAME) {}


        /// <summary>
        /// Load the model from a file
        /// </summary>
        /// <param name="path"></param>
        public SaveModelWithInfo(string path)
            : this()
        {
            this.path = path;
        }
    }
}
