using System;
using Xunit;

namespace Nml.Tests
{
    public class Vector3Tests
    {        
        [Fact]
        public void Vector3ArrayIndexTest()
        {
            var a = new Vector3(5.0f, 2.0f, -1.0f);

            float x = a[0];
            float y = a[1];
            float z = a[2];

            Assert.Equal<float>(5.0f, x);
            Assert.Equal<float>(2.0f, y);
            Assert.Equal<float>(-1.0f, z);
        }

        [Fact]
        public void Vector3ZeroTest()
        {
            var a = Vector3.Zero;
            var expectedResult = new Vector3(0.0f, 0.0f, 0.0f);            

            Assert.Equal<Vector3>(expectedResult, a);
        }

        [Fact]
        public void Vector3OneTest()
        {
            var a = Vector3.One;
            var expectedResult = new Vector3(1.0f, 1.0f, 1.0f);

            Assert.Equal<Vector3>(expectedResult, a);
        }

        [Fact]
        public void Vector3UnitXTest()
        {
            var a = Vector3.UnitX;
            var expectedResult = new Vector3(1.0f, 0.0f, 0.0f);

            Assert.Equal<Vector3>(expectedResult, a);
        }

        [Fact]
        public void Vector3UnitYTest()
        {
            var a = Vector3.UnitY;
            var expectedResult = new Vector3(0.0f, 1.0f, 0.0f);

            Assert.Equal<Vector3>(expectedResult, a);
        }

        [Fact]
        public void Vector3UnitZTest()
        {
            var a = Vector3.UnitZ;
            var expectedResult = new Vector3(0.0f, 0.0f, 1.0f);

            Assert.Equal<Vector3>(expectedResult, a);
        }

        [Fact]
        public void Vector3AddTest()
        {
            var a = new Vector3(6.0f, -2.0f, 1.0f);
            var b = new Vector3(-4.0f, 4.0f, 1.0f);
            var expectedResult = new Vector3(2.0f, 2.0f, 2.0f);

            var r = a + b;

            Assert.Equal<Vector3>(expectedResult, r);
        }

        [Fact]
        public void Vector3SubtractTest()
        {
            var a = new Vector3(6.0f, -2.0f, 1.0f);
            var b = new Vector3(-4.0f, 4.0f, 1.0f);
            var expectedResult = new Vector3(10.0f, -6.0f, 0.0f);

            var r = a - b;

            Assert.Equal<Vector3>(expectedResult, r);
        }

        [Fact]
        public void Vector3MultiplyTest()
        {
            var a = new Vector3(6.0f, -2.0f, 2.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector3(12.0f, -4.0f, 4.0f);

            var r = a * scalar;

            Assert.Equal<Vector3>(expectedResult, r);
        }

        [Fact]
        public void Vector3DivideTest()
        {
            var a = new Vector3(6.0f, -2.0f, 2.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector3(3.0f, -1.0f, 1.0f);

            var r = a / scalar;

            Assert.Equal<Vector3>(expectedResult, r);
        }

        [Fact]
        public void Vector3DotTest()
        {
            var a = new Vector3(6.0f, -2.0f, 1.0f);
            var b = new Vector3(-4.0f, 4.0f, 2.0f);
            var expectedResult = -30.0f;

            var r = Vector3.Dot(a, b);

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector3LengthTest()
        {
            var a = new Vector3(3.0f, 4.0f, 1.0f);
            var expectedResult = 5.09901953f;

            var r = a.Length;

            Assert.True(Math.Abs(r - expectedResult) < 1e-3f);
        }

        [Fact]
        public void Vector3LengthSquaredTest()
        {
            var a = new Vector3(3.0f, 4.0f, 1.0f);
            var expectedResult = 26.0f;

            var r = a.LengthSquared;

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector3NormaliseTest()
        {
            var a = new Vector3(3.0f, 4.0f, 1.0f);
            var expectedResult = new Vector3(0.5883484f, 0.784464538f, 0.196116135f);

            a.Normalise();

            Assert.True(a.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void Vector3IsNormaliseTest()
        {
            var a = new Vector3(3.0f, 4.0f, 1.0f);
            var expectedResult = true;

            a.Normalise();

            var r = a.IsNormalised;

            Assert.Equal<bool>(expectedResult, r);
        }

        [Fact]
        public void Vector3CrossTest()
        {
            var a = new Vector3(6.0f, -2.0f, 1.0f);
            var b = new Vector3(-4.0f, 4.0f, 2.0f);
            var expectedResult = new Vector3(-8.0f, -16.0f, 16.0f);

            var r = Vector3.Cross(a, b);

            Assert.Equal<Vector3>(expectedResult, r);
        }

        [Fact]
        public void Vector3LerpTest()
        {
            var a = new Vector3(3.0f, 4.0f, 2.0f);
            var b = new Vector3(6.0f, 8.0f, 4.0f);

            var r1 = Vector3.Lerp(a, b, 0);
            var expectedResult1 = new Vector3(3.0f, 4.0f, 2.0f);

            var r2 = Vector3.Lerp(a, b, 1);
            var expectedResult2 = new Vector3(6.0f, 8.0f, 4.0f);

            var r3 = Vector3.Lerp(a, b, 0.5f);
            var expectedResult3 = new Vector3(4.5f, 6.0f, 3.0f);

            Assert.Equal<Vector3>(expectedResult1, r1);
            Assert.Equal<Vector3>(expectedResult2, r2);
            Assert.Equal<Vector3>(expectedResult3, r3);
        }

        [Fact]
        public void Vector3DistanceTest()
        {
            var a = new Vector3(3.0f, 4.0f, 2.0f);
            var b = new Vector3(6.0f, 8.0f, 4.0f);

            var expectedResult = 5.38516f;

            var r = a.Distance(b);
            
            Assert.True(Math.Abs(r - expectedResult) < 1e-3f);
        }


        [Fact]
        public void Vector3DistanceSquaredTest()
        {
            var a = new Vector3(3.0f, 4.0f, 2.0f);
            var b = new Vector3(6.0f, 8.0f, 4.0f);

            var expectedResult = 29.0f;

            var r = a.DistanceSquared(b);

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector3NegateTest()
        {
            var a = new Vector3(3.0f, 4.0f, 8.0f);

            var expectedResult = new Vector3(-3.0f, -4.0f, -8.0f);

            var r = -a;

            Assert.Equal<Vector3>(expectedResult, r);
        }
    }
}
