using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mindstep.EasterEgg.Commons
{
    public class Position
    {
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private int z;
        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        public Position(Vector3 position)
        {
            X = (int)position.X;
            Y = (int)position.Y;
            Z = (int)position.Z;
        }

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Position Clone()
        {
            return new Position(X, Y, Z);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public override string ToString()
        {
            return "[Position " + X + "," + Y + "," + Z + "]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Position p = obj as Position;
            if ((System.Object)p == null)
            {
                return false;
            }

            return p.X == X && p.Y == Y && p.Z == Z;
        }

        public static bool operator==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }

        public static Position operator+(Position p1, Position p2)
        {
            return new Position(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Position operator -(Position p1, Position p2)
        {
            return p1 + (-p2);
        }

        public static Position operator -(Position p)
        {
            return new Position(-p.X, -p.Y, -p.Z);
        }

        public static Position operator +(Position p)
        {
            return p;
        }
    }
}
