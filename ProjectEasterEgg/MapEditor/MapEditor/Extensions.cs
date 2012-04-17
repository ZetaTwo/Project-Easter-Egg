using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.MapEditor.Animations;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
                        frame.Textures.BackToFront()));
        }

        public static Color GetPixelColor(this Texture2D texture, Point at)
        {
            Color[] colors = new Color[1];
            texture.GetData<Color>(0, new Rectangle(at.X, at.Y, 1, 1), colors, 0, 1);
            return colors[0];
        }
        
        public static void Write(this Stream stream, string stringToWrite)
        {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(stringToWrite);
            stream.Write(bytes, 0, bytes.Length);
        }

        public static IEnumerable<Texture2DWithPos> GetUnderlyingTextures2DWithDoublePos(this IEnumerable<Texture2DWithDoublePos> texs)
        {
            foreach (Texture2DWithDoublePos tex in texs)
            {
                yield return tex.t;
            }
        }
    }
}
