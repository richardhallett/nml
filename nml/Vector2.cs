using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nml
{
    [Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {       
        /// <summary>
        /// Creates a new instance of <see cref="Vector2"/> with specified optional values.
        /// </summary>
        /// <param name="x">x component value</param>
        /// <param name="y">y component value</param>
        public Vector2(float x = 0.0f, float y = 0.0f) 
            : this()
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Vector2"/> with all values set to same value.
        /// </summary>
        /// <param name="value">Value to set x,y component values to.</param>
        public Vector2(float value)
            : this()
        {
            this.x = value;
            this.y = value;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Vector2"/> with values specified by list collection.
        /// </summary>
        /// <param name="values">List collection of floats</param>
        public Vector2(IReadOnlyList<float> values)
            : this()
        {
            this.x = values[0];
            this.y = values[1];
        }

        /// <summary>
        /// x component
        /// </summary>
        public float x { get; set; }
        /// <summary>
        /// y component.
        /// </summary>
        public float y { get; set; }

        /// <summary>
        /// Provides array style indexing to vectors components.
        /// </summary>
        /// <param name="i">Element to get, must be either 0/1 which corresponds to x/y</param>
        /// <returns>Element value at specified index.</returns>
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                }

                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                }

                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Calculate the length of this vector.
        /// </summary>
        public float Length
        {
            get
            {
                float x = this.x;
                float y = this.y;
                return (float)Math.Sqrt((x * x) + (y * y));
            }
        }

        /// <summary>
        /// Calculate the squared length of this vector.
        /// If you're just comparing lengths of vectors then this is faster than <see cref="Vector2.Length"/> as it doesn't do a square root, which is only required if you need the actual value.
        /// </summary>
        public float LengthSquared
        {
            get
            {
                float x = this.x;
                float y = this.y;
                return (float)(x * x) + (y * y);
            }
        }

        /// <summary>
        /// Is this vector a unit vector.
        /// </summary>
        public bool IsNormalised
        {
            get 
            {
                return Math.Abs(this.LengthSquared - 1.0f) < 1e-6f;
            }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with zeros in all component values.
        /// </summary>
        public static Vector2 Zero { get { return new Vector2(); } }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with ones in all component values.
        /// </summary>
        public static Vector2 One { get { return new Vector2(1.0f); } }

        /// <summary>
        /// Returns a x unit <see cref="Vector2"/> 
        /// </summary>
        public static Vector2 UnitX { get { return new Vector2(1.0f, 0.0f); } }

        /// <summary>
        /// Returns a y unit <see cref="Vector2"/> 
        /// </summary>
        public static Vector2 UnitY { get { return new Vector2(0.0f, 1.0f); } }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The addition of the two vectors (a.x+b.x, a.y+b.y)</returns>
        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        
        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The subtraction of the two vectors (a.x-b.x, a.y-b.y)</returns>
        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Multiply vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The multiplication of the vector (v.x*s, v.y*s)</returns>
        public static Vector2 Multiply(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }       

        /// <summary>
        /// Divide vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The division of the vector (v.x/s, v.y/s)</returns>
        public static Vector2 Divide(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The dot product of the two vectors (a.x*b.x + a.y*b.y)</returns>
        public static float Dot(Vector2 a, Vector2 b)
        {
            return (a.x * b.x) + (a.y * b.y);
        }

        /// <summary>
        /// Normalise passed in vector
        /// </summary>
        /// <param name="a">The vector to normalise</param>
        /// <returns>The normalised vector.</returns>
        public static Vector2 Normalise(Vector2 a)
        {
            float length = a.LengthSquared;
            if (length > 1e-6f)
            {
                float inv = 1.0f / a.Length;
                float x = a.x * inv;
                float y = a.y * inv;

                return new Vector2(x, y);
            }
            else
            {
                // If the length is greater than the tolerance then we just force a return of a unit vector.
                // Not 100% sure on this.
                return new Vector2(1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Normalise this vector
        /// </summary>
        public void Normalise()
        {
            this = Vector2.Normalise(this);
        }

        /// <summary>
        /// Multiply vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The multiplication of the vector (v.x*s, v.y*s)</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return Vector2.Multiply(vector, scalar);
        }

        /// <summary>
        /// Divide vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The division of the vector (v.x/s, v.y/s)</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return Vector2.Divide(vector, scalar);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The subtraction of the two vectors (a.x-b.x, a.y-b.y)</returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return Vector2.Subtract(a, b);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The addition of the two vectors (a.x+b.x, a.y+b.y)</returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return Vector2.Add(a, b);
        }

        /// <summary>
        /// Return a string representation of the vector.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + String.Format(": ({0}, {1})", x, y);
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

            return Equals((Vector2)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Vector2"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Vector2 other)
        {
            return (this.x == other.x) && (this.y == other.y);
        }

        /// <summary>
        /// Returns a hash code.
        /// </summary>
        /// <returns>
        /// A hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}
