using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public interface Modifiable<T>
        where T : EventArgs
    {
        event EventHandler<T> Modified;
    }

    public interface Modifiable
    {
        event EventHandler Modified;
    }
}
