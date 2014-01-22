using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nml
{
    /// <summary>
    /// This is a 4x4 matrix, it is a generic case for working with transformations in 3d space.
    /// The data is stored in row-major order, as per C# arrays, e.g. m12 refers to first row second column.
    /// The mathmatical operations on vector transforms are for column major vectors i.e. M*v.
    /// </summary>
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

        /// <summary>
        /// Calculate the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        public float Determinant
        {
            get
            {
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
            float l11 = matrix[0, 0] * scalar;
            float l12 = matrix[0, 1] * scalar;
            float l13 = matrix[0, 2] * scalar;
            float l14 = matrix[0, 3] * scalar;

            float l21 = matrix[1, 0] * scalar;
            float l22 = matrix[1, 1] * scalar;
            float l23 = matrix[1, 2] * scalar;
            float l24 = matrix[1, 3] * scalar;

            float l31 = matrix[2, 0] * scalar;
            float l32 = matrix[2, 1] * scalar;
            float l33 = matrix[2, 2] * scalar;
            float l34 = matrix[2, 3] * scalar;

            float l41 = matrix[3, 0] * scalar;
            float l42 = matrix[3, 1] * scalar;
            float l43 = matrix[3, 2] * scalar;
            float l44 = matrix[3, 3] * scalar;

            return new Matrix4(new float[] {
                                        l11, l12, l13, l14,
                                        l21, l22, l23, l24,
                                        l31, l32, l33, l34,
                                        l41, l42, l43, l44,
            });
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
            float l11 = (a[0, 0] * b[0, 0]) + (a[0, 1] * b[1, 0]) + (a[0, 2] * b[2, 0]) + (a[0, 3] * b[3, 0]);
            float l12 = (a[0, 0] * b[0, 1]) + (a[0, 1] * b[1, 1]) + (a[0, 2] * b[2, 1]) + (a[0, 3] * b[3, 1]);
            float l13 = (a[0, 0] * b[0, 2]) + (a[0, 1] * b[1, 2]) + (a[0, 2] * b[2, 2]) + (a[0, 3] * b[3, 2]);
            float l14 = (a[0, 0] * b[0, 3]) + (a[0, 1] * b[1, 3]) + (a[0, 2] * b[2, 3]) + (a[0, 3] * b[3, 3]);

            float l21 = (a[1, 0] * b[0, 0]) + (a[1, 1] * b[1, 0]) + (a[1, 2] * b[2, 0]) + (a[1, 3] * b[3, 0]);
            float l22 = (a[1, 0] * b[0, 1]) + (a[1, 1] * b[1, 1]) + (a[1, 2] * b[2, 1]) + (a[1, 3] * b[3, 1]);
            float l23 = (a[1, 0] * b[0, 2]) + (a[1, 1] * b[1, 2]) + (a[1, 2] * b[2, 2]) + (a[1, 3] * b[3, 2]);
            float l24 = (a[1, 0] * b[0, 3]) + (a[1, 1] * b[1, 3]) + (a[1, 2] * b[2, 3]) + (a[1, 3] * b[3, 3]);

            float l31 = (a[2, 0] * b[0, 0]) + (a[2, 1] * b[1, 0]) + (a[2, 2] * b[2, 0]) + (a[2, 3] * b[3, 0]);
            float l32 = (a[2, 0] * b[0, 1]) + (a[2, 1] * b[1, 1]) + (a[2, 2] * b[2, 1]) + (a[2, 3] * b[3, 1]);
            float l33 = (a[2, 0] * b[0, 2]) + (a[2, 1] * b[1, 2]) + (a[2, 2] * b[2, 2]) + (a[2, 3] * b[3, 2]);
            float l34 = (a[2, 0] * b[0, 3]) + (a[2, 1] * b[1, 3]) + (a[2, 2] * b[2, 3]) + (a[2, 3] * b[3, 3]);

            float l41 = (a[3, 0] * b[0, 0]) + (a[3, 1] * b[1, 0]) + (a[3, 2] * b[2, 0]) + (a[3, 3] * b[3, 0]);
            float l42 = (a[3, 0] * b[0, 1]) + (a[3, 1] * b[1, 1]) + (a[3, 2] * b[2, 1]) + (a[3, 3] * b[3, 1]);
            float l43 = (a[3, 0] * b[0, 2]) + (a[3, 1] * b[1, 2]) + (a[3, 2] * b[2, 2]) + (a[3, 3] * b[3, 2]);
            float l44 = (a[3, 0] * b[0, 3]) + (a[3, 1] * b[1, 3]) + (a[3, 2] * b[2, 3]) + (a[3, 3] * b[3, 3]);

            return new Matrix4(new float[] {
                                        l11, l12, l13, l14,
                                        l21, l22, l23, l24,
                                        l31, l32, l33, l34,
                                        l41, l42, l43, l44,
            });
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting addition of the two matrices.</returns>
        public static Matrix4 Add(Matrix4 a, Matrix4 b)
        {
            float l11 = a[0, 0] + b[0, 0];
            float l12 = a[0, 1] + b[0, 1];
            float l13 = a[0, 2] + b[0, 2];
            float l14 = a[0, 3] + b[0, 3];

            float l21 = a[1, 0] + b[1, 0];
            float l22 = a[1, 1] + b[1, 1];
            float l23 = a[1, 2] + b[1, 2];
            float l24 = a[1, 3] + b[1, 3];

            float l31 = a[2, 0] + b[2, 0];
            float l32 = a[2, 1] + b[2, 1];
            float l33 = a[2, 2] + b[2, 2];
            float l34 = a[2, 3] + b[2, 3];

            float l41 = a[3, 0] + b[3, 0];
            float l42 = a[3, 1] + b[3, 1];
            float l43 = a[3, 2] + b[3, 2];
            float l44 = a[3, 3] + b[3, 3];

            return new Matrix4(new float[] {
                                        l11, l12, l13, l14,
                                        l21, l22, l23, l24,
                                        l31, l32, l33, l34,
                                        l41, l42, l43, l44,
            });
        }

        /// <summary>
        /// Subtract one matrix from another.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The resulting subtraction of the two matrices.</returns>
        public static Matrix4 Subtract(Matrix4 a, Matrix4 b)
        {
            float l11 = a[0, 0] - b[0, 0];
            float l12 = a[0, 1] - b[0, 1];
            float l13 = a[0, 2] - b[0, 2];
            float l14 = a[0, 3] - b[0, 3];

            float l21 = a[1, 0] - b[1, 0];
            float l22 = a[1, 1] - b[1, 1];
            float l23 = a[1, 2] - b[1, 2];
            float l24 = a[1, 3] - b[1, 3];

            float l31 = a[2, 0] - b[2, 0];
            float l32 = a[2, 1] - b[2, 1];
            float l33 = a[2, 2] - b[2, 2];
            float l34 = a[2, 3] - b[2, 3];

            float l41 = a[3, 0] - b[3, 0];
            float l42 = a[3, 1] - b[3, 1];
            float l43 = a[3, 2] - b[3, 2];
            float l44 = a[3, 3] - b[3, 3];

            return new Matrix4(new float[] {
                                        l11, l12, l13, l14,
                                        l21, l22, l23, l24,
                                        l31, l32, l33, l34,
                                        l41, l42, l43, l44,
            });
        }

        /// <summary>
        /// Swap all elements, rows become columns, columns become rows.
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

        public static Matrix4 Invert(Matrix4 matrix)
        {     
            // All the local variables are because it's generally faster in C#.
            // Todo: Potentially add optional inverse performance tricks that only work on certain kinds of matrices e.g. affine transforms. Based on GPU gems code.

            float l1 = matrix[0, 0];
            float l2 = matrix[0, 1];
            float l3 = matrix[0, 2];
            float l4 = matrix[0, 3];
            float l5 = matrix[1, 0];
            float l6 = matrix[1, 1];
            float l7 = matrix[1, 2];
            float l8 = matrix[1, 3];
            float l9 = matrix[2, 0];
            float l10 = matrix[2, 1];
            float l11 = matrix[2, 2];
            float l12 = matrix[2, 3];
            float l13 = matrix[3, 0];
            float l14 = matrix[3, 1];
            float l15 = matrix[3, 2];
            float l16 = matrix[3, 3];

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

            invertedMatrix[0, 0] = l23 * l27;
            invertedMatrix[1, 0] = l24 * l27;
            invertedMatrix[2, 0] = l25 * l27;
            invertedMatrix[3, 0] = l26 * l27;
            invertedMatrix[0, 1] = -(((l2 * l17) - (l3 * l18)) + (l4 * l19)) * l27;
            invertedMatrix[1, 1] = (((l1 * l17) - (l3 * l20)) + (l4 * l21)) * l27;
            invertedMatrix[2, 1] = -(((l1 * l18) - (l2 * l20)) + (l4 * l22)) * l27;
            invertedMatrix[3, 1] = (((l1 * l19) - (l2 * l21)) + (l3 * l22)) * l27;
            invertedMatrix[0, 2] = (((l2 * l28) - (l3 * l29)) + (l4 * l30)) * l27;
            invertedMatrix[1, 2] = -(((l1 * l28) - (l3 * l31)) + (l4 * l32)) * l27;
            invertedMatrix[2, 2] = (((l1 * l29) - (l2 * l31)) + (l4 * l33)) * l27;
            invertedMatrix[3, 2] = -(((l1 * l30) - (l2 * l32)) + (l3 * l33)) * l27;
            invertedMatrix[0, 3] = -(((l2 * l34) - (l3 * l35)) + (l4 * l36)) * l27;
            invertedMatrix[1, 3] = (((l1 * l34) - (l3 * l37)) + (l4 * l38)) * l27;
            invertedMatrix[2, 3] = -(((l1 * l35) - (l2 * l37)) + (l4 * l39)) * l27;
            invertedMatrix[3, 3] = (((l1 * l36) - (l2 * l38)) + (l3 * l39)) * l27;

            return invertedMatrix;
        }

        /// <summary>
        /// Creates a new translation matrix with the specified axis offset.
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
        /// Creates a new translation matrix using a <see cref="Vector3"/>
        /// </summary>
        /// <param name="vec"><see cref="Vector3"/> to use for translation.</param>
        /// <returns>The resulting translation matrix.</returns>
        public static Matrix4 Translate(Vector3 vec)
        {
            return Matrix4.Translate(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Creates a scaling matrix with the specified axis values.
        /// </summary>
        /// <param name="x">X scale.</param>
        /// <param name="y">Y scale.</param>
        /// <param name="z">Z scale.</param>
        /// <returns></returns>
        public static Matrix4 Scaling(float x, float y, float z)
        {
            return new Matrix4(new float[] { x, 0.0f, 0.0f, 0.0f,
                                             0.0f, y, 0.0f, 0.0f,
                                             0.0f, 0.0f, z, 0.0f,
                                             0.0f, 0.0f, 0.0f, 1.0f});
        }

        /// <summary>
        /// Creates a scaling matrix with the specified axis values.
        /// </summary>
        /// <param name="vec"><see cref="Vector3"/> to use for scaling.</param>
        /// <returns></returns>
        public static Matrix4 Scaling(Vector3 vec)
        {
            return Matrix4.Scaling(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Creates a scaling matrix with the specified value for uniformly scaling each axis.
        /// </summary>
        /// <param name="scale">The value to uniformly scale by.</param>
        /// <returns></returns>
        public static Matrix4 Scaling(float scale)
        {
            return Matrix4.Scaling(scale, scale, scale);
        }

        /// <summary>
        /// Transform a <see cref="Vector4"/> by this matrix.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns>The resulting <see cref="Vector4"/> transformed by this matrix i.e. M * (x, y, z, w)</returns>
        public Vector4 Transform(Vector4 vec)
        {
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

        public void Invert()
        {
            this = Matrix4.Invert(this);
        }

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
