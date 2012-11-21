using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.MapEditor
{
    public enum EditingMode
    {
        Block,
        Texture,
        TextureProjection,
    }

    public enum BlockDrawState
    {
        Solid,
        Wireframe,
        None,
    }

    public enum ClickOperation
    {
        Add,
        Subtract,
        Toggle,
        Replace,
        Copy,
    }
}
