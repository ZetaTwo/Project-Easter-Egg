using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Engine
{
    public interface IPhysicsObject
    {
        Vector3 Position
        {
            get;
            set;
        }

        List<IBox> Boxes
        {
            get;
            set;
        }
    }

    public interface IBox
    {
        Vector3 Offset
        {
            get;
            set;
        }

        int Size
        {
            get;
            set;
        }
    }
}
