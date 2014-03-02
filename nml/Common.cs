using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nml
{
    /// <summary>
    /// Common mathematical functions.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Restrict a value to be within the specified max/min range.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            if (value > max)
                value = max;
            if (value < min)
                value = min;

            return value;
        }

        /// <summary>
        /// Restrict a value to be within the specified max/min range.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static double Clamp(double value, double min, double max)
        {
            if (value > max)
                value = max;
            if (value < min)
                value = min;

            return value;
        }

        /// <summary>
        /// Restrict a value to be within the specified max/min range.
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            if (value > max)
                value = max;
            if (value < min)
                value = min;

            return value;
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="a">Source value.</param>
        /// <param name="b">Source value.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is <c>a</c> and 1 is <c>b</c></param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="a">Source value.</param>
        /// <param name="b">Source value.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is <c>a</c> and 1 is <c>b</c></param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
    }
}
