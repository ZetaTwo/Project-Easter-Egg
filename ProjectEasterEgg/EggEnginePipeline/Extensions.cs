using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Graphics;
using System.Drawing.Drawing2D;
using Xna = Microsoft.Xna.Framework;

namespace EggEnginePipeline
{
    internal static class Extensions
    {
        /// <summary>
        /// The "eater" image steals all the pixels from the "eaten" image
        /// within a offseted by offset
        /// </summary>
        /// <param name="eater"></param>
        /// <param name="eaten"></param>
        /// <param name="offset"></param>
        public static void eat(this Graphics eater, Bitmap eaten, Xna.Point offset)
        {
            eater.DrawImage(eaten, -offset.X, -offset.Y);
            using (Graphics graphics = Graphics.FromImage(eaten))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.FillRegion(Brushes.Transparent, BlockRegions.WholeBlock.Offset(offset));
            }
        }
    }
}
