using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
    class Vector2Benchmarks
    {
        static Vector2f a = new Vector2f(6.0f, -2.0f);
        static Vector2f b = new Vector2f(-4.0f, 4.0f);
        static float scalar = 2.0f;

        [Benchmark]
        public void Vector2AddBenchmark()
        {            
            var r = a + b;
        }

        [Benchmark]
        public void Vector2SubtractBenchmark()
        {           
            var r = a - b;
        }

        [Benchmark]
        public void Vector2MultiplyBenchmark()
        {           
            var r = a * scalar;
        }

        [Benchmark]
        public void Vector2DivideBenchmark()
        {           
            var r = a / scalar;
        }

        [Benchmark]
        public void Vector2DotBenchmark()
        {            
            var r = Vector2f.Dot(a, b);
        }

        [Benchmark]
        public void Vector2LengthBenchmark()
        {           
            var r = a.Length;
        }

        [Benchmark]
        public void Vector2LengthSquaredBenchmark()
        {          
            var r = a.LengthSquared;
        }

        [Benchmark]
        public void Vector2NormaliseBenchmark()
        {           
            a.Normalise();
        }

        [Benchmark]
        public void Vector2IsNormaliseBenchmark()
        {            
            var r = a.IsNormalised;
        }
    }
}
