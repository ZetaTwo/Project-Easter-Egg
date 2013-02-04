using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public class SaveSubModel<T> : SaveModel<T>, Modifiable
        where T : ImageWithPos
    {
        public event EventHandler Modified;

        private Position offset;
        public Position Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                if (Modified != null) Modified(this, null);
            }
        }

        public SaveSubModel(SaveModel<T> baseModel, Position offset)
            : base(baseModel.Name, baseModel.Blocks, baseModel.Animations, baseModel.SubModels)
        {
            this.offset = offset;
        }
    }
}
