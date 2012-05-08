using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Mindstep.EasterEgg.Commons;
using Mindstep.EasterEgg.Commons.Graphic;
using System.Drawing.Drawing2D;
using Xna = Microsoft.Xna.Framework;

namespace EggEnginePipeline
{
    internal static class Extensions
    {
        public static IEnumerable<int> IndexOf<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                yield return list.IndexOf(item);
            }
        }

        public static IEnumerable<T> GetRange<T>(this List<T> list, IEnumerable<int> indexes)
        {
            foreach (int index in indexes)
            {
                yield return list[index];
            }
        }
    }
}
