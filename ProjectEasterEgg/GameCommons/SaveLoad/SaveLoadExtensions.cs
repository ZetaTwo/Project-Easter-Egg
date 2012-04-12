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

        public static Position LoadPosition(this string s)
        {
            string[] p = s.Split(' ');
            return new Position(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]));
        }



        public static string GetSaveString(this Point coord)
        {
            return coord.X + " " + coord.Y;
        }

        public static Point LoadPoint(this string s)
        {
            string[] p = s.Split(' ');
            return new Point(int.Parse(p[0]), int.Parse(p[1]));
        }



        private static string GetEnumSaveString<T>(T enumType)
        {
            return Enum.GetName(typeof(T), enumType);
        }

        private static T LoadEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s, true);
        }

        public static string GetSaveString(this BlockType blockType)
        {
            return GetEnumSaveString(blockType);
        }

        public static BlockType LoadBlockType(this string s)
        {
            return LoadEnum<BlockType>(s);
        }

        public static string GetSaveString(this Facing facing)
        {
            return GetEnumSaveString(facing);
        }

        public static Facing LoadFacing(this string s)
        {
            return LoadEnum<Facing>(s);
        }



        public static string GetSaveString(this int i)
        {
            return i.ToString();
        }

        public static int LoadInt(this string s)
        {
            return int.Parse(s);
        }
    }
}
