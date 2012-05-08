using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveModel<T> where T : ImageWithPos
    {
        public string name;
        public readonly List<SaveBlock> blocks = new List<SaveBlock>();
        public readonly List<SaveSubModel<T>> subModels = new List<SaveSubModel<T>>();
        public readonly List<SaveAnimation<T>> animations = new List<SaveAnimation<T>>();

        private SaveAnimation<T> currentAnimation;
        public SaveAnimation<T> CurrentAnimation
        {
            get
            {
                if (this.animations.Count == 0)
                {
                    this.animations.Add(new SaveAnimation<T>());
                }
                if (currentAnimation == null)
                {
                    currentAnimation = this.animations[0];
                }
                return currentAnimation;
            }
            set
            {
                if (!animations.Contains(value))
                {
                    throw new ArgumentException("Can't set CurrentAnimation to an animation that isn't in the animations list");
                }
                currentAnimation = value;
            }
        }





        /// <summary>
        /// Creates a new SaveModel and adds the given blocks, animations and models.
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="blocks"></param>
        /// <param name="animations"></param>
        /// <param name="subModels"></param>
        public SaveModel(string modelName, IEnumerable<SaveBlock> blocks, IEnumerable<SaveAnimation<T>> animations, IEnumerable<SaveSubModel<T>> subModels)
        {
            this.name = modelName;
            this.blocks.AddRange(blocks);
            this.animations.AddRange(animations);
            this.subModels.AddRange(subModels);
        }

        /// <summary>
        /// Creates a new SaveModel.
        /// </summary>
        /// <param name="modelName">Name for the model</param>
        public SaveModel(string modelName)
        {
            this.name = modelName;
        }
    }
}
