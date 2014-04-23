using Xunit;

namespace Nml.Tests
{
    public class Vector2Tests
    {        
        [Fact]
        public void Vector2ArrayIndexTest()
        {
            var a = new Vector2(5.0f, 2.0f);

            float x = a[0];
            float y = a[1];

            Assert.Equal<float>(5.0f, x);
            Assert.Equal<float>(2.0f, y);
        }

        [Fact]
        public void Vector2ZeroTest()
        {
            var a = Vector2.Zero;
            var expectedResult = new Vector2(0.0f, 0.0f);            

            Assert.Equal<Vector2>(expectedResult, a);
        }

        [Fact]
        public void Vector2OneTest()
        {
            var a = Vector2.One;
            var expectedResult = new Vector2(1.0f, 1.0f);

            Assert.Equal<Vector2>(expectedResult, a);
        }

        [Fact]
        public void Vector2UnitXTest()
        {
            var a = Vector2.UnitX;
            var expectedResult = new Vector2(1.0f, 0.0f);

            Assert.Equal<Vector2>(expectedResult, a);
        }

        [Fact]
        public void Vector2UnitYTest()
        {
            var a = Vector2.UnitY;
            var expectedResult = new Vector2(0.0f, 1.0f);

            Assert.Equal<Vector2>(expectedResult, a);
        }

        [Fact]
        public void Vector2AddTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            var b = new Vector2(-4.0f, 4.0f);
            var expectedResult = new Vector2(2.0f, 2.0f);

            var r = a + b;

            Assert.Equal<Vector2>(expectedResult, r);
        }

        [Fact]
        public void Vector2SubtractTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            var b = new Vector2(-4.0f, 4.0f);
            var expectedResult = new Vector2(10.0f, -6.0f);

            var r = a - b;

            Assert.Equal<Vector2>(expectedResult, r);
        }

        [Fact]
        public void Vector2MultiplyTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector2(12.0f, -4.0f);

            var r = a * scalar;

            Assert.Equal<Vector2>(expectedResult, r);
        }

        [Fact]
        public void Vector2DivideTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector2(3.0f, -1.0f);

            var r = a / scalar;

            Assert.Equal<Vector2>(expectedResult, r);
        }

        [Fact]
        public void Vector2DotTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            var b = new Vector2(-4.0f, 4.0f);
            var expectedResult = -32.0f;

            var r = Vector2.Dot(a, b);

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector2LengthTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var expectedResult = 5.0f;

            var r = a.Length;

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector2LengthSquaredTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var expectedResult = 25.0f;

            var r = a.LengthSquared;

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector2NormaliseTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var expectedResult = new Vector2(0.6f, 0.8f);

            a.Normalise();

            Assert.Equal<Vector2>(expectedResult, a);
        }

        [Fact]
        public void Vector2IsNormaliseTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var expectedResult = true;

            a.Normalise();

            var r = a.IsNormalised;

            Assert.Equal<bool>(expectedResult, r);
        }

        [Fact]
        public void Vector2LerpTest()
        {            
            var a = new Vector2(3.0f, 4.0f);
            var b = new Vector2(6.0f, 8.0f);
            
            var r1 = Vector2.Lerp(a, b, 0);
            var expectedResult1 = new Vector2(3.0f, 4.0f);

            var r2 = Vector2.Lerp(a, b, 1);
            var expectedResult2 = new Vector2(6.0f, 8.0f);

            var r3 = Vector2.Lerp(a, b, 0.5f);
            var expectedResult3 = new Vector2(4.5f, 6.0f);

            Assert.Equal<Vector2>(expectedResult1, r1);
            Assert.Equal<Vector2>(expectedResult2, r2);
            Assert.Equal<Vector2>(expectedResult3, r3);
        }

        [Fact]
        public void Vector2DistanceTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var b = new Vector2(6.0f, 8.0f);

            var expectedResult = 5.0f;

            var r = a.Distance(b);

            Assert.Equal<float>(expectedResult, r);
        }


        [Fact]
        public void Vector2DistanceSquaredTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var b = new Vector2(6.0f, 8.0f);

            var expectedResult = 25.0f;

            var r = a.DistanceSquared(b);

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Vector2NegateTest()
        {
            var a = new Vector2(3.0f, 4.0f);

            var expectedResult = new Vector2(-3.0f, -4.0f);

            var r = -a;

            Assert.Equal<Vector2>(expectedResult, r);
        }
    }
}
