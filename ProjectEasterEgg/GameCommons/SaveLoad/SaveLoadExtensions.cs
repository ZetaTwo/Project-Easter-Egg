using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons.SaveLoad
{
    public static class SaveLoadExtensions
    {
        public static string GetSaveString(this Position pos)
        {
            return pos.X + " " + pos.Y + " " + pos.Z;
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
    }
}
