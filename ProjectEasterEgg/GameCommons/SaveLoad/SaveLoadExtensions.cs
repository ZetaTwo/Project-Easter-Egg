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

        public static Position LoadPosition(string s)
        {
            string[] ss = s.Split(' ');
            int x, y, z;
            bool success = int.TryParse(ss[0], out x);
            success &= int.TryParse(ss[1], out y);
            success &= int.TryParse(ss[2], out z);
            return new Position(x, y, z);
        }

        public static Point LoadPoint(string s)
        {
            string[] ss = s.Split(' ');
            int x, y;
            bool success = int.TryParse(ss[0], out x);
            success &= int.TryParse(ss[1], out y);
            return new Point(x, y);
        }
    }
}
