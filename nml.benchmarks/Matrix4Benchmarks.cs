using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nml.benchmarks
{
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

        [Benchmark(Name = "Matrix4x4 * scalar")]
        public void MultiplyScalar()
        {
            var r = matrix * 2.0f;
        }

        [Benchmark(Name = "Matrix4x4 + Matrix4x4")]
        public void Addition()
        {
            var r = matrix + matrix;
        }

        [Benchmark(Name = "Matrix4x4 - Matrix4x4")]
        public void Subtraction()
        {
            var r = matrix - matrix;
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
    }
}
