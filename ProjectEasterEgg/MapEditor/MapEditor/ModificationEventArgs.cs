using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor
{
    public class ModificationEventArgs<T> : EventArgs
    {
        public readonly T Before;
        public readonly T After;

        public ModificationEventArgs(T before, T after)
        {
            this.Before = before;
            this.After = after;
        }
}
}
