using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.MapEditor
{
    public class Settings
    {
        public Color backgroundColor;
        public BlockDrawState blockDrawState;
        public float opacity;

        public Settings(Color backgroundColor, BlockDrawState blockDrawState, float opacity)
        {
            this.backgroundColor = backgroundColor;
            this.blockDrawState = blockDrawState;
            this.opacity = opacity;
        }
    }
}
