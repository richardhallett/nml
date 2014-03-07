using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
    #pragma warning disable 219
    class Vector4Benchmarks
    {
        static Vector4 a = new Vector4(6.0f, -2.0f, 1.0f, 2.0f);
        static Vector4 b = new Vector4(-4.0f, 4.0f, 1.0f, 3.0f);
        static float scalar = 2.0f;

        [Benchmark(Name = "Vector4 == Vector4")]
        public void Equals()
        {
            var r = a == b;
        }

        [Benchmark(Name = "Vector4 == Vector4 By Ref")]
        public void EqualsByRef()
        {
            var r = Vector4.Equals(a, b);
        }

        [Benchmark(Name = "Vector4 + Vector4")]
        public void Add()
        {
            var r = a + b;
        }

        [Benchmark(Name = "Vector4 + Vector4 By Ref")]
        public void AddByRef()
        {
            Vector4 r;
            Vector4.Add(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector4 - Vector4")]
        public void Subtract()
        {
            var r = a - b;
        }

        [Benchmark(Name = "Vector4 - Vector4 By Ref")]
        public void SubtractByRef()
        {
            Vector4 r;
            Vector4.Subtract(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector4 * scalar")]
        public void Multiply()
        {
            var r = a * scalar;
        }

        [Benchmark(Name = "Vector4 * scalar By Ref")]
        public void MultiplyByRef()
        {
            Vector4 r;
            Vector4.Multiply(ref a, scalar, out r);
        }

        [Benchmark(Name = "Vector4 / scalar")]
        public void Divide()
        {
            var r = a / scalar;
        }

        [Benchmark(Name = "Vector4 / scalar By Ref")]
        public void DivideByRef()
        {
            Vector4 r;
            Vector4.Divide(ref a, scalar, out r);
        }

        [Benchmark(Name = "Vector4 Dot product")]
        public void Dot()
        {
            var r = Vector4.Dot(a, b);
        }

        [Benchmark(Name = "Vector4 Dot product By Ref")]
        public void DotByRef()
        {
            float r;
            Vector4.Dot(ref a, ref b, out r);
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
            var r = Vector4.Normalise(a);
        }

        [Benchmark(Name = "Vector4 Normalise By Ref")]
        public void NormaliseByRef()
        {
            Vector4 result;
            Vector4.Normalise(ref a, out result);
        }

        [Benchmark(Name = "Vector4 IsNormalised")]
        public void IsNormalised()
        {
            var r = a.IsNormalised;
        }

        [Benchmark(Name = "Vector4 Lerp")]
        public void Lerp()
        {
            var r = Vector4.Lerp(a, b, 0.5f);
        }

        [Benchmark(Name = "Vector4 Lerp By Ref")]
        public void LerpByRef()
        {
            Vector4 result;
            Vector4.Lerp(ref a, ref b, 0.5f, out result);
        }

        [Benchmark(Name = "Vector4 Distance")]
        public void Distance()
        {
            var r = Vector4.Distance(a, b);
        }

        [Benchmark(Name = "Vector4 Distance By Ref")]
        public void DistanceByRef()
        {
            float result;
            Vector4.Distance(ref a, ref b, out result);
        }

        [Benchmark(Name = "Vector4 DistanceSquared")]
        public void DistanceSquared()
        {
            var r = Vector4.DistanceSquared(a, b);
        }

        [Benchmark(Name = "Vector4 DistanceSquared By Ref")]
        public void DistanceSquaredByRef()
        {
            float result;
            Vector4.DistanceSquared(ref a, ref b, out result);
        }
    }
}
