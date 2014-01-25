using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace nml
{
    /// <summary>
    /// This is a 4x4 matrix, it is a generic case for working with transformations in 3d space.
    /// The data is stored in row-major order, as per C# arrays, e.g. m12 refers to first row second column.
    /// The mathematical operations on vector transforms use column major vectors i.e. M*v.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4 : IEquatable<Matrix4>
    {
        /// <summary>
        /// A 4x4 identity matrix.
        /// </summary>
        public static readonly Matrix4 Identity = new Matrix4() { m11 = 1.0f, m22 = 1.0f, m33 = 1.0f, m44 = 1.0f };

        /// <summary>
        /// Creates a new instance of <see cref="Matrix4"/> with scalar.
        /// </summary>
        /// <param name="value">A scalar value that will be assigned to all components.</param>
        public Matrix4(float value)
        {
            m11 = m12 = m13 = m14 =
            m21 = m22 = m23 = m24 =
            m31 = m32 = m33 = m34 =
            m41 = m42 = m43 = m44 = value;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Matrix4"/> with values specified by list collection.
        /// </summary>
        /// <param name="values">Collection of floats</param>
        public Matrix4(IReadOnlyList<float> values)
        {
            if (values.Count != 16) {
                throw new ArgumentOutOfRangeException("values", "The size of the values collection must contain 16 elements.");
            }
            
            m11 = values[0];
            m12 = values[1];
            m13 = values[2];
            m14 = values[3];

            m21 = values[4];
            m22 = values[5];
            m23 = values[6];
            m24 = values[7];

            m31 = values[8];
            m32 = values[9];
            m33 = values[10];
            m34 = values[11];

            m41 = values[12];
            m42 = values[13];
            m43 = values[14];
            m44 = values[15];
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
                switch (index)
                {
                    case 0: return m11;
                    case 1: return m12;
                    case 2: return m13;
                    case 3: return m14;
                    case 4: return m21;
                    case 5: return m22;
                    case 6: return m23;
                    case 7: return m24;
                    case 8: return m31;
                    case 9: return m32;
                    case 10: return m33;
                    case 11: return m34;
                    case 12: return m41;
                    case 13: return m42;
                    case 14: return m43;
                    case 15: return m44;
                }

                throw new IndexOutOfRangeException();
            }

            set
            {
                switch (index)
                {
                    case 0: m11 = value; break;
                    case 1: m12 = value; break;
                    case 2: m13 = value; break;
                    case 3: m14 = value; break;
                    case 4: m21 = value; break;
                    case 5: m22 = value; break;
                    case 6: m23 = value; break;
                    case 7: m24 = value; break;
                    case 8: m31 = value; break;
                    case 9: m32 = value; break;
                    case 10: m33 = value; break;
                    case 11: m34 = value; break;
                    case 12: m41 = value; break;
                    case 13: m42 = value; break;
                    case 14: m43 = value; break;
                    case 15: m44 = value; break;

                    throw new IndexOutOfRangeException();
                }                
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
                if (row < 0 || row > 3)
                    throw new IndexOutOfRangeException("Rows must be in range 0 to 3");
                if (column < 0 || column > 3)
                    throw new IndexOutOfRangeException("Columns must be in range 0 to 3");
                return this[(row * 4) + column];
            }
            set
            {
                if (row < 0 || row > 3)
                    throw new IndexOutOfRangeException("Rows must be in range 0 to 3");
                if (column < 0 || column > 3)
                    throw new IndexOutOfRangeException("Columns must be in range 0 to 3");

                this[(row * 4) + column] = value;
            }
        }

        public float M11 { get { return this.m11; } set { this.m11 = value; } }
        public float M12 { get { return this.m12; } set { this.m12 = value; } }
        public float M13 { get { return this.m13; } set { this.m13 = value; } }
        public float M14 { get { return this.m14; } set { this.m14 = value; } }
        public float M21 { get { return this.m21; } set { this.m21 = value; } }
        public float M22 { get { return this.m22; } set { this.m22 = value; } }
        public float M23 { get { return this.m23; } set { this.m23 = value; } }
        public float M24 { get { return this.m24; } set { this.m24 = value; } }
        public float M31 { get { return this.m31; } set { this.m31 = value; } }
        public float M32 { get { return this.m32; } set { this.m32 = value; } }
        public float M33 { get { return this.m33; } set { this.m33 = value; } }
        public float M34 { get { return this.m34; } set { this.m34 = value; } }
        public float M41 { get { return this.m41; } set { this.m41 = value; } }
        public float M42 { get { return this.m42; } set { this.m42 = value; } }
        public float M43 { get { return this.m43; } set { this.m43 = value; } }
        public float M44 { get { return this.m44; } set { this.m44 = value; } }

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
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
            this.m34 = m34;
            this.m41 = m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;
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
                float l11 = this.m11;
                float l12 = this.m12;
                float l13 = this.m13;
                float l14 = this.m14;
                float l21 = this.m21;
                float l22 = this.m22;
                float l23 = this.m23;
                float l24 = this.m24;
                float l31 = this.m31;
                float l32 = this.m32;
                float l33 = this.m33;
                float l34 = this.m34;
                float l41 = this.m41;
                float l42 = this.m42;
                float l43 = this.m43;
                float l44 = this.m44;

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
        public static Matrix4 Multiply(Matrix4 matrix, float scalar)
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

            Matrix4 result = new Matrix4();

            result.M11 = a11 * scalar;
            result.M12 = a12 * scalar;
            result.M13 = a13 * scalar;
            result.M14 = a14 * scalar;

            result.M21 = a21 * scalar;
            result.M22 = a22 * scalar;
            result.M23 = a23 * scalar;
            result.M24 = a24 * scalar;

            result.M31 = a31 * scalar;
            result.M32 = a32 * scalar;
            result.M33 = a33 * scalar;
            result.M34 = a34 * scalar;

            result.M41 = a41 * scalar;
            result.M42 = a42 * scalar;
            result.M43 = a43 * scalar;
            result.M44 = a44 * scalar;

            return result;
        }

        /// <summary>
        /// Multiply two matrices and return product.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>The product of the two matrices.</returns>
        /// 
        public static Matrix4 Multiply(Matrix4 a, Matrix4 b)
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

            Matrix4 result = new Matrix4();

            result.M11 = (a11 * b11) + (a12 * b21) + (a13 * b31) + (a14 * b41);
            result.M12 = (a11 * b12) + (a12 * b22) + (a13 * b32) + (a14 * b42);
            result.M13 = (a11 * b13) + (a12 * b23) + (a13 * b33) + (a14 * b43);
            result.M14 = (a11 * b14) + (a12 * b24) + (a13 * b34) + (a14 * b44);

            result.M21 = (a21 * b11) + (a22 * b21) + (a23 * b31) + (a24 * b41);
            result.M22 = (a21 * b12) + (a22 * b22) + (a23 * b32) + (a24 * b42);
            result.M23 = (a21 * b13) + (a22 * b23) + (a23 * b33) + (a24 * b43);
            result.M24 = (a21 * b14) + (a22 * b24) + (a23 * b34) + (a24 * b44);

            result.M31 = (a31 * b11) + (a32 * b21) + (a33 * b31) + (a34 * b41);
            result.M32 = (a31 * b12) + (a32 * b22) + (a33 * b32) + (a34 * b42);
            result.M33 = (a31 * b13) + (a32 * b23) + (a33 * b33) + (a34 * b43);
            result.M34 = (a31 * b14) + (a32 * b24) + (a33 * b34) + (a34 * b44);

            result.M41 = (a41 * b11) + (a42 * b21) + (a43 * b31) + (a44 * b41);
            result.M42 = (a41 * b12) + (a42 * b22) + (a43 * b32) + (a44 * b42);
            result.M43 = (a41 * b13) + (a42 * b23) + (a43 * b33) + (a44 * b43);
            result.M44 = (a41 * b14) + (a42 * b24) + (a43 * b34) + (a44 * b44);

            return result;          
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting addition of the two matrices.</returns>
        public static Matrix4 Add(Matrix4 a, Matrix4 b)
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

            Matrix4 result = new Matrix4(); 

            result.m11 = a11 + b11;
            result.m12 = a12 + b12;
            result.m13 = a13 + b13;
            result.m14 = a14 + b14;

            result.m21 = a21 + b21;
            result.m22 = a22 + b22;
            result.m23 = a23 + b23;
            result.m24 = a24 + b24;

            result.m31 = a31 + b31;
            result.m32 = a32 + b32;
            result.m33 = a33 + b33;
            result.m34 = a34 + b34;

            result.m41 = a41 + b41;
            result.m42 = a42 + b42;
            result.m43 = a43 + b43;
            result.m44 = a44 + b44;

            return result;
        }

        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting subtraction of the two matrices.</returns>
        public static Matrix4 Subtract(Matrix4 a, Matrix4 b)
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

            Matrix4 result = new Matrix4();

            result.m11 = a11 - b11;
            result.m12 = a12 - b12;
            result.m13 = a13 - b13;
            result.m14 = a14 - b14;

            result.m21 = a21 - b21;
            result.m22 = a22 - b22;
            result.m23 = a23 - b23;
            result.m24 = a24 - b24;

            result.m31 = a31 - b31;
            result.m32 = a32 - b32;
            result.m33 = a33 - b33;
            result.m34 = a34 - b34;

            result.m41 = a41 - b41;
            result.m42 = a42 - b42;
            result.m43 = a43 - b43;
            result.m44 = a44 - b44;

            return result;
        }

        /// <summary>
        /// Swap all elements, rows become columns, columns become rows of the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to tranpose.</param>
        /// <param name="result">The transposed matrix</param>
        public static Matrix4 Transpose(Matrix4 matrix)
        {
            Matrix4 tMatrix = new Matrix4();
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

            return tMatrix;
        }

        /// <summary>
        /// Inverts the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to invert.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix4 Invert(Matrix4 matrix)
        {                 
            // Todo: Potentially add optional inverse performance tricks that only work on certain kinds of matrices e.g. affine transforms. Based on GPU gems code.

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

            var invertedMatrix = new Matrix4();

            invertedMatrix.m11 = l23 * l27;
            invertedMatrix.m21 = l24 * l27;
            invertedMatrix.m31 = l25 * l27;
            invertedMatrix.m41 = l26 * l27;
            invertedMatrix.m12 = -(((l2 * l17) - (l3 * l18)) + (l4 * l19)) * l27;
            invertedMatrix.m22 = (((l1 * l17) - (l3 * l20)) + (l4 * l21)) * l27;
            invertedMatrix.m32 = -(((l1 * l18) - (l2 * l20)) + (l4 * l22)) * l27;
            invertedMatrix.m42 = (((l1 * l19) - (l2 * l21)) + (l3 * l22)) * l27;
            invertedMatrix.m13 = (((l2 * l28) - (l3 * l29)) + (l4 * l30)) * l27;
            invertedMatrix.m23 = -(((l1 * l28) - (l3 * l31)) + (l4 * l32)) * l27;
            invertedMatrix.m33 = (((l1 * l29) - (l2 * l31)) + (l4 * l33)) * l27;
            invertedMatrix.m43 = -(((l1 * l30) - (l2 * l32)) + (l3 * l33)) * l27;
            invertedMatrix.m14 = -(((l2 * l34) - (l3 * l35)) + (l4 * l36)) * l27;
            invertedMatrix.m24 = (((l1 * l34) - (l3 * l37)) + (l4 * l38)) * l27;
            invertedMatrix.m34 = -(((l1 * l35) - (l2 * l37)) + (l4 * l39)) * l27;
            invertedMatrix.m44 = (((l1 * l36) - (l2 * l38)) + (l3 * l39)) * l27;

            return invertedMatrix;
        }

        /// <summary>
        /// Create a new translation matrix with the specified axis offset.
        /// </summary>
        /// <param name="x">X translation.</param>
        /// <param name="y">Y translation.</param>
        /// <param name="z">Z translation.</param>
        /// <returns>The resulting translation matrix.</returns>
        public static Matrix4 Translate(float x, float y, float z)
        {
            return new Matrix4(new float[] { 1.0f, 0.0f, 0.0f, x,
                                             0.0f, 1.0f, 0.0f, y,
                                             0.0f, 0.0f, 1.0f, z,
                                             0.0f, 0.0f, 0.0f, 1.0f});
        }

        /// <summary>
        /// Create a new translation matrix using a <see cref="Vector3"/>
        /// </summary>
        /// <param name="vec"><see cref="Vector3"/> to use for translation.</param>
        /// <returns>The resulting translation matrix.</returns>
        public static Matrix4 Translate(Vector3 vec)
        {
            return Matrix4.Translate(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Create a scaling matrix with the specified axis values.
        /// </summary>
        /// <param name="x">X scale.</param>
        /// <param name="y">Y scale.</param>
        /// <param name="z">Z scale.</param>
        /// <returns></returns>
        public static Matrix4 Scale(float x, float y, float z)
        {
            return new Matrix4(new float[] { x, 0.0f, 0.0f, 0.0f,
                                             0.0f, y, 0.0f, 0.0f,
                                             0.0f, 0.0f, z, 0.0f,
                                             0.0f, 0.0f, 0.0f, 1.0f});
        }

        /// <summary>
        /// Create a scaling matrix with the specified axis values.
        /// </summary>
        /// <param name="vec"><see cref="Vector3"/> to use for scaling.</param>
        /// <returns></returns>
        public static Matrix4 Scale(Vector3 vec)
        {
            return Matrix4.Scale(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Create a scaling matrix with the specified value for uniformly scaling each axis.
        /// </summary>
        /// <param name="scale">The value to uniformly scale by.</param>
        /// <returns></returns>
        public static Matrix4 Scale(float scale)
        {
            return Matrix4.Scale(scale, scale, scale);
        }

        /// <summary>
        /// Create a rotation matrix around the X axis by the given angle.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4 RotateX(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            return new Matrix4(new float[] { 1.0f, 0.0f, 0.0f, 0.0f,
                                             0.0f, cos, -sin, 0.0f,
                                             0.0f, sin, cos, 0.0f,
                                             0.0f, 0.0f, 0.0f, 1.0f});
        }

        /// <summary>
        /// Create a rotation matrix around the Y axis by the given angle.
        /// 
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4 RotateY(float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            return new Matrix4(new float[] { cos, 0.0f, sin, 0.0f,
                                             0.0f, 1.0f, 0.0f, 0.0f,
                                             -sin, 0.0f, cos, 0.0f,
                                             0.0f, 0.0f, 0.0f, 1.0f});
        }

        /// <summary>
        /// Create a rotation matrix around the Z axis by the given angle.
        /// 
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4 RotateZ(float angle)
        {
            float cos = (float)Math.Cos(angle);            
            float sin = (float)Math.Sin(angle);            

            return new Matrix4(new float[] { cos, -sin, 0.0f, 0.0f,
                                             sin, cos, 0.0f, 0.0f,
                                             0.0f, 0.0f, 1.0f, 0.0f,
                                             0.0f, 0.0f, 0.0f, 1.0f});
        }

        /// <summary>
        /// Create a rotation matrix around an axis by a given angle.
        /// </summary>
        /// <remarks>If you do not use a normalised vector for the axis, you will get undefined behaviour, in the interest of speed this function does not do any safety checks.</remarks>
        /// <param name="axis">A normalised unit vector representing the axis to rotate about.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4 RotateAxis(Vector3 axis, float angle)
        {
            float x = axis.x;
            float y = axis.y;
            float z = axis.z;

            float cos = (float)Math.Cos(angle);
            float cos1 = 1.0f - cos;
            float sin = (float)Math.Sin(angle);

            float xx1 = x * x * cos1;
            float xy1 = x * y * cos1;
            float xz1 = x * z * cos1;
            float yy1 = y * y * cos1;
            float yz1 = y * z * cos1;
            float zz1 = z * z * cos1;
            
            float sinx = x * sin;
            float siny = y * sin;
            float sinz = z * sin;

            return new Matrix4(new float[] { cos + xx1,     xy1 - sinz,     xz1 + siny, 0.0f,
                                             xy1 + sinz,    cos + yy1,      yz1 - sinx, 0.0f,
                                             xz1 - siny,    yz1 + sinx,     cos + zz1,  0.0f,
                                             0.0f,          0.0f,           0.0f,       1.0f});
        }

        /// <summary>
        /// Transform a <see cref="Vector4"/> by this matrix.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns>The resulting <see cref="Vector4"/> transformed by this matrix i.e. M * (x, y, z, w)</returns>
        public Vector4 Transform(Vector4 vec)
        {
            // Local variables for faster lookup.
            float l11 = this.m11;
            float l12 = this.m12;
            float l13 = this.m13;
            float l14 = this.m14;
            float l21 = this.m21;
            float l22 = this.m22;
            float l23 = this.m23;
            float l24 = this.m24;
            float l31 = this.m31;
            float l32 = this.m32;
            float l33 = this.m33;
            float l34 = this.m34;
            float l41 = this.m41;
            float l42 = this.m42;
            float l43 = this.m43;
            float l44 = this.m44;

            return new Vector4(
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
            this = Matrix4.Invert(this);
        }

        /// <summary>
        /// Swap all elements, rows become columns, columns become rows of this matrix.
        /// </summary>
        public void Transpose()
        {
            this = Matrix4.Transpose(this);
        }

        /// <summary>
        /// Multiply matrix components by scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The value you want to scale the matrix by.</param>
        /// <returns>The resulting multiplication of the matrix</returns>
        public static Matrix4 operator *(Matrix4 matrix, float scalar)
        {
            return Matrix4.Multiply(matrix, scalar);
        }     

        /// <summary>
        /// Multiply two matrices and return product.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>The product of the two matrices.</returns>
        /// 
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            return Matrix4.Multiply(a, b);
        }

        /// <summary>
        /// Transform a vector by a matrix, M*v
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector4 operator *(Matrix4 matrix, Vector4 vec)
        {
            return matrix.Transform(vec);
        }     

        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting subtraction of the two matrices.</returns>
        public static Matrix4 operator -(Matrix4 a, Matrix4 b)
        {
            return Matrix4.Subtract(a, b);
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting addition of the two matrices.</returns>
        public static Matrix4 operator +(Matrix4 a, Matrix4 b)
        {
            return Matrix4.Add(a, b);
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

            return Equals((Matrix4)obj);
        }

        /// <summary>
        /// Return a string representation of the Matrix.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + String.Format(": \n[{0}, {1}, {2}, {3}]\n[{4}, {5}, {6}, {7}]\n[{8}, {9}, {10}, {11}]\n[{12}, {13}, {14}, {15}]", m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
        }

        /// <summary>
        /// Returns the elements of a matrix in an array.
        /// </summary>
        /// <returns>An array of all sixteen matrix elements.</returns>
        public float[] ToArray()
        {
            return new float[] { m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44 };
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Vector2"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Matrix4 other)
        {
            return 
                (this.m11 == other.m11) && (this.m12 == other.m12) && (this.m13 == other.m13) && (this.m14 == other.m14) &&
                (this.m21 == other.m21) && (this.m22 == other.m22) && (this.m23 == other.m23) && (this.m24 == other.m24) &&
                (this.m31 == other.m31) && (this.m32 == other.m32) && (this.m33 == other.m33) && (this.m34 == other.m34) &&
                (this.m41 == other.m41) && (this.m42 == other.m42) && (this.m43 == other.m43) && (this.m44 == other.m44);  
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
                m11.GetHashCode() ^ m12.GetHashCode() ^ m13.GetHashCode() ^ m14.GetHashCode() ^
                m21.GetHashCode() ^ m22.GetHashCode() ^ m23.GetHashCode() ^ m24.GetHashCode() ^
                m31.GetHashCode() ^ m32.GetHashCode() ^ m33.GetHashCode() ^ m34.GetHashCode() ^
                m41.GetHashCode() ^ m42.GetHashCode() ^ m43.GetHashCode() ^ m44.GetHashCode();
        }

        private float m11;
        private float m12;
        private float m13;
        private float m14;
        private float m21;
        private float m22;
        private float m23;
        private float m24;
        private float m31;
        private float m32;
        private float m33;
        private float m34;
        private float m41;
        private float m42;
        private float m43;
        private float m44;
    }
}
