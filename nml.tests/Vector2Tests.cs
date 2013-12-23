using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace nml.tests
{
    [TestClass]
    public class Vector2Tests
    {        
        [TestMethod]
        public void Vector2ArrayIndexTest()
        {
            var a = new Vector2(5.0f, 2.0f);

            float x = a[0];
            float y = a[1];

            Assert.AreEqual<float>(5.0f, x);
            Assert.AreEqual<float>(2.0f, y);
        }

        [TestMethod]
        public void Vector2ZeroTest()
        {
            var a = Vector2.Zero;
            var expectedResult = new Vector2(0.0f, 0.0f);            

            Assert.AreEqual<Vector2>(expectedResult, a);
        }

        [TestMethod]
        public void Vector2OneTest()
        {
            var a = Vector2.One;
            var expectedResult = new Vector2(1.0f, 1.0f);

            Assert.AreEqual<Vector2>(expectedResult, a);
        }

        [TestMethod]
        public void Vector2UnitXTest()
        {
            var a = Vector2.UnitX;
            var expectedResult = new Vector2(1.0f, 0.0f);

            Assert.AreEqual<Vector2>(expectedResult, a);
        }

        [TestMethod]
        public void Vector2UnitYTest()
        {
            var a = Vector2.UnitY;
            var expectedResult = new Vector2(0.0f, 1.0f);

            Assert.AreEqual<Vector2>(expectedResult, a);
        }

        [TestMethod]
        public void Vector2AddTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            var b = new Vector2(-4.0f, 4.0f);
            var expectedResult = new Vector2(2.0f, 2.0f);

            var r = a + b;

            Assert.AreEqual<Vector2>(expectedResult, r);
        }

        [TestMethod]
        public void Vector2SubtractTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            var b = new Vector2(-4.0f, 4.0f);
            var expectedResult = new Vector2(10.0f, -6.0f);

            var r = a - b;

            Assert.AreEqual<Vector2>(expectedResult, r);
        }

        [TestMethod]
        public void Vector2MultiplyTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector2(12.0f, -4.0f);

            var r = a * scalar;

            Assert.AreEqual<Vector2>(expectedResult, r);
        }

        [TestMethod]
        public void Vector2DivideTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            float scalar = 2.0f;
            var expectedResult = new Vector2(3.0f, -1.0f);

            var r = a / scalar;

            Assert.AreEqual<Vector2>(expectedResult, r);
        }

        [TestMethod]
        public void Vector2DotTest()
        {
            var a = new Vector2(6.0f, -2.0f);
            var b = new Vector2(-4.0f, 4.0f);
            var expectedResult = -32.0f;

            var r = Vector2.Dot(a, b);

            Assert.AreEqual<float>(expectedResult, r);
        }

        [TestMethod]
        public void Vector2LengthTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var expectedResult = 5.0f;

            var r = a.Length;

            Assert.AreEqual<float>(expectedResult, r);
        }

        [TestMethod]
        public void Vector2LengthSquaredTest()
        {
            var a = new Vector2(3.0f, 4.0f);
            var expectedResult = 25.0f;

            var r = a.LengthSquared;

            Assert.AreEqual<float>(expectedResult, r);
        }
    }
}
