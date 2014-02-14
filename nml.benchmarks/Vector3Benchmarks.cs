using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
    class Vector3Benchmarks
    {
        static Vector3 a = new Vector3(6.0f, -2.0f, 1.0f);
        static Vector3 b = new Vector3(-4.0f, 4.0f, 1.0f);
        static float scalar = 2.0f;

        [Benchmark(Name = "Vector3 + Vector3")]
        public void Add()
        {
            var r = a + b;
        }

        [Benchmark(Name = "Vector3 - Vector3")]
        public void Subtract()
        {
            var r = a - b;
        }

        [Benchmark(Name = "Vector3 * scalar")]
        public void Multiply()
        {
            var r = a * scalar;
        }

        [Benchmark(Name = "Vector3 / scalar")]
        public void Divide()
        {
            var r = a / scalar;
        }

        [Benchmark(Name = "Vector3 Dot product")]
        public void Dot()
        {
            var r = Vector3.Dot(a, b);
        }

        [Benchmark(Name = "Vector3 Length")]
        public void Length()
        {
            var r = a.Length;
        }

        [Benchmark(Name = "Vector3 LengthSquared")]
        public void LengthSquared()
        {
            var r = a.LengthSquared;
        }

        [Benchmark(Name = "Vector3 Normalise")]
        public void Normalise()
        {
            a.Normalise();
        }

        [Benchmark(Name = "Vector3 IsNormalised")]
        public void IsNormalised()
        {
            var r = a.IsNormalised;
        }
    }
}
