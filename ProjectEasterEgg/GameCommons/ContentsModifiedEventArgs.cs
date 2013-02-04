using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class ContentsModifiedEventArgs<T> : EventArgs
    {
        public readonly ContentAction Action;
        public readonly T Element;

        public ContentsModifiedEventArgs(ContentAction action, T element)
        {
            this.Element = element;
        }
    }

    public enum ContentAction
    {
        Add,
        Remove,
        Clear,
    }
}
