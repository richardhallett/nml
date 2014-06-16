
using System;
namespace Nml
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
        /// A faster combined implementation of SinCos 
        /// Useful for when SSE is not available and you want something significantly faster than separate System.Math.Sin/Cos
        /// </summary>
        /// <param name="x">An angle in radians</param>
        /// <param name="sin">The sine.</param>
        /// <param name="cos">The cosine.</param>
        /// <exception cref="System.OverflowException">Function does not support angles over 8192.</exception>
        public static void SinCos(float x, out float sin, out float cos)
        {
            int signS = 1;
            int signC = 1;
            if (x < 0)
            {
                signS = -1;
                x = -x;
            }
            if (x > lossth)
                throw new OverflowException("Function does not support angles over 8192.");

            uint j = (uint)(FOPI * x);
            float y = j;
            if ((j & 0x1) != 0)
            {
                j++;
                y++;
            }

            j &= 0x7;
            if (j > 3)
            {
                signS = -signS;
                signC = -signC;
                j -= 4;
            }

            if (j > 1)
                signC = -signC;

            x = ((x - y * DP1) - y * DP2) - y * DP3;
            float z = x * x;

            var a = CosCof0;
            a = a * z + CosCof1;
            a = a * z + CosCof2;
            a *= z * z;
            a -= 0.5f * z;
            a += 1.0f;

            var b = SinCof0;
            b = b * z + SinCof1;
            b = b * z + SinCof2;
            b *= z * x;
            b += x;

            if (j == 1 || j == 2)
            {
                sin = a;
                cos = b;
            }
            else
            {
                sin = b;
                cos = a;
            }

            if (signS < 0)
                sin = -sin;
            if (signC < 0)
                cos = -cos;
        }

        const float lossth = 8192.0f;
        const float FOPI = 1.27323954473516f;
        const float DP1 = 0.78515625f;
        const float DP2 = 2.4187564849853515625e-4f;
        const float DP3 = 3.77489497744594108e-8f;
        const float CosCof0 = 2.443315711809948E-005f;
        const float CosCof1 = -1.388731625493765E-003f;
        const float CosCof2 = 4.166664568298827E-002f;
        const float SinCof0 = -1.9515295891E-4f;
        const float SinCof1 = 8.3321608736E-3f;
        const float SinCof2 = -1.6666654611E-1f;

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
