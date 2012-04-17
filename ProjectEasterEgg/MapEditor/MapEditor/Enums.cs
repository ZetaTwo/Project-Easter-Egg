using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor
{
    enum EditingMode
    {
        Block,
        Texture,
        TextureProjection,
    }

    enum BlockDrawState
    {
        Solid,
        Wireframe,
        None,
    }

    enum ClickOperation
    {
        Add,
        Subtract,
        Toggle,
        Replace,
        Copy,
    }
}
