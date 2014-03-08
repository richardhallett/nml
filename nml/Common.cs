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
        /// Represents the ratio of the circumference of a circle to its diameter as a <see cref="System.Single"/>.
        /// </summary>
        public const float Pi = 3.14159265358979323846f;

        /// <summary>
        /// Represents the value of Pi divided by two as a <see cref="System.Single"/>.
        /// </summary>
        public const float HalfPi = Pi / 2;

        /// <summary>
        /// Represents the value of Pi multiplied by two as a <see cref="System.Single"/>..
        /// </summary>
        public const float TwoPi = 2 * Pi;

        /// <summary>
        /// Represents the natural logarithmic base, as a <see cref="System.Single"/>.
        /// </summary>
        public const float E = 2.71828182845904523536f;

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

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="value">The value to convert in degrees.</param>
        /// <returns>The value after conversion in radians.</returns>
        public static float DegreesToRadians(float value)
        {
            return value * (Pi / 180.0f);
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="value">The value to convert in radians.</param>
        /// <returns>The value after conversion in degrees.</returns>
        public static float RadiansToDegrees(float value)
        {
            return value * (180.0f / Pi);
        }
    }
}
