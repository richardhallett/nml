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

        [Benchmark(Name = "Vector2 + Vector2")]
        public void Add()
        {            
            var r = a + b;
        }

        [Benchmark(Name = "Vector2 - Vector2")]
        public void Subtract()
        {           
            var r = a - b;
        }

        [Benchmark(Name = "Vector2 * scalar")]
        public void Multiply()
        {           
            var r = a * scalar;
        }

        [Benchmark(Name = "Vector2 / scalar")]
        public void Divide()
        {           
            var r = a / scalar;
        }

        [Benchmark(Name = "Vector2 Dot product")]
        public void Dot()
        {            
            var r = Vector2.Dot(a, b);
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
            a.Normalise();
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
    }
}
