using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor
{
    public class AddedEventArgs<T> : EventArgs
    {
        public readonly T Element;

        public AddedEventArgs(T element)
        {
            this.Element = element;
        }
    }
}
