﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public enum BlockType { WALKABLE, SOLID, STAIRS, SPAWN_LOCATION, OUT_OF_BOUNDS, EMPTY };
    public enum BlockFaces { LEFT, RIGHT, TOP };
    public enum Facing { POSITIVE_X, POSITIVE_Y, NEGATIVE_X, NEGATIVE_Y };
}
