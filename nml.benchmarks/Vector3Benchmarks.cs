
namespace nml.benchmarks
{
    #pragma warning disable 219
    class Vector3Benchmarks
    {
        static Vector3 a = new Vector3(6.0f, -2.0f, 1.0f);
        static Vector3 b = new Vector3(-4.0f, 4.0f, 1.0f);
        static float scalar = 2.0f;

        [Benchmark(Name = "Vector3 == Vector3")]
        public void Equals()
        {
            var r = a == b;
        }

        [Benchmark(Name = "Vector3 == Vector3 By Ref")]
        public void EqualsByRef()
        {
            var r = Vector3.Equals(a, b);
        }

        [Benchmark(Name = "Vector3 + Vector3")]
        public void Add()
        {
            var r = a + b;
        }

        [Benchmark(Name = "Vector3 + Vector3 By Ref")]
        public void AddByRef()
        {
            Vector3 r;
            Vector3.Add(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector3 - Vector3")]
        public void Subtract()
        {
            var r = a - b;
        }

        [Benchmark(Name = "Vector3 - Vector3 By Ref")]
        public void SubtractByRef()
        {
            Vector3 r;
            Vector3.Subtract(ref a, ref b, out r);
        }

        [Benchmark(Name = "Vector3 * scalar")]
        public void Multiply()
        {
            var r = a * scalar;
        }

        [Benchmark(Name = "Vector3 * scalar By Ref")]
        public void MultiplyByRef()
        {
            Vector3 r;
            Vector3.Multiply(ref a, scalar, out r);
        }

        [Benchmark(Name = "Vector3 / scalar")]
        public void Divide()
        {
            var r = a / scalar;
        }

        [Benchmark(Name = "Vector3 / scalar By Ref")]
        public void DivideByRef()
        {
            Vector3 r;
            Vector3.Divide(ref a, scalar, out r);
        }

        [Benchmark(Name = "Vector3 Dot product")]
        public void Dot()
        {
            var r = Vector3.Dot(a, b);
        }

        [Benchmark(Name = "Vector3 Dot product By Ref")]
        public void DotByRef()
        {
            float r;
            Vector3.Dot(ref a, ref b, out r);
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
            var r = Vector3.Normalise(a);
        }

        [Benchmark(Name = "Vector3 Normalise By Ref")]
        public void NormaliseByRef()
        {
            Vector3 result;
            Vector3.Normalise(ref a, out result);
        }

        [Benchmark(Name = "Vector3 IsNormalised")]
        public void IsNormalised()
        {
            var r = a.IsNormalised;
        }

        [Benchmark(Name = "Vector3 Lerp")]
        public void Lerp()
        {
            var r = Vector3.Lerp(a, b, 0.5f);
        }

        [Benchmark(Name = "Vector3 Lerp By Ref")]
        public void LerpByRef()
        {
            Vector3 result;
            Vector3.Lerp(ref a, ref b, 0.5f, out result);
        }

        [Benchmark(Name = "Vector3 Distance")]
        public void Distance()
        {
            var r = Vector3.Distance(a, b);
        }

        [Benchmark(Name = "Vector3 Distance By Ref")]
        public void DistanceByRef()
        {
            float result;
            Vector3.Distance(ref a, ref b, out result);
        }

        [Benchmark(Name = "Vector3 DistanceSquared")]
        public void DistanceSquared()
        {
            var r = Vector3.DistanceSquared(a, b);
        }

        [Benchmark(Name = "Vector3 DistanceSquared By Ref")]
        public void DistanceSquaredByRef()
        {
            float result;
            Vector3.DistanceSquared(ref a, ref b, out result);
        }

        [Benchmark(Name = "Vector3 CrossProduct")]
        public void CrossProduct()
        {
            var r = Vector3.Cross(a, b);
        }

        [Benchmark(Name = "Vector3 CrossProduct By Ref")]
        public void CrossProductByRef()
        {
            Vector3 result;
            Vector3.Cross(ref a, ref b, out result);
        }
    }
}
