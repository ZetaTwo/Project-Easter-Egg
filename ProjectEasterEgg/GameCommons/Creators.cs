using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public class Creators
    {
        public static T[][][] CreateWorldMatrix<T>(Position size)
        {
            T[][][] matrix = new T[size.X][][];

            for (int x = 0; x < size.X; x++)
            {
                matrix[x] = new T[size.Y][];
                for (int y = 0; y < size.Y; y++)
                {
                    matrix[x][y] = new T[size.Z];
                }
            }

            return matrix;
        }
    }
}
