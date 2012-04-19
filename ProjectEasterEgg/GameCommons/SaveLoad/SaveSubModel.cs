using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveSubModel<T> : SaveModel<T>
        where T : ImageWithPos
    {
        public Position offset;

        public SaveSubModel(SaveModel<T> baseModel, Position offset)
            : base(baseModel.name, baseModel.blocks, baseModel.animations, baseModel.subModels)
        {
            this.offset = offset;
        }
    }
}
