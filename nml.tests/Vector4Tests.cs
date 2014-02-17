using System;
using Xunit;

namespace nml.tests
{
    public class Vector4Tests
    {        
        [Fact]
        public void Vector4ArrayIndexTest()
        {
            var a = new Vector4(5.0f, 2.0f, -1.0f, 3.0f);

            float x = a[0];
            float y = a[1];
            float z = a[2];
            float w = a[3];

            Assert.Equal<float>(5.0f, x);
            Assert.Equal<float>(2.0f, y);
            Assert.Equal<float>(-1.0f, z);
            Assert.Equal<float>(3.0f, w);
        }

        [Fact]
        public void Vector4ZeroTest()
        {
            var a = Vector4.Zero;
            var expectedResult = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);            

            Assert.Equal<Vector4>(expectedResult, a);
        }

        [Fact]
        public void Vector4OneTest()
        {
            var a = Vector4.One;
            var expectedResult = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            Assert.Equal<Vector4>(expectedResult, a);
        }

        [Fact]
        public void Vector4UnitXTest()
        {
            var a = Vector4.UnitX;
            var expectedResult = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);

            Assert.Equal<Vector4>(expectedResult, a);
        }

        [Fact]
        public void Vector4UnitYTest()
        {
            var a = Vector4.UnitY;
            var expectedResult = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);

            Assert.Equal<Vector4>(expectedResult, a);
        }

        [Fact]
        public void Vector4UnitZTest()
        {
            var a = Vector4.UnitZ;
            var expectedResult = new Vector4(0.0f, 0.0f, 1.0f, 0.0f);

            Assert.Equal<Vector4>(expectedResult, a);
        }

        [Fact]
        public void Vector4UnitWTest()
        {
            var a = Vector4.UnitW;
            var expectedResult = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

            Assert.Equal<Vector4>(expectedResult, a);
        }

        [Fact]
        public void Vector4AddTest()
        {
            var a = new Vector4(6.0f, -2.0f, 1.0f, 2.0f);
            var b = new Vector4(-4.0f, 4.0f, 1.0f, 3.0f);
            var expectedResult = new Vector4(2.0f, 2.0f, 2.0f, 5.0f);

            var r = a + b;

            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Vector4SubtractTest()
        {
            var a = new Vector4(6.0f, -2.0f, 1.0f, 2.0f);
            var b = new Vector4(-4.0f, 4.0f, 1.0f, 3.0f);
            var expectedResult = new Vector4(10.0f, -6.0f, 0.0f, -1.0f);

            var r = a - b;

            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Vector4MultiplyTest()
        {
            var a = new Vector4(6.0f, -2.0f, 2.0f, 4.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector4(12.0f, -4.0f, 4.0f, 8.0f);

            var r = a * scalar;

            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Vector4DivideTest()
        {
            var a = new Vector4(6.0f, -2.0f, 2.0f, 4.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector4(3.0f, -1.0f, 1.0f, 2.0f);

            var r = a / scalar;

            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Vector4DotTest()
        {
            var a = new Vector4(6.0f, -2.0f, 1.0f, 3.0f);
            var b = new Vector4(-4.0f, 4.0f, 2.0f, 4.0f);
            var expectedResult = -18.0f;

            var r = Vector4.Dot(a, b);

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector4LengthTest()
        {
            var a = new Vector4(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = 5.477226f;
            var r = a.Length;

            Assert.True(Math.Abs(r - expectedResult) < 1e-3f);
        }

        [Fact]
        public void Vector4LengthSquaredTest()
        {
            var a = new Vector4(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = 30.0f;

            var r = a.LengthSquared;

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector4NormaliseTest()
        {
            var a = new Vector4(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = new Vector4(0.5477226f, 0.730296731f, 0.182574183f, 0.365148365f);

            a.Normalise();

            Assert.True(a.Equals(expectedResult, 1e-3f));                
        }

        [Fact]
        public void Vector4IsNormaliseTest()
        {
            var a = new Vector4(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = true;

            a.Normalise();

            var r = a.IsNormalised;

            Assert.Equal<bool>(expectedResult, r);
        }

        [Fact]
        public void Vector4LerpTest()
        {
            var a = new Vector4(3.0f, 4.0f, 2.0f, 1.5f);
            var b = new Vector4(6.0f, 8.0f, 4.0f, 2.5f);

            var r1 = Vector4.Lerp(a, b, 0);
            var expectedResult1 = new Vector4(3.0f, 4.0f, 2.0f, 1.5f);

            var r2 = Vector4.Lerp(a, b, 1);
            var expectedResult2 = new Vector4(6.0f, 8.0f, 4.0f, 2.5f);

            var r3 = Vector4.Lerp(a, b, 0.5f);
            var expectedResult3 = new Vector4(4.5f, 6.0f, 3.0f, 2.0f);

            Assert.Equal<Vector4>(expectedResult1, r1);
            Assert.Equal<Vector4>(expectedResult2, r2);
            Assert.Equal<Vector4>(expectedResult3, r3);
        }

        [Fact]
        public void Vector4DistanceTest()
        {
            var a = new Vector4(3.0f, 4.0f, 2.0f, 1.5f);
            var b = new Vector4(6.0f, 8.0f, 4.0f, 2.5f);

            var expectedResult = 5.47723f;

            var r = a.Distance(b);

            Assert.True(Math.Abs(r - expectedResult) < 1e-3f);
        }


        [Fact]
        public void Vector4DistanceSquaredTest()
        {
            var a = new Vector4(3.0f, 4.0f, 2.0f, 1.5f);
            var b = new Vector4(6.0f, 8.0f, 4.0f, 2.5f);

            var expectedResult = 30.0f;

            var r = a.DistanceSquared(b);

            Assert.Equal<float>(expectedResult, r);
        }
    }
}
