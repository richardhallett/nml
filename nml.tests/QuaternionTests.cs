﻿using System;
using Xunit;

namespace Nml.Tests
{
    public class QuaternionTests
    {
        [Fact]
        public void QuaternionAddTest()
        {
            var a = new Quaternion(6.0f, -2.0f, 1.0f, 2.0f);
            var b = new Quaternion(-4.0f, 4.0f, 1.0f, 3.0f);
            var expectedResult = new Quaternion(2.0f, 2.0f, 2.0f, 5.0f);

            var r = a + b;

            Assert.Equal<Quaternion>(expectedResult, r);
        }

        [Fact]
        public void QuaternionSubtractTest()
        {
            var a = new Quaternion(6.0f, -2.0f, 1.0f, 2.0f);
            var b = new Quaternion(-4.0f, 4.0f, 1.0f, 3.0f);
            var expectedResult = new Quaternion(10.0f, -6.0f, 0.0f, -1.0f);

            var r = a - b;

            Assert.Equal<Quaternion>(expectedResult, r);
        }

        [Fact]
        public void QuaternionLengthTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 1.0f, 2.0f);
            var expectedResult = 5.477226f;
            var r = a.Length;

            Assert.True(Math.Abs(r - expectedResult) < 1e-3f);
        }

        [Fact]
        public void QuaternionDotTest()
        {
            var a = new Quaternion(6.0f, -2.0f, 1.0f, 3.0f);
            var b = new Quaternion(-4.0f, 4.0f, 2.0f, 4.0f);
            var expectedResult = -18.0f;

            var r = Quaternion.Dot(a, b);

            Assert.Equal<float>(expectedResult, r);
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
            var rotQuatYaw = Quaternion.RotateEuler((float)Math.PI, 0.0f, 0.0f);
            var rotQuatPitch = Quaternion.RotateEuler(0.0f, (float)Math.PI, 0.0f);
            var rotQuatRoll = Quaternion.RotateEuler(0.0f, 0.0f, (float)Math.PI);

            var vec = new Vector3(3, 4, 5);
            
            var rYaw = rotQuatYaw * vec;
            var rPitch = rotQuatPitch * vec;
            var rRoll = rotQuatRoll * vec;            

            Assert.True(rYaw.Equals(new Vector3(-3, 4, -5), 1e-3f));
            Assert.True(rPitch.Equals(new Vector3(3, -4, -5), 1e-3f));
            Assert.True(rRoll.Equals(new Vector3(-3, -4, 5), 1e-3f));            
        }

        [Fact]
        public void QuaternionToMatrix4Test()
        {
            var rotQuat = Quaternion.RotateEuler((float)Math.PI, 0.0f, 0.0f);
            var rotMatrix = rotQuat.GetMatrix4x4();

            var vec = new Vector4(3, 4, 5, 1);
            var expectedResult = new Vector4(-3, 4, -5, 1);

            var r = rotMatrix * vec;

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }


        [Fact]
        public void QuaternionLerpTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 2.0f, 1.5f);
            var b = new Quaternion(6.0f, 8.0f, 4.0f, 2.5f);

            var r = Quaternion.Lerp(a, b, 0.5f);
            var expectedResult = new Quaternion(4.5f, 6.0f, 3.0f, 2.0f);

            Assert.Equal<Quaternion>(expectedResult, r);            
        }

        [Fact]
        public void QuaternionNLerpTest()
        {
            var a = new Quaternion(3.0f, 4.0f, 2.0f, 1.5f);
            var b = new Quaternion(6.0f, 8.0f, 4.0f, 2.5f);

            var r = Quaternion.NLerp(a, b, 0.5f);
            var expectedResult = new Quaternion(0.540758f, 0.72101f, 0.360505f, 0.240337f);

            Assert.True(r.Equals(expectedResult, 1e-3f));            
        }

        [Fact]
        public void QuaternionSlerpTest()
        {
            // Testing slerp, we'll actually just use some rotation quaternions
            // And test the known expected results of a vector.
            var a = Quaternion.RotateEuler(0.0f, 0.0f, (float)Math.PI);
            var b = Quaternion.RotateEuler(0.0f, 0.0f, (float)Math.PI * 2);
            
            var c = Quaternion.Slerp(a, b, 0.0f);
            var d = Quaternion.Slerp(a, b, 1.0f);
            var e = Quaternion.Slerp(a, b, 0.5f);

            var vec = new Vector3(3, 4, 0);
            var r1 = c * vec;
            var r2 = d * vec;
            var r3 = e * vec;
        
            Assert.True(r1.Equals(new Vector3(-3, -4, 0), 1e-3f));
            Assert.True(r2.Equals(new Vector3(3, 4, 0), 1e-3f));
            Assert.True(r3.Equals(new Vector3(4, -3, 0), 1e-3f));
        }
    }
}
