﻿using System;
using Xunit;

namespace nml.tests
{
    public class CommonTests
    {
        
        [Fact]
        public void CommonClampTest()
        {
            float r;
            r = Common.Clamp(1.1f, 0.0f, 1.0f);
            Assert.Equal<float>(1.0f, r);

            r = Common.Clamp(-1.0f, 0.0f, 1.0f);
            Assert.Equal<float>(0.0f, r);

            r = Common.Clamp(0.5f, 0.0f, 1.0f);
            Assert.Equal<float>(0.5f, r);
        }

        [Fact]
        public void CommonLerpFloatTest()
        {
            double a = 0.5;
            double b = 1.0;
            var r = Common.Lerp(a, b, 0.5f);

            Assert.Equal<double>(0.75f, r);
        }

        [Fact]
        public void CommonLerpDoubleTest()
        {
            double a = 0.5E+7;
            double b = 1.0E+7;
            var r = Common.Lerp(a, b, 0.5);

            Assert.Equal<double>(0.75E+7, r);
        }
    }
}