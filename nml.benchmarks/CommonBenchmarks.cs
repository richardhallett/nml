﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nml.benchmarks
{
    public class CommonBenchmarks
    {
        [Benchmark(Name = "Common Lerp float")]
        public void Lerp()
        {
            var r = Common.Lerp(0.0f, 5.0f, 0.5f);
        }

        [Benchmark(Name = "Common Lerp double")]
        public void LerpDouble()
        {
            double a = 0.5E+7;
            double b = 1.0E+7; 
            var r = Common.Lerp(a, b, 0.5f);
        }
    }
}