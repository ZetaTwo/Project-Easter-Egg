using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveModel<T> where T : ImageWithPos
    {
        public string name;
        public readonly List<SaveBlock> blocks = new List<SaveBlock>();
        public readonly List<SaveAnimation<T>> animations = new List<SaveAnimation<T>>();
        public readonly List<SaveSubModel<T>> subModels = new List<SaveSubModel<T>>();

        /// <summary>
        /// Creates a new SaveModel.
        /// </summary>
        /// <param name="modelName">Name for the model</param>
        public SaveModel(string modelName)
        {
            this.name = modelName;
        }

        /// <summary>
        /// Creates a new SaveModel and adds the given blocks, animations and models.
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="blocks"></param>
        /// <param name="animations"></param>
        /// <param name="subModels"></param>
        public SaveModel(string modelName, IEnumerable<SaveBlock> blocks, IEnumerable<SaveAnimation<T>> animations, IEnumerable<SaveSubModel<T>> subModels)
            : this(modelName)
        {
            this.blocks.AddRange(blocks);
            this.animations.AddRange(animations);
            this.subModels.AddRange(subModels);
        }
    }
}
