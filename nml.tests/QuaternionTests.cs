using System;
using Xunit;

namespace nml.tests
{
    public class QuaternionTests
    {        
      
        [Fact]
        public void QuaternionLengthTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = 5.477226f;
            var r = a.Length;

            Assert.True(Math.Abs(r - expectedResult) < 1e-3f);
        }

        [Fact]
        public void QuaternionLengthSquaredTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = 30.0f;

            var r = a.LengthSquared;

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void QuaternionNormaliseTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 3.0f, 1.0f);
            var expectedResult = new Quaternion(0.5070925f, 0.6761234f, 0.5070925f, 0.1690308f);

            a.Normalise();

            Assert.True(a.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionIsNormaliseTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 3.0f, 1.0f);
            var expectedResult = true;

            a.Normalise();

            var r = a.IsNormalised;

            Assert.Equal<bool>(expectedResult, r);
        }    

        [Fact]
        public void QuaternionConjugateTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 3.0f, 1.0f);
            var expectedResult = new Quaternion(-3.0f, -4.0f, -3.0f, 1.0f);

            a.Conjugate();

            Assert.True(a.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionInvertTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 3.0f, 1.0f);
            var expectedResult = new Quaternion(-0.08571429f, -0.1142857f, -0.08571429f, 0.02857143f);

            var r = Quaternion.Invert(a);

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionMultiplyQuaternionTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 3.0f, 1.0f);
            var b = new Quaternion(2.0f, 1.0f, 6.0f, 1.0f);
            var expectedResult = new Quaternion(26.0f, -7.0f, 4.0f, -27.0f);

            var r = a * b;

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionMultiplyScalarTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 3.0f, 1.0f);
            var b = 3.0f;
            var expectedResult = new Quaternion(9.0f, 12.0f, 9.0f, 3.0f);

            var r = a * b;

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }
    }
}
