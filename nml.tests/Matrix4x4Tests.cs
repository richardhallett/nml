﻿using System;
using Xunit;

namespace Nml.Tests
{
    public class Matrix4x4Tests
    {
        [Fact]
        public void Matrix4x4ArrayIndexTest()
        {
            var a = new Matrix4x4();

            a[0, 1] = 1.0f;
            a[3, 2] = 5.0f;

            float x = a[0, 1];
            float y = a[3, 2];

            Assert.Equal<float>(1.0f, x);
            Assert.Equal<float>(5.0f, y);
        }

        [Fact]
        public void Matrix4x4MultiplyScalarTest()
        {
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});            
            float scalar = 2.0f;

            var expectedResult = new Matrix4x4(new float[] {  
                                            2.0f, 4.0f, 6.0f, 8.0f,
                                            10.0f, 12.0f, 14.0f, 16.0f,
                                            18.0f, 20.0f, 22.0f, 24.0f,
                                            26.0f, 28.0f, 30.0f, 32.0f});

            var r = a * scalar;

            Assert.Equal<Matrix4x4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4AdditionTest()
        {
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

            var expectedResult = new Matrix4x4(new float[] {  
                                            2.0f, 4.0f, 6.0f, 8.0f,
                                            10.0f, 12.0f, 14.0f, 16.0f,
                                            18.0f, 20.0f, 22.0f, 24.0f,
                                            26.0f, 28.0f, 30.0f, 32.0f});

            var r = a + a;

            Assert.Equal<Matrix4x4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4SubtractionTest()
        {
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

            var expectedResult = new Matrix4x4(0.0f);

            var r = a - a;

            Assert.Equal<Matrix4x4>(expectedResult, r);
        }


        [Fact]
        public void Matrix4x4TransposeTest()
        {
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

            var expectedResult = new Matrix4x4(new float[] { 
                                            1.0f, 5.0f, 9.0f, 13.0f,
                                            2.0f, 6.0f, 10.0f, 14.0f,
                                            3.0f, 7.0f, 11.0f, 15.0f,
                                            4.0f, 8.0f, 12.0f, 16.0f});

            a.Transpose();

            Assert.Equal<Matrix4x4>(expectedResult, a);
        }

        [Fact]
        public void Matrix4x4MultiplyMatrixTest()
        {
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});
            

            var expectedResult = new Matrix4x4(new float[] {  
                                            90.0f, 100.0f, 110.0f, 120.0f,
                                            202.0f, 228.0f, 254.0f, 280.0f,
                                            314.0f, 356.0f, 398.0f, 440.0f,
                                            426.0f, 484.0f, 542.0f, 600.0f});

            var r = a * a;

            Assert.Equal<Matrix4x4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4DeterminantTest()
        {
            // Although i've been using this elsewhere as I like ordered numbers, as it turns out works pretty well for getting a zero based determinant (singular matrix, i.e. no inverse)
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});


            var expectedResult = 0.0f;

            var r = a.Determinant;

            Assert.Equal<float>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4InvertTest()
        {            
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 1.0f, 0.0f, 0.0f,
                                            1.0f, 1.0f, 1.0f, 0.0f,
                                            0.0f, 1.0f, 1.0f, 0.0f,
                                            0.0f, 0.0f, 0.0f, 1.0f});


            var expectedResult = new Matrix4x4(new float[] { 
                                            0.0f, 1.0f, -1.0f, 0.0f,
                                            1.0f, -1.0f, 1.0f, 0.0f,
                                            -1.0f, 1.0f, 0.0f, 0.0f,
                                            0.0f, 0.0f, 0.0f, 1.0f});

            var r = Matrix4x4.Invert(a);

            Assert.Equal<Matrix4x4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4TransformTest()
        {
            var a = new Matrix4x4(new float[] { 
                                            1.0f, 0.0f, 0.0f, 2.0f,
                                            0.0f, 1.0f, 0.0f, 4.0f,
                                            0.0f, 0.0f, 1.0f, 6.0f,
                                            0.0f, 0.0f, 0.0f, 1.0f});

            var b = new Vector4(3,4,5,1);

            var expectedResult = new Vector4(5, 8, 11, 1);

            var r = a.Transform(b);
            
            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4TranslateTest()
        {
            var a = Matrix4x4.Translate(2, 4, 6);

            var b = new Vector4(3, 4, 5, 1);

            var expectedResult = new Vector4(5, 8, 11, 1);

            var r = a * b;

            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4ScaleTest()
        {
            var a = Matrix4x4.Scale(2, 4, 6);

            var b = new Vector4(3, 4, 5, 1);

            var expectedResult = new Vector4(6, 16, 30, 1);

            var r = a.Transform(b);

            Assert.Equal<Vector4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4x4RotateXTest()
        {
            var a = Matrix4x4.RotateX((float)Math.PI);

            var b = new Vector4(3, 4, 5, 1);

            var expectedResult = new Vector4(3, -4, -5, 1);

            var r = a.Transform(b);

            Assert.True(r.Equals(expectedResult, 1e-3f)); 
        }

        [Fact]
        public void Matrix4x4RotateYTest()
        {
            var a = Matrix4x4.RotateY((float)Math.PI);

            var b = new Vector4(3, 4, 5, 1);

            var expectedResult = new Vector4(-3, 4, -5, 1);

            var r = a.Transform(b);

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void Matrix4x4RotateZTest()
        {
            var a = Matrix4x4.RotateZ((float)Math.PI);

            var b = new Vector4(3, 4, 5, 1);

            var expectedResult = new Vector4(-3, -4, 5, 1);

            var r = a.Transform(b);

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void Matrix4x4RotateAxisTest()
        {
            var rotXAxis = Matrix4x4.RotateAxis(Vector3.UnitX, (float)Math.PI);
            var rotYAxis = Matrix4x4.RotateAxis(Vector3.UnitY, (float)Math.PI);
            var rotZAxis = Matrix4x4.RotateAxis(Vector3.UnitZ, (float)Math.PI);

            var vec = new Vector4(3, 4, 5, 1);

            var expectedResultX = new Vector4(3, -4, -5, 1);
            var expectedResultY = new Vector4(-3, 4, -5, 1);
            var expectedResultZ = new Vector4(-3, -4, 5, 1);

            var rX = rotXAxis.Transform(vec);
            var rY = rotYAxis.Transform(vec);
            var rZ = rotZAxis.Transform(vec);

            Assert.True(rX.Equals(expectedResultX, 1e-3f));
            Assert.True(rY.Equals(expectedResultY, 1e-3f));
            Assert.True(rZ.Equals(expectedResultZ, 1e-3f));
        }
        
        [Fact]
        public void Matrix4x4OrtographicProjectionRHTest()
        {
            var projMatrix = Matrix4x4.OrthographicProjectionRH(0, 10, 0, 10, -1, 1);

            // This would be at the full extent of our clipping plane (top right)
            var vec = new Vector4(10, 10, 1, 1);
            var expectedResult = new Vector4(1, 1, -1, 1);

            var r = projMatrix.Transform(vec);

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }

        [Fact]
        public void Matrix4x4PerspectiveProjectionRHTest()
        {
            var projMatrix = Matrix4x4.PerspectiveProjectionRH(0, 10, 0, 10, 0.1f, 100.0f);

            // Arbitrarily chosen
            var vec = new Vector4(3, 4, 5, 1);
            var expectedResult = new Vector4(5.06f, 5.08f, -5.21021f, -5.0f);

            var r = projMatrix.Transform(vec);

            Assert.True(r.Equals(expectedResult, 1e-3f));
        }
    }
}
