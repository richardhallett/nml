using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
    class QuaternionBenchmarks
    {
        static Quaternion a = new Quaternion(3.0f, 4.0f, 1.0f, 2.0f);
        static Quaternion b = new Quaternion(2.0f, 1.0f, 6.0f, 1.0f);
        static Vector3 vec = new Vector3(3, 4, 5);

        [Benchmark(Name = "Quaternion Length")]
        public void Length()
        {
            var r = a.Length;
        }

        [Benchmark(Name = "Quaternion LengthSquared")]
        public void LengthSquared()
        {         
            var r = a.LengthSquared;
        }

        [Benchmark(Name = "Quaternion Normalise")]
        public void Normalise()
        {
            a.Normalise();
        }

        [Benchmark(Name = "Quaternion IsNormalised")]
        public void IsNormalised()
        {
            var r = a.IsNormalised;
        }

        [Benchmark(Name = "Quaternion Conjugate")]
        public void Conjugate()
        {
            a.Conjugate();
        }

        [Benchmark(Name = "Quaternion Invert")]
        public void Invert()
        {           
            var r = Quaternion.Invert(a);
        }

        [Benchmark(Name = "Quaternion * Quaternion")]
        public void MultiplyQuaternion()
        {           
            var r = a * b;
        }

        [Benchmark(Name = "Quaternion * scalar")]
        public void MultiplyScalar()
        {            
            var r = a * 3.0f;
        }

        [Benchmark(Name = "Quaternion * Vector3")]
        public void TransformVector3()
        {            
            var r = a * vec;
        }

        [Benchmark(Name = "Quaternion GetAxisAngle")]
        public void GetAxisAngle()
        {
            var r = a.GetAxisAngle();
        }

        [Benchmark(Name = "Quaternion GetMatrix4x4")]
        public void GetMatrix4x4()
        {
            var r = a.GetMatrix4x4();
        }
    }
}
