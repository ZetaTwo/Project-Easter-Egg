using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveModel<T> where T : ImageWithPos
    {
        public string Name;
        public readonly List<SaveBlock> Blocks = new List<SaveBlock>();
        public readonly List<SaveSubModel<T>> SubModels = new List<SaveSubModel<T>>();
        public readonly List<SaveAnimation<T>> Animations = new List<SaveAnimation<T>>();





        /// <summary>
        /// Creates a new SaveModel and adds the given blocks, animations and models.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blocks"></param>
        /// <param name="animations"></param>
        /// <param name="subModels"></param>
        public SaveModel(string name, IEnumerable<SaveBlock> blocks, IEnumerable<SaveAnimation<T>> animations, IEnumerable<SaveSubModel<T>> subModels)
        {
            this.Name = name;
            this.Blocks.AddRange(blocks);
            this.Animations.AddRange(animations);
            this.SubModels.AddRange(subModels);
        }

        /// <summary>
        /// Creates a new SaveModel.
        /// </summary>
        /// <param name="name">Name for the model</param>
        public SaveModel(string name)
        {
            this.Name = name;
        }

        protected SaveModel() {}
    }
}
