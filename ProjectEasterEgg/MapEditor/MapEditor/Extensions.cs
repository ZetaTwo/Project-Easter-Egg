using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.MapEditor.Animations;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Extensions
    {
        public static IEnumerable<Texture2DWithPos> GetAllTextures(this IEnumerable<Animation> animations)
        {
            return animations.SelectMany(
                animation =>
                    animation.Frames.SelectMany(
                    frame =>
                        frame.Textures));
        }
    }
}
