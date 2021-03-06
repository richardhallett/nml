﻿
namespace Nml.Benchmarks
{
    #pragma warning disable 219
    class Matrix4x4Benchmarks
    {
        static Matrix4x4 matrix = new Matrix4x4(new float[] { 
                                            1.0f, 2.0f, 3.0f, 4.0f,
                                            5.0f, 6.0f, 7.0f, 8.0f,
                                            9.0f, 10.0f, 11.0f, 12.0f,
                                            13.0f, 14.0f, 15.0f, 16.0f});

        static Vector4 vector = new Vector4(3, 4, 5, 1);

        [Benchmark(Name = "Matrix4x4 * Matrix4x4")]
        public static void MultiplyMatrix()
        {
            var r = matrix * matrix;
        }

        [Benchmark(Name = "Matrix4x4 * Matrix4x4 By Ref")]
        public static void MultiplyMatrixByRef()
        {
            Matrix4x4 result;
            Matrix4x4.Multiply(ref matrix, ref matrix, out result);
        }

        [Benchmark(Name = "Matrix4x4 * scalar")]
        public void MultiplyScalar()
        {
            var r = matrix * 2.0f;
        }

        [Benchmark(Name = "Matrix4x4 * scalar By Ref")]
        public void MultiplyScalarByRef()
        {
            Matrix4x4 result;
            Matrix4x4.Multiply(ref matrix, 2.0f, out result);
        }

        [Benchmark(Name = "Matrix4x4 + Matrix4x4")]
        public void Addition()
        {
            var r = matrix + matrix;
        }

        [Benchmark(Name = "Matrix4x4 + Matrix4x4 By Ref")]
        public static void AdditionByRef()
        {
            Matrix4x4 result;
            Matrix4x4.Add(ref matrix, ref matrix, out result);
        }

        [Benchmark(Name = "Matrix4x4 - Matrix4x4")]
        public void Subtraction()
        {
            var r = matrix - matrix;
        }

        [Benchmark(Name = "Matrix4x4 - Matrix4x4 By Ref")]
        public static void SubtractionByRef()
        {
            Matrix4x4 result;
            Matrix4x4.Subtract(ref matrix, ref matrix, out result);
        }

        [Benchmark(Name = "Matrix4x4 Transpose")]
        public void Transpose()
        {
            matrix.Transpose();
        }

        [Benchmark(Name = "Matrix4x4 Determinant")]
        public void Determinant()
        {
            var r = matrix.Determinant;
        }

        [Benchmark(Name = "Matrix4x4 Invert")]
        public void Invert()
        {
            var r = Matrix4x4.Invert(matrix);
        }

        [Benchmark(Name = "Matrix4x4 * Vector4f")]
        public void Matrix4TransformVectorBenchmark()
        {
            var r = matrix * vector;
        }

        [Benchmark(Name = "Matrix4x4 * Vector4f By Ref")]
        public void Matrix4TransformVectorBenchmarkByRef()
        {
            Vector4 result;
            Matrix4x4.Transform(ref matrix, ref vector, out result);
        }

        [Benchmark(Name = "Matrix4x4 RotateX")]
        public void Matrix4RotateX()
        {
            var r = Matrix4x4.RotateX(1.0f);
        }

        [Benchmark(Name = "Matrix4x4 RotateY")]
        public void Matrix4RotateY()
        {
            var r = Matrix4x4.RotateY(1.0f);
        }

        [Benchmark(Name = "Matrix4x4 RotateZ")]
        public void Matrix4RotateZ()
        {
            var r = Matrix4x4.RotateZ(1.0f);
        }

        [Benchmark(Name = "Matrix4x4 Scale")]
        public void Matrix4Scale()
        {
            var r = Matrix4x4.Scale(2.0f);
        }

        [Benchmark(Name = "Matrix4x4 Translate")]
        public void Matrix4Translate()
        {
            var r = Matrix4x4.Translate(1.0f, 2.0f, 3.0f);
        }
    }
}
