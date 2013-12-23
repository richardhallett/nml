using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nml
{
    public struct Matrix4 : IEquatable<Matrix4>
    {
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
        /// Divide matrix components by scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The value you want to scale the matrix by.</param>
        /// <returns>The resulting division of the matrix</returns>
        public static Matrix4 Divide(Matrix4 matrix, float scalar)
        {
            float l11 = matrix[0, 0] / scalar;
            float l12 = matrix[0, 1] / scalar;
            float l13 = matrix[0, 2] / scalar;
            float l14 = matrix[0, 3] / scalar;

            float l21 = matrix[1, 0] / scalar;
            float l22 = matrix[1, 1] / scalar;
            float l23 = matrix[1, 2] / scalar;
            float l24 = matrix[1, 3] / scalar;

            float l31 = matrix[2, 0] / scalar;
            float l32 = matrix[2, 1] / scalar;
            float l33 = matrix[2, 2] / scalar;
            float l34 = matrix[2, 3] / scalar;

            float l41 = matrix[3, 0] / scalar;
            float l42 = matrix[3, 1] / scalar;
            float l43 = matrix[3, 2] / scalar;
            float l44 = matrix[3, 3] / scalar;

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
        /// Divide matrix components by scalar.
        /// </summary>
        /// <param name="matrix">The matrix to scale.</param>
        /// <param name="scalar">The value you want to scale the matrix by.</param>
        /// <returns>The resulting division of the matrix</returns>
        public static Matrix4 operator /(Matrix4 matrix, float scalar)
        {
            return Matrix4.Divide(matrix, scalar);
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
