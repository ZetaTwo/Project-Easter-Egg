using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindstep.EasterEgg.Commons
{
    public static class MathExtensions
    {
        public static int Clamp(this int i, int min, int max)
        {
            return i.lowerLimit(min).upperLimit(max);
        }
        public static float Clamp(this float f, float min, float max)
        {
            return f.lowerLimit(min).upperLimit(max);
        }

        public static int lowerLimit(this int i, int lowerLimit)
        {
            return Math.Max(i, lowerLimit);
        }
        public static float lowerLimit(this float f, float lowerLimit)
        {
            return Math.Max(f, lowerLimit);
        }

        public static int upperLimit(this int i, int upperLimit)
        {
            return Math.Min(i, upperLimit);
        }
        public static float upperLimit(this float f, float upperLimit)
        {
            return Math.Min(f, upperLimit);
        }

        public static bool BetweenInclusive(this int i, int above, int below)
        {
            return above <= i && i <= below;
        }
        public static bool BetweenInclusive(this float f, float above, float below)
        {
            return above <= f && f <= below;
        }

        public static bool BetweenExclusive(this int i, int above, int below)
        {
            return above < i && i < below;
        }
        public static bool BetweenExclusive(this float f, float above, float below)
        {
            return above < f && f < below;
        }

        /// <summary>
        /// Casts a float to an int, rounding it up even if it has a negative value.
        /// </summary>
        /// <param name="f">float to cast</param>
        /// <returns></returns>
        public static int Ceiling(this float f)
        {
            return (int)Math.Ceiling(f);
        }
        public static float Ceiling(this float f, int part)
        {
            return (float)(f * part).Ceiling() / part;
        }

        /// <summary>
        /// Casts a float to an int, rounding it down even if it has a negative value.
        /// </summary>
        /// <param name="f">float to cast</param>
        /// <returns></returns>
        public static int Floor(this float f)
        {
            return (int)Math.Floor(f);
        }
        public static float Floor(this float f, int part)
        {
            return (float)(f * part).Floor() / part;
        }

        public static int Round(this float f)
        {
            return (int)Math.Round(f, MidpointRounding.ToEven);
        }
        public static float Round(this float f, int part)
        {
            return (float)(f * part).Round() / part;
        }
    }
}
