﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nml
{
    /// <summary>
    /// This is a 4x4 matrix, used for working with affine transformations ( rotation, scaling, translation) as well as perspective projections in 3d space.
    /// The data is stored in row-major order, e.g. m12 refers to first row second column.
    /// The mathematical operations on vector transforms are suitable for the order of M*v.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        /// <summary>
        /// First row, first column.
        /// </summary>
        public float M11;
        /// <summary>
        /// First row, second column.
        /// </summary>
        public float M12;
        /// <summary>
        /// First row, third column.
        /// </summary>
        public float M13;
        /// <summary>
        /// First row, fourth column.
        /// </summary>
        public float M14;
        /// <summary>
        /// Second row, first column.
        /// </summary>
        public float M21;
        /// <summary>
        /// Second row, second column.
        /// </summary>
        public float M22;
        /// <summary>
        /// Second row, third column.
        /// </summary>
        public float M23;
        /// <summary>
        /// Second row, fourth column.
        /// </summary>
        public float M24;
        /// <summary>
        /// Third row, first column.
        /// </summary>
        public float M31;
        /// <summary>
        /// Third row, second column.
        /// </summary>
        public float M32;
        /// <summary>
        /// Third row, third column.
        /// </summary>
        public float M33;
        /// <summary>
        /// Third row, fourth column.
        /// </summary>
        public float M34;
        /// <summary>
        /// Fourth row, first column.
        /// </summary>
        public float M41;
        /// <summary>
        /// Fourth row, second column.
        /// </summary>
        public float M42;
        /// <summary>
        /// Fourth row, third column.
        /// </summary>
        public float M43;
        /// <summary>
        /// Fourth row, fourth column.
        /// </summary>
        public float M44;

        /// <summary>
        /// A 4x4 identity matrix.
        /// </summary>
        public static readonly Matrix4x4 Identity = new Matrix4x4() { M11 = 1.0f, M22 = 1.0f, M33 = 1.0f, M44 = 1.0f };

        /// <summary>
        /// Creates a new instance of <see cref="Matrix4x4"/> with scalar.
        /// </summary>
        /// <param name="value">A scalar value that will be assigned to all components.</param>
        public Matrix4x4(float value)
        {
            M11 = M12 = M13 = M14 =
            M21 = M22 = M23 = M24 =
            M31 = M32 = M33 = M34 =
            M41 = M42 = M43 = M44 = value;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Matrix4x4"/> with values specified by list collection.
        /// </summary>
        /// <param name="values">The floats to initialise the values with</param>
        public Matrix4x4(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length != 16) {
                throw new ArgumentOutOfRangeException("values", "The size of the values collection must contain 16 elements.");
            }
            
            M11 = values[0];
            M12 = values[1];
            M13 = values[2];
            M14 = values[3];

            M21 = values[4];
            M22 = values[5];
            M23 = values[6];
            M24 = values[7];

            M31 = values[8];
            M32 = values[9];
            M33 = values[10];
            M34 = values[11];

            M41 = values[12];
            M42 = values[13];
            M43 = values[14];
            M44 = values[15];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4x4"/> struct.
        /// </summary>
        /// <param name="m11">First row, first column</param>
        /// <param name="m12">First row, second column</param>
        /// <param name="m13">First row, third column</param>
        /// <param name="m14">First row, fourth column</param>
        /// <param name="m21">Second row, first column</param>
        /// <param name="m22">Second row, second column</param>
        /// <param name="m23">Second row, third column</param>
        /// <param name="m24">Second row, fourth column</param>
        /// <param name="m31">Third row, first column</param>
        /// <param name="m32">Third row, second column</param>
        /// <param name="m33">Third row, third column</param>
        /// <param name="m34">Third row, fourth column</param>
        /// <param name="m41">Fourth row, first column</param>
        /// <param name="m42">Fourth row, second column</param>
        /// <param name="m43">Fourth row, third column</param>
        /// <param name="m44">Fourth row, fourth column</param>
        public Matrix4x4(float m11, float m12, float m13, float m14,
                         float m21, float m22, float m23, float m24,
                         float m31, float m32, float m33, float m34,
                         float m41, float m42, float m43, float m44)
        {           
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }        

        /// <summary>
        /// Provides array style indexing to matrix elements.
        /// </summary>
        /// <param name="index">The zero-based index of the element to return.</param>
        /// <returns>Element value at specified index.</returns>
        public float this[int index]
        {
            get
            {
                return GetElement(index);
            }

            set
            {
                SetElement(index, value);
            }
        }

        /// <summary>
        /// Provides row,column indexing to matrix elements.
        /// Uses zero-based indexing.
        /// </summary>        
        /// <param name="row">Row of element.</param>
        /// <param name="column">Column of element.</param>
        /// <returns>Element value at specified [row,column].</returns>
        public float this[int row, int column]
        {
            get
            {
                return GetElement(row, column);
            }
            set
            {                
                SetElement(row, column, value);
            }
        }

        /// <summary>
        /// Gets or sets the translate vector part of the matrix.
        /// </summary>
        /// <value>
        /// The translate part.
        /// </value>
        public Vector3 TranslatePart
        {
            get
            {
                return new Vector3(this.M14, this.M24, this.M23);
            }
            set
            {
                this.M14 = value.x;
                this.M24 = value.y;
                this.M34 = value.z;
            }
        }

      
        /// <summary>
        /// Set all elements.
        /// </summary>
        /// <param name="m11">Row 1 Column 1</param>
        /// <param name="m12">Row 1 Column 2</param>
        /// <param name="m13">Row 1 Column 3</param>
        /// <param name="m14">Row 1 Column 4</param>
        /// <param name="m21">Row 2 Column 1</param>
        /// <param name="m22">Row 2 Column 2</param>
        /// <param name="m23">Row 2 Column 3</param>
        /// <param name="m24">Row 2 Column 4</param>
        /// <param name="m31">Row 3 Column 1</param>
        /// <param name="m32">Row 3 Column 3</param>
        /// <param name="m33">Row 3 Column 4</param>
        /// <param name="m34">Row 3 Column 4</param>
        /// <param name="m41">Row 4 Column 1</param>
        /// <param name="m42">Row 4 Column 2</param>
        /// <param name="m43">Row 4 Column 3</param>
        /// <param name="m44">Row 4 Column 4</param>
        public void Set(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;
            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        /// <summary>
        /// Calculate the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        public float Determinant
        {
            get
            {
                // Local variables for faster lookup.
                float l11 = this.M11;
                float l12 = this.M12;
                float l13 = this.M13;
                float l14 = this.M14;
                float l21 = this.M21;
                float l22 = this.M22;
                float l23 = this.M23;
                float l24 = this.M24;
                float l31 = this.M31;
                float l32 = this.M32;
                float l33 = this.M33;
                float l34 = this.M34;
                float l41 = this.M41;
                float l42 = this.M42;
                float l43 = this.M43;
                float l44 = this.M44;

                float c1 = (l33 * l44) - (l34 * l43);
                float c2 = (l32 * l44) - (l34 * l42);
                float c3 = (l32 * l43) - (l33 * l42);
                float c4 = (l31 * l44) - (l34 * l41);
                float c5 = (l31 * l43) - (l33 * l41);
                float c6 = (l31 * l42) - (l32 * l41);
                return ((((l11 * (((l22 * c1) - (l23 * c2)) + (l24 * c3))) - (l12 * (((l21 * c1) -
                (l23 * c4)) + (l24 * c5)))) + (l13 * (((l21 * c2) - (l22 * c4)) + (l24 * c6)))) -
                (l14 * (((l21 * c3) - (l22 * c5)) + (l23 * c6))));
            }
        }

        /// <summary>
        /// Multiply matrix components by scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The value you want to scale the matrix by.</param>
        /// <returns>The resulting multiplication of the matrix</returns>
        public static Matrix4x4 Multiply(Matrix4x4 matrix, float scalar)
        {            
            Matrix4x4 result = new Matrix4x4();
            Matrix4x4.Multiply(ref matrix, scalar, out result);
            return result;
        }

        /// <summary>
        /// Multiply matrix components by scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The value you want to scale the matrix by.</param>
        /// <param name="result">The resulting multiplication of the matrix</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Multiply(ref Matrix4x4 matrix, float scalar, out Matrix4x4 result)
        {
            // Local variables for faster lookup.
            float a11 = matrix.M11;
            float a12 = matrix.M12;
            float a13 = matrix.M13;
            float a14 = matrix.M14;
            float a21 = matrix.M21;
            float a22 = matrix.M22;
            float a23 = matrix.M23;
            float a24 = matrix.M24;
            float a31 = matrix.M31;
            float a32 = matrix.M32;
            float a33 = matrix.M33;
            float a34 = matrix.M34;
            float a41 = matrix.M41;
            float a42 = matrix.M42;
            float a43 = matrix.M43;
            float a44 = matrix.M44;

            Matrix4x4 r = new Matrix4x4();
            r.M11 = a11 * scalar;
            r.M12 = a12 * scalar;
            r.M13 = a13 * scalar;
            r.M14 = a14 * scalar;

            r.M21 = a21 * scalar;
            r.M22 = a22 * scalar;
            r.M23 = a23 * scalar;
            r.M24 = a24 * scalar;

            r.M31 = a31 * scalar;
            r.M32 = a32 * scalar;
            r.M33 = a33 * scalar;
            r.M34 = a34 * scalar;

            r.M41 = a41 * scalar;
            r.M42 = a42 * scalar;
            r.M43 = a43 * scalar;
            r.M44 = a44 * scalar;

            result = r;
        }

        /// <summary>
        /// Multiply two matrices and return product.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>The product of the two matrices.</returns>
        /// 
        public static Matrix4x4 Multiply(Matrix4x4 a, Matrix4x4 b)
        {            
            Matrix4x4 result = new Matrix4x4();
            Matrix4x4.Multiply(ref a, ref b, out result);
            return result;          
        }

        /// <summary>
        /// Multiply two matrices and return product.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <param name="result">The product of the two matrices.</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Multiply(ref Matrix4x4 a, ref Matrix4x4 b, out Matrix4x4 result)
        {
            // Local variables for faster lookup.
            float a11 = a.M11;
            float a12 = a.M12;
            float a13 = a.M13;
            float a14 = a.M14;
            float a21 = a.M21;
            float a22 = a.M22;
            float a23 = a.M23;
            float a24 = a.M24;
            float a31 = a.M31;
            float a32 = a.M32;
            float a33 = a.M33;
            float a34 = a.M34;
            float a41 = a.M41;
            float a42 = a.M42;
            float a43 = a.M43;
            float a44 = a.M44;

            float b11 = b.M11;
            float b12 = b.M12;
            float b13 = b.M13;
            float b14 = b.M14;
            float b21 = b.M21;
            float b22 = b.M22;
            float b23 = b.M23;
            float b24 = b.M24;
            float b31 = b.M31;
            float b32 = b.M32;
            float b33 = b.M33;
            float b34 = b.M34;
            float b41 = b.M41;
            float b42 = b.M42;
            float b43 = b.M43;
            float b44 = b.M44;

            Matrix4x4 r = new Matrix4x4();

            r.M11 = (a11 * b11) + (a12 * b21) + (a13 * b31) + (a14 * b41);
            r.M12 = (a11 * b12) + (a12 * b22) + (a13 * b32) + (a14 * b42);
            r.M13 = (a11 * b13) + (a12 * b23) + (a13 * b33) + (a14 * b43);
            r.M14 = (a11 * b14) + (a12 * b24) + (a13 * b34) + (a14 * b44);

            r.M21 = (a21 * b11) + (a22 * b21) + (a23 * b31) + (a24 * b41);
            r.M22 = (a21 * b12) + (a22 * b22) + (a23 * b32) + (a24 * b42);
            r.M23 = (a21 * b13) + (a22 * b23) + (a23 * b33) + (a24 * b43);
            r.M24 = (a21 * b14) + (a22 * b24) + (a23 * b34) + (a24 * b44);

            r.M31 = (a31 * b11) + (a32 * b21) + (a33 * b31) + (a34 * b41);
            r.M32 = (a31 * b12) + (a32 * b22) + (a33 * b32) + (a34 * b42);
            r.M33 = (a31 * b13) + (a32 * b23) + (a33 * b33) + (a34 * b43);
            r.M34 = (a31 * b14) + (a32 * b24) + (a33 * b34) + (a34 * b44);

            r.M41 = (a41 * b11) + (a42 * b21) + (a43 * b31) + (a44 * b41);
            r.M42 = (a41 * b12) + (a42 * b22) + (a43 * b32) + (a44 * b42);
            r.M43 = (a41 * b13) + (a42 * b23) + (a43 * b33) + (a44 * b43);
            r.M44 = (a41 * b14) + (a42 * b24) + (a43 * b34) + (a44 * b44);

            result = r;
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting addition of the two matrices.</returns>
        public static Matrix4x4 Add(Matrix4x4 a, Matrix4x4 b)
        {         
            Matrix4x4 result = new Matrix4x4();
            Matrix4x4.Add(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="result">The resulting addition of the two matrices.</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Add(ref Matrix4x4 a, ref Matrix4x4 b, out Matrix4x4 result)
        {
            // Local variables for faster lookup.
            float a11 = a.M11;
            float a12 = a.M12;
            float a13 = a.M13;
            float a14 = a.M14;
            float a21 = a.M21;
            float a22 = a.M22;
            float a23 = a.M23;
            float a24 = a.M24;
            float a31 = a.M31;
            float a32 = a.M32;
            float a33 = a.M33;
            float a34 = a.M34;
            float a41 = a.M41;
            float a42 = a.M42;
            float a43 = a.M43;
            float a44 = a.M44;

            float b11 = b.M11;
            float b12 = b.M12;
            float b13 = b.M13;
            float b14 = b.M14;
            float b21 = b.M21;
            float b22 = b.M22;
            float b23 = b.M23;
            float b24 = b.M24;
            float b31 = b.M31;
            float b32 = b.M32;
            float b33 = b.M33;
            float b34 = b.M34;
            float b41 = b.M41;
            float b42 = b.M42;
            float b43 = b.M43;
            float b44 = b.M44;

            Matrix4x4 r = new Matrix4x4();

            r.M11 = a11 + b11;
            r.M12 = a12 + b12;
            r.M13 = a13 + b13;
            r.M14 = a14 + b14;

            r.M21 = a21 + b21;
            r.M22 = a22 + b22;
            r.M23 = a23 + b23;
            r.M24 = a24 + b24;

            r.M31 = a31 + b31;
            r.M32 = a32 + b32;
            r.M33 = a33 + b33;
            r.M34 = a34 + b34;

            r.M41 = a41 + b41;
            r.M42 = a42 + b42;
            r.M43 = a43 + b43;
            r.M44 = a44 + b44;

            result = r;
        }

        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting subtraction of the two matrices.</returns>
        public static Matrix4x4 Subtract(Matrix4x4 a, Matrix4x4 b)
        {           
            Matrix4x4 result = new Matrix4x4();
            Matrix4x4.Subtract(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="result">The resulting subtraction of the two matrices.</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Subtract(ref Matrix4x4 a, ref Matrix4x4 b, out Matrix4x4 result)
        {
            // Local variables for faster lookup.
            float a11 = a.M11;
            float a12 = a.M12;
            float a13 = a.M13;
            float a14 = a.M14;
            float a21 = a.M21;
            float a22 = a.M22;
            float a23 = a.M23;
            float a24 = a.M24;
            float a31 = a.M31;
            float a32 = a.M32;
            float a33 = a.M33;
            float a34 = a.M34;
            float a41 = a.M41;
            float a42 = a.M42;
            float a43 = a.M43;
            float a44 = a.M44;

            float b11 = b.M11;
            float b12 = b.M12;
            float b13 = b.M13;
            float b14 = b.M14;
            float b21 = b.M21;
            float b22 = b.M22;
            float b23 = b.M23;
            float b24 = b.M24;
            float b31 = b.M31;
            float b32 = b.M32;
            float b33 = b.M33;
            float b34 = b.M34;
            float b41 = b.M41;
            float b42 = b.M42;
            float b43 = b.M43;
            float b44 = b.M44;

            Matrix4x4 r = new Matrix4x4();

            r.M11 = a11 - b11;
            r.M12 = a12 - b12;
            r.M13 = a13 - b13;
            r.M14 = a14 - b14;

            r.M21 = a21 - b21;
            r.M22 = a22 - b22;
            r.M23 = a23 - b23;
            r.M24 = a24 - b24;

            r.M31 = a31 - b31;
            r.M32 = a32 - b32;
            r.M33 = a33 - b33;
            r.M34 = a34 - b34;

            r.M41 = a41 - b41;
            r.M42 = a42 - b42;
            r.M43 = a43 - b43;
            r.M44 = a44 - b44;

            result = r;
        }

        /// <summary>
        /// Swap all elements, rows become columns, columns become rows of the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to tranpose.</param>
        /// <returns>The transposed matrix</returns>
        public static Matrix4x4 Transpose(Matrix4x4 matrix)
        {
            Matrix4x4 result = new Matrix4x4();
            Matrix4x4.Transpose(ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Swap all elements, rows become columns, columns become rows of the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to tranpose.</param>
        /// <param name="result">The transposed matrix</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Transpose(ref Matrix4x4 matrix, out Matrix4x4 result)
        {
            Matrix4x4 tMatrix = new Matrix4x4();
            tMatrix[0, 0] = matrix[0, 0];
            tMatrix[0, 1] = matrix[1, 0];
            tMatrix[0, 2] = matrix[2, 0];
            tMatrix[0, 3] = matrix[3, 0];

            tMatrix[1, 0] = matrix[0, 1];
            tMatrix[1, 1] = matrix[1, 1];
            tMatrix[1, 2] = matrix[2, 1];
            tMatrix[1, 3] = matrix[3, 1];

            tMatrix[2, 0] = matrix[0, 2];
            tMatrix[2, 1] = matrix[1, 2];
            tMatrix[2, 2] = matrix[2, 2];
            tMatrix[2, 3] = matrix[3, 2];

            tMatrix[3, 0] = matrix[0, 3];
            tMatrix[3, 1] = matrix[1, 3];
            tMatrix[3, 2] = matrix[2, 3];
            tMatrix[3, 3] = matrix[3, 3];
            
            result = tMatrix;
        }

        /// <summary>
        /// Inverts the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to invert.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix4x4 Invert(Matrix4x4 matrix)
        {
            // Local variables for faster lookup.
            float l1 = matrix.M11;
            float l2 = matrix.M12;
            float l3 = matrix.M13;
            float l4 = matrix.M14;
            float l5 = matrix.M21;
            float l6 = matrix.M22;
            float l7 = matrix.M23;
            float l8 = matrix.M24;
            float l9 = matrix.M31;
            float l10 = matrix.M32;
            float l11 = matrix.M33;
            float l12 = matrix.M34;
            float l13 = matrix.M41;
            float l14 = matrix.M42;
            float l15 = matrix.M43;
            float l16 = matrix.M44;

            float l17 = (l11 * l16) - (l12 * l15);
            float l18 = (l10 * l16) - (l12 * l14);
            float l19 = (l10 * l15) - (l11 * l14);
            float l20 = (l9 * l16) - (l12 * l13);
            float l21 = (l9 * l15) - (l11 * l13);
            float l22 = (l9 * l14) - (l10 * l13);
            float l23 = ((l6 * l17) - (l7 * l18)) + (l8 * l19);
            float l24 = -(((l5 * l17) - (l7 * l20)) + (l8 * l21));
            float l25 = ((l5 * l18) - (l6 * l20)) + (l8 * l22);
            float l26 = -(((l5 * l19) - (l6 * l21)) + (l7 * l22));
            float l27 = 1f / ((((l1 * l23) + (l2 * l24)) + (l3 * l25)) + (l4 * l26));
            float l28 = (l7 * l16) - (l8 * l15);
            float l29 = (l6 * l16) - (l8 * l14);
            float l30 = (l6 * l15) - (l7 * l14);
            float l31 = (l5 * l16) - (l8 * l13);
            float l32 = (l5 * l15) - (l7 * l13);
            float l33 = (l5 * l14) - (l6 * l13);
            float l34 = (l7 * l12) - (l8 * l11);
            float l35 = (l6 * l12) - (l8 * l10);
            float l36 = (l6 * l11) - (l7 * l10);
            float l37 = (l5 * l12) - (l8 * l9);
            float l38 = (l5 * l11) - (l7 * l9);
            float l39 = (l5 * l10) - (l6 * l9);

            var invertedMatrix = new Matrix4x4();

            invertedMatrix.M11 = l23 * l27;
            invertedMatrix.M21 = l24 * l27;
            invertedMatrix.M31 = l25 * l27;
            invertedMatrix.M41 = l26 * l27;
            invertedMatrix.M12 = -(((l2 * l17) - (l3 * l18)) + (l4 * l19)) * l27;
            invertedMatrix.M22 = (((l1 * l17) - (l3 * l20)) + (l4 * l21)) * l27;
            invertedMatrix.M32 = -(((l1 * l18) - (l2 * l20)) + (l4 * l22)) * l27;
            invertedMatrix.M42 = (((l1 * l19) - (l2 * l21)) + (l3 * l22)) * l27;
            invertedMatrix.M13 = (((l2 * l28) - (l3 * l29)) + (l4 * l30)) * l27;
            invertedMatrix.M23 = -(((l1 * l28) - (l3 * l31)) + (l4 * l32)) * l27;
            invertedMatrix.M33 = (((l1 * l29) - (l2 * l31)) + (l4 * l33)) * l27;
            invertedMatrix.M43 = -(((l1 * l30) - (l2 * l32)) + (l3 * l33)) * l27;
            invertedMatrix.M14 = -(((l2 * l34) - (l3 * l35)) + (l4 * l36)) * l27;
            invertedMatrix.M24 = (((l1 * l34) - (l3 * l37)) + (l4 * l38)) * l27;
            invertedMatrix.M34 = -(((l1 * l35) - (l2 * l37)) + (l4 * l39)) * l27;
            invertedMatrix.M44 = (((l1 * l36) - (l2 * l38)) + (l3 * l39)) * l27;

            return invertedMatrix;
        }

        /// <summary>
        /// Create a new translation matrix with the specified axis offset.
        /// </summary>
        /// <param name="x">X translation.</param>
        /// <param name="y">Y translation.</param>
        /// <param name="z">Z translation.</param>
        /// <returns>The resulting translation matrix.</returns>
        public static Matrix4x4 Translate(float x, float y, float z)
        {
            return new Matrix4x4(1.0f, 0.0f, 0.0f, x,
                                 0.0f, 1.0f, 0.0f, y,
                                 0.0f, 0.0f, 1.0f, z,
                                 0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>
        /// Create a new translation matrix using a <see cref="Vector3"/>
        /// </summary>
        /// <param name="vec"><see cref="Vector3"/> to use for translation.</param>
        /// <returns>The resulting translation matrix.</returns>
        public static Matrix4x4 Translate(Vector3 vec)
        {
            return Matrix4x4.Translate(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Create a scaling matrix with the specified axis values.
        /// </summary>
        /// <param name="x">X scale.</param>
        /// <param name="y">Y scale.</param>
        /// <param name="z">Z scale.</param>
        /// <returns></returns>
        public static Matrix4x4 Scale(float x, float y, float z)
        {
            return new Matrix4x4(x, 0.0f, 0.0f, 0.0f,
                                 0.0f, y, 0.0f, 0.0f,
                                 0.0f, 0.0f, z, 0.0f,
                                 0.0f, 0.0f, 0.0f, 1.0f);

        }

        /// <summary>
        /// Create a scaling matrix with the specified axis values.
        /// </summary>
        /// <param name="vec"><see cref="Vector3"/> to use for scaling.</param>
        /// <returns></returns>
        public static Matrix4x4 Scale(Vector3 vec)
        {
            return Matrix4x4.Scale(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Create a scaling matrix with the specified value for uniformly scaling each axis.
        /// </summary>
        /// <param name="scale">The value to uniformly scale by.</param>
        /// <returns></returns>
        public static Matrix4x4 Scale(float scale)
        {
            return Matrix4x4.Scale(scale, scale, scale);
        }

        /// <summary>
        /// Create a rotation matrix around the X axis by the given angle.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4x4 RotateX(float angle)
        {
            float cos;
            float sin;
            Common.SinCos(angle, out sin, out cos);

            return new Matrix4x4(1.0f, 0.0f, 0.0f, 0.0f,
                                 0.0f, cos, -sin, 0.0f,
                                 0.0f, sin, cos, 0.0f,
                                 0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>
        /// Create a rotation matrix around the Y axis by the given angle.
        /// 
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4x4 RotateY(float angle)
        {
            float cos;
            float sin;
            Common.SinCos(angle, out sin, out cos);

            return new Matrix4x4(cos, 0.0f, sin, 0.0f,
                                 0.0f, 1.0f, 0.0f, 0.0f,
                                 -sin, 0.0f, cos, 0.0f,
                                 0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>
        /// Create a rotation matrix around the Z axis by the given angle.
        /// 
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4x4 RotateZ(float angle)
        {
            float cos;
            float sin;
            Common.SinCos(angle, out sin, out cos);           

            return new Matrix4x4(cos, -sin, 0.0f, 0.0f,
                                 sin, cos, 0.0f, 0.0f,
                                 0.0f, 0.0f, 1.0f, 0.0f,
                                 0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>
        /// Create a rotation matrix around an axis by a given angle.
        /// </summary>
        /// <remarks>If you do not use a normalised vector for the axis, you will get undefined behaviour, in the interest of speed this function does not do any safety checks.</remarks>
        /// <param name="axis">A normalised unit vector representing the axis to rotate about.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4x4 RotateAxis(Vector3 axis, float angle)
        {
            float x = axis.x;
            float y = axis.y;
            float z = axis.z;

            float cos;
            float sin;
            Common.SinCos(angle, out sin, out cos);

            float cos1 = 1.0f - cos;

            float xx1 = x * x * cos1;
            float xy1 = x * y * cos1;
            float xz1 = x * z * cos1;
            float yy1 = y * y * cos1;
            float yz1 = y * z * cos1;
            float zz1 = z * z * cos1;
            
            float sinx = x * sin;
            float siny = y * sin;
            float sinz = z * sin;

            return new Matrix4x4(cos + xx1,     xy1 - sinz,     xz1 + siny, 0.0f,
                                 xy1 + sinz,    cos + yy1,      yz1 - sinx, 0.0f,
                                 xz1 - siny,    yz1 + sinx,     cos + zz1,  0.0f,
                                 0.0f,          0.0f,           0.0f,       1.0f);
        }

        /// <summary>
        /// Create a right handed orthographic projection matrix. 
        /// A clipping plane is used to form a box that is scaled to a unit cube (-1, -1, -1) to (1, 1, 1) which has its centre at the origin (0, 0, 0).
        /// </summary>
        /// <param name="left">The left coordinate of the view frustrum.</param>
        /// <param name="right">The right coordinate of the view frustrum.</param>
        /// <param name="bottom">The bottom coordinate of the view frustrum</param>
        /// <param name="top">The top coordinate of the view frustrum</param>
        /// <param name="near">The near coordinate of the depth plane. Can be negative if the plane is behind the viewer.</param>
        /// <param name="far">The far coordinate of the depth plane. Can be negative if the plane is behind the viewer.</param>
        /// <returns>A projection matrix.</returns>
        public static Matrix4x4 OrthographicProjectionRH(float left, float right, float bottom, float top, float near, float far)
        {            
            return new Matrix4x4( 2 / (right - left),    0.0f,               0.0f,               -(right + left) / (right - left),
                                 0.0f,                  2 / (top - bottom), 0.0f,               -(top + bottom) / (top - bottom),
                                 0.0f,                  0.0f,               -2 / (far - near),  -(far + near) / (far - near),
                                 0.0f,                  0.0f,               0.0f,               1.0f);
        }

        /// <summary>
        /// Create a right handed perspective projection matrix. 
        /// A clipping plane is used to form a box that is scaled to a unit cube (-1, -1, -1) to (1, 1, 1) which has its centre at the origin (0, 0, 0).
        /// </summary>
        /// <param name="left">The left coordinate of the view frustrum.</param>
        /// <param name="right">The right coordinate of the view frustrum.</param>
        /// <param name="bottom">The bottom coordinate of the view frustrum</param>
        /// <param name="top">The top coordinate of the view frustrum</param>
        /// <param name="near">Specifies the distance from the viewer to the near clipping plane (always positive).</param>
        /// <param name="far">Specifies the distance from the viewer to the far clipping plane (always positive).</param>
        /// <returns>A projection matrix.</returns>
        public static Matrix4x4 PerspectiveProjectionRH(float left, float right, float bottom, float top, float near, float far)
        {
            if (near <= 0)
                throw new ArgumentOutOfRangeException("near");
            if (far <= 0)
                throw new ArgumentOutOfRangeException("far");
            if (near >= far)
                throw new ArgumentOutOfRangeException("near");

            return new Matrix4x4((2.0f * near) / (right - left),    0.0f,                           (right + left) / (right - left),    0.0f,
                                 0.0f,                              (2.0f * near) / (top - bottom),  (top + bottom) / (top - bottom),    0.0f,
                                 0.0f,                              0.0f,                           -(far + near) / (far - near),       -(2 * far * near) / (far - near),
                                 0.0f,                              0.0f,                           -1.0f,                              0.0f);
        }

        /// <summary>
        /// Create a right handed perspective projection matrix
        /// A clipping plane is used to form a box that is scaled to a unit cube (-1, -1, -1) to (1, 1, 1) which has its centre at the origin (0, 0, 0).
        /// </summary>
        /// <param name="fovy">The field of view angle in radians.</param>
        /// <param name="aspect">The aspect ratio of the view (width / height)</param>
        /// <param name="near">Specifies the distance from the viewer to the near clipping plane (always positive).</param>
        /// <param name="far">Specifies the distance from the viewer to the far clipping plane (always positive).</param>
        /// <returns></returns>
        public static Matrix4x4 PerspectiveProjectionRH(float fovy, float aspect, float near, float far)
        {
            if (fovy <= 0 || fovy > Common.Pi)
                throw new ArgumentOutOfRangeException("fovy");
            if (aspect <= 0)
                throw new ArgumentOutOfRangeException("aspect");
            if (near <= 0)
                throw new ArgumentOutOfRangeException("near");
            if (far <= 0)
                throw new ArgumentOutOfRangeException("far");
            if (near >= far)
                throw new ArgumentOutOfRangeException("near");

            float top = near * (float)Math.Tan(fovy * 0.5f);
            float bottom = -top;
            float left = bottom * aspect;
            float right = top * aspect;

            return PerspectiveProjectionRH(left, right, bottom, top, near, far);
        }

        /// <summary>
        /// Transform a <see cref="Vector4"/> by this matrix.
        /// </summary>
        /// <param name="vec">Vector to transform.</param>
        /// <returns>The resulting <see cref="Vector4"/> transformed by this matrix i.e. M * (x, y, z, w)</returns>
        public Vector4 Transform(Vector4 vec)
        {
            Vector4 result;
            Matrix4x4.Transform(ref this, ref vec, out result);
            return result;
        }

        /// <summary>
        /// Transform a <see cref="Vector4"/> by a matrix.
        /// </summary>
        /// <param name="matrix">Matrix to transform with.</param>
        /// <param name="vec">Vector to transform.</param>
        /// <param name="result">The resulting <see cref="Vector4"/> transformed by this matrix i.e. M * (x, y, z, w)</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Transform(ref Matrix4x4 matrix, ref Vector4 vec, out Vector4 result)
        {
            // Local variables for faster lookup.
            float l11 = matrix.M11;
            float l12 = matrix.M12;
            float l13 = matrix.M13;
            float l14 = matrix.M14;
            float l21 = matrix.M21;
            float l22 = matrix.M22;
            float l23 = matrix.M23;
            float l24 = matrix.M24;
            float l31 = matrix.M31;
            float l32 = matrix.M32;
            float l33 = matrix.M33;
            float l34 = matrix.M34;
            float l41 = matrix.M41;
            float l42 = matrix.M42;
            float l43 = matrix.M43;
            float l44 = matrix.M44;

            result = new Vector4(
                Vector4.Dot(new Vector4(l11, l12, l13, l14), vec),
                Vector4.Dot(new Vector4(l21, l22, l23, l24), vec),
                Vector4.Dot(new Vector4(l31, l32, l33, l34), vec),
                Vector4.Dot(new Vector4(l41, l42, l43, l44), vec)
            );
        }

        /// <summary>
        /// Inverts this matrix.
        /// </summary>
        public void Invert()
        {
            this = Invert(this);
        }

        /// <summary>
        /// Swap all elements, rows become columns, columns become rows of this matrix.
        /// </summary>
        public void Transpose()
        {
            Matrix4x4 result;
            Matrix4x4.Transpose(ref this, out result);
            this = result;
        }

        /// <summary>
        /// Multiply matrix components by scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The value you want to scale the matrix by.</param>
        /// <returns>The resulting multiplication of the matrix</returns>
        public static Matrix4x4 operator *(Matrix4x4 matrix, float scalar)
        {
            Matrix4x4 result;
            Matrix4x4.Multiply(ref matrix, scalar, out result);
            return result;
        }     

        /// <summary>
        /// Multiply two matrices and return product.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>The product of the two matrices.</returns>
        /// 
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            Matrix4x4.Multiply(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Transform a vector by a matrix, M*v
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="vec">The vector to transform.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 operator *(Matrix4x4 matrix, Vector4 vec)
        {
            Vector4 result;
            Matrix4x4.Transform(ref matrix, ref vec, out result);
            return result;
        }     

        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting subtraction of the two matrices.</returns>
        public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            Matrix4x4.Subtract(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting addition of the two matrices.</returns>
        public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            Matrix4x4.Add(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Determines whether two <see cref="Matrix4x4"/> are equal.
        /// </summary>
        /// <param name="a">First matrix.</param>
        /// <param name="b">Second matrix.</param>
        /// <returns>
        /// <c>true</c> if the the two <see cref="Matrix4x4"/> are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Matrix4x4 a, Matrix4x4 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two <see cref="Matrix4x4"/> are not equal.
        /// </summary>
        /// <param name="a">First matrix.</param>
        /// <param name="b">Second matrix.</param>
        /// <returns>
        /// <c>true</c> if the the two <see cref="Matrix4x4"/> are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Matrix4x4 a, Matrix4x4 b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return Equals((Matrix4x4)obj);
        }

        /// <summary>
        /// Return a string representation of the Matrix.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + String.Format(": \n[{0}, {1}, {2}, {3}]\n[{4}, {5}, {6}, {7}]\n[{8}, {9}, {10}, {11}]\n[{12}, {13}, {14}, {15}]", M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }

        /// <summary>
        /// Returns the elements of a matrix in an array.
        /// </summary>
        /// <returns>An array of all sixteen matrix elements.</returns>
        public float[] ToArray()
        {
            return new float[] { M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44 };
        }

        /// <summary>
        /// Determines whether the specified <see cref="Matrix4x4"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Matrix4x4"/> to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Matrix4x4"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Matrix4x4 other)
        {
            return 
                (this.M11 == other.M11) && (this.M12 == other.M12) && (this.M13 == other.M13) && (this.M14 == other.M14) &&
                (this.M21 == other.M21) && (this.M22 == other.M22) && (this.M23 == other.M23) && (this.M24 == other.M24) &&
                (this.M31 == other.M31) && (this.M32 == other.M32) && (this.M33 == other.M33) && (this.M34 == other.M34) &&
                (this.M41 == other.M41) && (this.M42 == other.M42) && (this.M43 == other.M43) && (this.M44 == other.M44);  
        }

        /// <summary>
        /// Returns a hash code.
        /// </summary>
        /// <returns>
        /// A hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return
                M11.GetHashCode() ^ M12.GetHashCode() ^ M13.GetHashCode() ^ M14.GetHashCode() ^
                M21.GetHashCode() ^ M22.GetHashCode() ^ M23.GetHashCode() ^ M24.GetHashCode() ^
                M31.GetHashCode() ^ M32.GetHashCode() ^ M33.GetHashCode() ^ M34.GetHashCode() ^
                M41.GetHashCode() ^ M42.GetHashCode() ^ M43.GetHashCode() ^ M44.GetHashCode();
        }

        /// <summary>
        /// Get a matrix element.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private float GetElement(int index)
        {
            switch (index)
            {
                case 0: return M11;
                case 1: return M12;
                case 2: return M13;
                case 3: return M14;
                case 4: return M21;
                case 5: return M22;
                case 6: return M23;
                case 7: return M24;
                case 8: return M31;
                case 9: return M32;
                case 10: return M33;
                case 11: return M34;
                case 12: return M41;
                case 13: return M42;
                case 14: return M43;
                case 15: return M44;
            }

            throw new ArgumentOutOfRangeException("index", "Argument must be in the range 0-15");
        }

        /// <summary>
        /// Gets a matrix element by array index
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Rows must be in range 0 to 3
        /// or
        /// Columns must be in range 0 to 3
        /// </exception>
        private float GetElement(int row, int column)
        {
            if (row < 0 || row > 3)
                throw new ArgumentOutOfRangeException("row", "Rows must be in range 0 to 3");
            if (column < 0 || column > 3)
                throw new ArgumentOutOfRangeException("column", "Columns must be in range 0 to 3");
            return GetElement((row * 4) + column);
        }

        /// <summary>
        /// Sets a matrix element.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private void SetElement(int index, float value)
        {
            switch (index)
            {
                case 0: M11 = value; break;
                case 1: M12 = value; break;
                case 2: M13 = value; break;
                case 3: M14 = value; break;
                case 4: M21 = value; break;
                case 5: M22 = value; break;
                case 6: M23 = value; break;
                case 7: M24 = value; break;
                case 8: M31 = value; break;
                case 9: M32 = value; break;
                case 10: M33 = value; break;
                case 11: M34 = value; break;
                case 12: M41 = value; break;
                case 13: M42 = value; break;
                case 14: M43 = value; break;
                case 15: M44 = value; break;

                throw new ArgumentOutOfRangeException();
            }            
        }

        /// <summary>
        /// Sets a matrix element.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Rows must be in range 0 to 3
        /// or
        /// Columns must be in range 0 to 3
        /// </exception>
        private void SetElement(int row, int column, float value)
        {
            if (row < 0 || row > 3)
                throw new ArgumentOutOfRangeException("row", "Rows must be in range 0 to 3");
            if (column < 0 || column > 3)
                throw new ArgumentOutOfRangeException("column", "Columns must be in range 0 to 3");

            SetElement((row * 4) + column, value);
        }

       
    }
}
