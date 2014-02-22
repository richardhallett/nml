using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
    class Vector2Benchmarks
    {
        static Vector2 a = new Vector2(6.0f, -2.0f);
        static Vector2 b = new Vector2(-4.0f, 4.0f);
        static float scalar = 2.0f;

        [Benchmark(Name = "Vector2 == Vector2")]
        public void Equals()
        {
            var r = a == b;
        }

        [Benchmark(Name = "Vector2 == Vector2 By Ref")]
        public void EqualsByRef()
        {
            var r = Vector2.Equals(a, b);
        }

        [Benchmark(Name = "Vector2 + Vector2")]
        public void Add()
        {
            var r = a + b;
        }

        [Benchmark(Name = "Vector2 + Vector2 By Ref")]
        public void AddByRef()
        {
            Vector2 r;
            Vector2.Add(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector2 - Vector2")]
        public void Subtract()
        {
            var r = a - b;
        }

        [Benchmark(Name = "Vector2 - Vector2 By Ref")]
        public void SubtractByRef()
        {
            Vector2 r;
            Vector2.Subtract(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector2 * scalar")]
        public void Multiply()
        {
            var r = a * scalar;
        }

        [Benchmark(Name = "Vector2 * scalar By Ref")]
        public void MultiplyByRef()
        {
            Vector2 r;
            Vector2.Multiply(ref a, scalar, out r);
        }

        [Benchmark(Name = "Vector2 / scalar")]
        public void Divide()
        {
            var r = a / scalar;
        }

        [Benchmark(Name = "Vector2 / scalar By Ref")]
        public void DivideByRef()
        {
            Vector2 r;
            Vector2.Divide(ref a, scalar, out r);
        }

        [Benchmark(Name = "Vector2 Dot product")]
        public void Dot()
        {
            var r = Vector2.Dot(a, b);
        }

        [Benchmark(Name = "Vector2 Dot product By Ref")]
        public void DotByRef()
        {
            float r;
            Vector2.Dot(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector2 Length")]
        public void Length()
        {
            var r = a.Length;
        }

        [Benchmark(Name = "Vector2 LengthSquared")]
        public void LengthSquared()
        {
            var r = a.LengthSquared;
        }

        [Benchmark(Name = "Vector2 Normalise")]
        public void Normalise()
        {
            var r = Vector2.Normalise(a);
        }

        [Benchmark(Name = "Vector2 Normalise By Ref")]
        public void NormaliseByRef()
        {
            Vector2 result;
            Vector2.Normalise(ref a, out result);
        }

        [Benchmark(Name = "Vector2 IsNormalised")]
        public void IsNormalised()
        {
            var r = a.IsNormalised;
        }

        [Benchmark(Name = "Vector2 Lerp")]
        public void Lerp()
        {
            var r = Vector2.Lerp(a, b, 0.5f);
        }

        [Benchmark(Name = "Vector2 Lerp By Ref")]
        public void LerpByRef()
        {
            Vector2 result;
            Vector2.Lerp(ref a, ref b, 0.5f, out result);
        }

        [Benchmark(Name = "Vector2 Distance")]
        public void Distance()
        {
            var r = Vector2.Distance(a, b);
        }

        [Benchmark(Name = "Vector2 Distance By Ref")]
        public void DistanceByRef()
        {
            float result;
            Vector2.Distance(ref a, ref b, out result);
        }

        [Benchmark(Name = "Vector2 DistanceSquared")]
        public void DistanceSquared()
        {
            var r = Vector2.DistanceSquared(a, b);
        }

        [Benchmark(Name = "Vector2 DistanceSquared By Ref")]
        public void DistanceSquaredByRef()
        {
            float result;
            Vector2.DistanceSquared(ref a, ref b, out result);
        }
    }
}
