using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class ReplacedEventArgs<T> : EventArgs
    {
        public readonly T Before;
        public readonly T After;

        public ReplacedEventArgs(T before, T after)
        {
            this.Before = before;
            this.After = after;
        }
}
}
