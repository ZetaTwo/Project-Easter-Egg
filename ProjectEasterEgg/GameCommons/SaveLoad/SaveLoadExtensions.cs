using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public static class SaveLoadExtensions
    {
        public static string GetSaveString(this Position pos)
        {
            return pos.X + " " + pos.Y + " " + pos.Z;
        }

        public static string GetSaveString(this Point coord)
        {
            return coord.X + " " + coord.Y;
        }

        public static Position LoadPosition(this string s)
        {
            string[] p = s.Split(' ');
            return new Position(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
        }

        public static Point LoadPoint(this string s)
        {
            string[] p = s.Split(' ');
            return new Point(int.Parse(p[0]), int.Parse(p[1]));
        }
    }
}
