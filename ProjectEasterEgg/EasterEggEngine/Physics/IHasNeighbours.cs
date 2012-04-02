using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine.Physics
{
    interface IHasNeighbours<N>
    {
        IEnumerable<N> Neighbours { get; }
    }
}
