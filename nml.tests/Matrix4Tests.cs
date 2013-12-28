using System;
using Xunit;

namespace nml.tests
{
    public class Matrix4Tests
    {
        [Fact]
        public void Matrix4ArrayIndexTest()
        {
            var a = new Matrix4();

            a[0, 1] = 1.0f;
            a[3, 2] = 5.0f;

            float x = a[0, 1];
            float y = a[3, 2];

            Assert.Equal<float>(1.0f, x);
            Assert.Equal<float>(5.0f, y);
        }

        [Fact]
        public void Matrix4MultiplyScalarTest()
        {
            var a = new Matrix4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});            
            float scalar = 2.0f;

            var expectedResult = new Matrix4(new float[] {  
                                            2.0f, 4.0f, 6.0f, 8.0f,
                                            10.0f, 12.0f, 14.0f, 16.0f,
                                            18.0f, 20.0f, 22.0f, 24.0f,
                                            26.0f, 28.0f, 30.0f, 32.0f});

            var r = a * scalar;

            Assert.Equal<Matrix4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4DivideScalarTest()
        {
            var a = new Matrix4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});
            float scalar = 2.0f;

            var expectedResult = new Matrix4(new float[] {  
                                            0.5f, 1.0f, 1.5f, 2.0f,
                                            2.5f, 3.0f, 3.5f, 4.0f,
                                            4.5f, 5.0f, 5.5f, 6.0f,
                                            6.5f, 7.0f, 7.5f, 8.0f});

            var r = a / scalar;

            Assert.Equal<Matrix4>(expectedResult, r);
        }


        [Fact]
        public void Matrix4AdditionTest()
        {
            var a = new Matrix4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

            var expectedResult = new Matrix4(new float[] {  
                                            2.0f, 4.0f, 6.0f, 8.0f,
                                            10.0f, 12.0f, 14.0f, 16.0f,
                                            18.0f, 20.0f, 22.0f, 24.0f,
                                            26.0f, 28.0f, 30.0f, 32.0f});

            var r = a + a;

            Assert.Equal<Matrix4>(expectedResult, r);
        }

        [Fact]
        public void Matrix4SubtractionTest()
        {
            var a = new Matrix4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

            var expectedResult = new Matrix4(0.0f);

            var r = a - a;

            Assert.Equal<Matrix4>(expectedResult, r);
        }


        [Fact]
        public void Matrix4TransposeTest()
        {
            var a = new Matrix4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

            var expectedResult = new Matrix4(new float[] { 
                                            1.0f, 5.0f, 9.0f, 13.0f,
                                            2.0f, 6.0f, 10.0f, 14.0f,
                                            3.0f, 7.0f, 11.0f, 15.0f,
                                            4.0f, 8.0f, 12.0f, 16.0f});

            a.Transpose();

            Assert.Equal<Matrix4>(expectedResult, a);
        }

        [Fact]
        public void Matrix4MultiplyMatrixTest()
        {
            var a = new Matrix4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});
            

            var expectedResult = new Matrix4(new float[] {  
                                            90.0f, 100.0f, 110.0f, 120.0f,
                                            202.0f, 228.0f, 254.0f, 280.0f,
                                            314.0f, 356.0f, 398.0f, 440.0f,
                                            426.0f, 484.0f, 542.0f, 600.0f});

            var r = a * a;

            Assert.Equal<Matrix4>(expectedResult, r);
        }
    
    }
}
