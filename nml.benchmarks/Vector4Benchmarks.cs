using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
    class Vector4Benchmarks
    {
        static Vector4 a = new Vector4(6.0f, -2.0f, 1.0f, 2.0f);
        static Vector4 b = new Vector4(-4.0f, 4.0f, 1.0f, 3.0f);
        static float scalar = 2.0f;

        [Benchmark(Name = "Vector4 + Vector4")]
        public void Add()
        {
            var r = a + b;
        }

        [Benchmark(Name = "Vector4 - Vector4")]
        public void Subtract()
        {
            var r = a - b;
        }

        [Benchmark(Name = "Vector4 * scalar")]
        public void Multiply()
        {
            var r = a * scalar;
        }

        [Benchmark(Name = "Vector4 / scalar")]
        public void Divide()
        {
            var r = a / scalar;
        }

        [Benchmark(Name = "Vector4 Dot product")]
        public void Dot()
        {
            var r = Vector4.Dot(a, b);
        }

        [Benchmark(Name = "Vector4 Length")]
        public void Length()
        {
            var r = a.Length;
        }

        [Benchmark(Name = "Vector4 LengthSquared")]
        public void LengthSquared()
        {
            var r = a.LengthSquared;
        }

        [Benchmark(Name = "Vector4 Normalise")]
        public void Normalise()
        {
            a.Normalise();
        }

        [Benchmark(Name = "Vector4 IsNormalised")]
        public void IsNormalised()
        {
            var r = a.IsNormalised;
        }
    }
}
