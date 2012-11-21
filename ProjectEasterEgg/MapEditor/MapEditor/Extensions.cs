using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mindstep.EasterEgg.Commons.SaveLoad;
using System.Windows.Forms;

namespace Mindstep.EasterEgg.MapEditor
{
    static class Extensions
    {
        public static IEnumerable<Texture2DWithPos> GetAllTextures(this IEnumerable<SaveAnimation<Texture2DWithPos>> animations)
        {
            return animations.SelectMany(
                animation =>
                    animation.Frames.SelectMany(
                    frame =>
                        frame.Images.BackToFront()));
        }

        public static Color GetPixelColor(this Texture2D texture, Point at)
        {
            Color[] colors = new Color[1];
            texture.GetData<Color>(0, new Rectangle(at.X, at.Y, 1, 1), colors, 0, 1);
            return colors[0];
        }

        //public static IEnumerable<Texture2DWithPos> GetUnderlyingTextures2DWithDoublePos(this IEnumerable<Texture2DWithDoublePos> texs)
        //{
        //    foreach (Texture2DWithDoublePos tex in texs)
        //    {
        //        yield return tex.t;
        //    }
        //}
    }
}
