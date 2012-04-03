using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Engine.Physics
{
    public interface IHasNeighbours<N>
    {
        List<N> Neighbours { get; }
    }
}
