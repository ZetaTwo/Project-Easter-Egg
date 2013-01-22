using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor
{
    public class RemovedEventArgs<T> : EventArgs
    {
        public readonly T Element;

        public RemovedEventArgs(T element)
        {
            this.Element = element;
        }
    }
}
