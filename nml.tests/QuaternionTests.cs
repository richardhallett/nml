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
        
        [Fact]
        public void QuaternionTransformVector3Test()
        {
            var a = new Quaternion(1.0f, 0.0f, 0.0f, 0.0f);
            var b = new Vector3(3, 4, 5);

            var expectedResult = new Vector3(3, -4, -5);
            
            var r = a * b;

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }


        [Fact]
        public void QuaternionRotateXTest()
        {
            //var a = Matrix4.RotateX((float)Math.PI);

            //var b = new Vector4(3, 4, 5, 1);

            //var expectedResult = new Vector4(3, -4, -5, 1);

            //var r = a.Transform(b);

            //Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionRotateYTest()
        {
            //var a = Matrix4.RotateY((float)Math.PI);

            //var b = new Vector4(3, 4, 5, 1);

            //var expectedResult = new Vector4(-3, 4, -5, 1);

            //var r = a.Transform(b);

            //Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionRotateZTest()
        {
            //var a = Matrix4.RotateZ((float)Math.PI);

            //var b = new Vector4(3, 4, 5, 1);

            //var expectedResult = new Vector4(-3, -4, 5, 1);

            //var r = a.Transform(b);

            //Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionRotateAxisTest()
        {
            var rotXAxis = Quaternion.RotateAxis(Vector3.UnitX, (float)Math.PI);
            var rotYAxis = Quaternion.RotateAxis(Vector3.UnitY, (float)Math.PI);
            var rotZAxis = Quaternion.RotateAxis(Vector3.UnitZ, (float)Math.PI);

            var vec = new Vector3(3, 4, 5);

            var expectedResultX = new Vector3(3, -4, -5);
            var expectedResultY = new Vector3(-3, 4, -5);
            var expectedResultZ = new Vector3(-3, -4, 5);

            var rX = rotXAxis.Transform(vec);
            var rY = rotYAxis.Transform(vec);
            var rZ = rotZAxis.Transform(vec);

            Assert.True(rX.Equals(expectedResultX, 1e-3f));
            Assert.True(rY.Equals(expectedResultY, 1e-3f));
            Assert.True(rZ.Equals(expectedResultZ, 1e-3f));
        }

        [Fact]
        public void QuaternionGetAxisAngleTest()
        {
            var rotQuat = Quaternion.RotateAxis(Vector3.UnitX, (float)Math.PI);
            var expectedResult = new Vector4(1.0f, 0, 0, (float)Math.PI);

            var r = rotQuat.GetAxisAngle();

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void QuaternionRotateEulerTest()
        {
            var rotQuat = Quaternion.RotateEuler((float)Math.PI, 0.0f, 0.0f);
            var vec = new Vector3(3, 4, 5);

            var expectedResult = new Vector3(-3, 4, -5);

            var r = rotQuat * vec;

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }
    }
}
