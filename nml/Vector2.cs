﻿using System;
using System.Runtime.CompilerServices;

namespace Nml
{
    /// <summary>
    /// A vector (x,y)
    /// </summary>
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
        /// <param name="values">The floats to initialise the values with</param>
        public Vector2(float[] values)
            : this()
        {
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length != 2)
                throw new ArgumentOutOfRangeException("values", "Values must be an array of exactly two elements");

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
        /// <param name="index">Element to get, must be either 0/1 which corresponds to x/y</param>
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
            Vector2 result;
            Vector2.Add(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="result">The addition of the two vectors (a.x+b.x, a.y+b.y)</param>
#if !COMPAT
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Add(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result = new Vector2(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The subtraction of the two vectors (a.x-b.x, a.y-b.y)</returns>
        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            Vector2 result;
            Vector2.Subtract(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="result">The subtraction of the two vectors (a.x-b.x, a.y-b.y)</param>
#if !COMPAT
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Subtract(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result = new Vector2(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Multiply vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The multiplication of the vector (v.x*s, v.y*s)</returns>
        public static Vector2 Multiply(Vector2 vector, float scalar)
        {
            Vector2 result;
            Vector2.Multiply(ref vector, scalar, out result);
            return result;
        }

        /// <summary>
        /// Multiply vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <param name="result">The multiplication of the vector (v.x*s, v.y*s)</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Multiply(ref Vector2 vector, float scalar, out Vector2 result)
        {
            result = new Vector2(vector.x * scalar, vector.y * scalar);
        }

        /// <summary>
        /// Divide vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The division of the vector (v.x/s, v.y/s)</returns>
        public static Vector2 Divide(Vector2 vector, float scalar)
        {
            Vector2 result;
            Vector2.Divide(ref vector, scalar, out result);
            return result;
        }

        /// <summary>
        /// Divide vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <param name="result">The division of the vector (v.x/s, v.y/s)</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Divide(ref Vector2 vector, float scalar, out Vector2 result)
        {
            result = new Vector2(vector.x / scalar, vector.y / scalar);
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The dot product of the two vectors (a.x*b.x + a.y*b.y)</returns>
        public static float Dot(Vector2 a, Vector2 b)
        {
            float result;
            Vector2.Dot(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="result">The dot product of the two vectors (a.x*b.x + a.y*b.y)</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Dot(ref Vector2 a, ref Vector2 b, out float result)
        {
            result = (a.x * b.x) + (a.y * b.y);
        }

        /// <summary>
        /// Normalise passed in vector
        /// </summary>
        /// <param name="a">The vector to normalise</param>
        /// <returns>The normalised vector.</returns>
        public static Vector2 Normalise(Vector2 a)
        {
            Vector2 result;
            Normalise(ref a, out result);
            return result;
        }

        /// <summary>
        /// Normalise passed in vector
        /// </summary>
        /// <param name="a">The vector to normalise</param>
        /// <param name="result">The normalised vector.</param>
#if !COMPAT
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Normalise(ref Vector2 a, out Vector2 result)
        {
            float length = a.LengthSquared;
            if (length > 1e-6f)
            {
                float inv = 1.0f / a.Length;
                float x = a.x * inv;
                float y = a.y * inv;

                result = new Vector2(x, y);
            }
            else
            {
                // If the length is greater than the tolerance then we just force a return of a unit vector.
                result = new Vector2(1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Normalise this vector
        /// </summary>
        public void Normalise()
        {
            Vector2 result;
            Vector2.Normalise(ref this, out result);
            this = result;
        }

        /// <summary>
        /// Linearly interpolate between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Vector A and 1 is Vector B</param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            Vector2 result;
            Vector2.Lerp(ref a, ref b, t, out result);
            return result;
        }

        /// <summary>
        /// Linearly interpolate between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Vector A and 1 is Vector B</param>
        /// <param name="result">A linear combination: a when t=0 or b when t=1 else a point between.</param>
#if !COMPAT
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Lerp(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            float x = a.x + (b.x - a.x) * t;
            float y = a.y + (b.y - a.y) * t;

            result = new Vector2(x, y);
        }

        /// <summary>
        /// Linearly interpolate between this and another vector.
        /// </summary>
        /// <param name="vector">Vector to interpolate with.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is this and 1 is Vector B</param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public Vector2 Lerp(Vector2 vector, float t)
        {
            return Vector2.Lerp(this, vector, t);
        }

        /// <summary>
        /// Get the distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The euclidean distance between a and b, you can also consider this the length of the distance (displacement) vector (a-b).Length.</returns>
        public static float Distance(Vector2 a, Vector2 b)
        {
            float result;
            Vector2.Distance(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Get the distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="result">The euclidean distance between a and b, you can also consider this the length of the distance (displacement) vector (a-b).Length.</param>
#if !COMPAT        
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Distance(ref Vector2 a, ref Vector2 b, out float result)
        {
            float x = a.x - b.x;
            float y = a.y - b.y;

            result = (float)Math.Sqrt((x * x) + (y * y));
        }
        /// <summary>
        /// Distance between this vector and specified vector.
        /// </summary>
        /// <param name="vector">Vector to calculate distance against.</param>
        /// <returns>The euclidean distance between a and b, you can also consider this the length of the distance (displacement) vector (a-b).Length.</returns>
        public float Distance(Vector2 vector)
        {
            float result;
            Vector2.Distance(ref this, ref vector, out result);
            return result;
        }

        /// <summary>
        /// Get the squared distance between two vectors.
        /// This is slightly faster as we avoid the square root, use this if you're just comparing distances.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The squared distance between a and b</returns>
        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            float result;
            Vector2.DistanceSquared(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Get the squared distance between two vectors.
        /// This is slightly faster as we avoid the square root, use this if you're just comparing distances.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="result">The squared distance between a and b</param>
#if !COMPAT
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
#endif
        public static void DistanceSquared(ref Vector2 a, ref Vector2 b, out float result)
        {
            float x = a.x - b.x;
            float y = a.y - b.y;
            
            result = (float)(x * x) + (y * y);
        }

        /// <summary>
        /// Get the squared distance between this vector and specified vector.
        /// This is slightly faster as we avoid the square root, use this if you're just comparing distances.
        /// </summary>
        /// <param name="vector">Vector to calculate distance against.</param>
        /// <returns>The squared distance between a and b</returns>
        public float DistanceSquared(Vector2 vector)
        {
            float result;
            Vector2.DistanceSquared(ref this, ref vector, out result);
            return result;
        }

        /// <summary>
        /// Reverse the direction of a given vector.
        /// </summary>
        /// <param name="vector">Vector to negate.</param>
        /// <param name="result">The vector in the opposite direction.</param>
        public static void Negate(ref Vector2 vector, out Vector2 result)
        {
            result = new Vector2(-vector.x, -vector.y);
        }

        /// <summary>
        /// Reverse the direction of a given vector.
        /// </summary>
        /// <param name="vector">Vector to negate.</param>
        /// <returns>The vector in the opposite direction.</returns>
        public static Vector2 Negate(Vector2 vector)
        {
            Vector2 result;
            Vector2.Negate(ref vector, out result);
            return result;
        }

        /// <summary>
        /// Reverse the direction of a given vector.
        /// </summary>
        /// <param name="vector">Vector to negate.</param>
        /// <returns>The vector in the opposite direction.</returns>
        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.x, -vector.y);
        }

        /// <summary>
        /// Multiply vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The multiplication of the vector (v.x*s, v.y*s)</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            Vector2 result;
            Vector2.Multiply(ref vector, scalar, out result);
            return result;
        }

        /// <summary>
        /// Divide vector components by scalar.
        /// </summary>
        /// <param name="vector">The vector to scale.</param>
        /// <param name="scalar">The value you want to scale the vector by.</param>
        /// <returns>The division of the vector (v.x/s, v.y/s)</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            Vector2 result;
            Vector2.Divide(ref vector, scalar, out result);
            return result;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The subtraction of the two vectors (a.x-b.x, a.y-b.y)</returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            Vector2 result;
            Vector2.Subtract(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>The addition of the two vectors (a.x+b.x, a.y+b.y)</returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            Vector2 result;
            Vector2.Add(ref a, ref b, out result);
            return result;
        }   

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is exactly equal to this instance.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns><c>true</c> if the two vectors are equal <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is not equal to this instance.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns><c>true</c> if the two vectors are not equal <c>false</c> otherwise.</returns>
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a.Equals(b));
        }

        /// <summary>
        /// Returns the components of a vector in an array.
        /// </summary>
        /// <returns>An array with the components of the vector.</returns>
        public float[] ToArray()
        {
            return new float[] { x, y };
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
        /// <returns><c>true</c> if the objects are equal <c>false</c> otherwise.</returns>
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return Equals((Vector2)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is exactly equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to compare with.</param>
        /// <returns><c>true</c> if the two vectors are equal <c>false</c> otherwise.</returns>
        public bool Equals(Vector2 other)
        {
            return (this.x == other.x) && (this.y == other.y);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is equal to this instance up to the given precision.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to compare with.</param>
        /// <param name="epsilon">The precision.</param>
        /// <returns><c>true</c> if the two vectors are equal to the given epsilon <c>false</c> otherwise.</returns>
        public bool Equals(Vector2 other, float epsilon)
        {
            return (Math.Abs(this.x - other.x) < epsilon) &&
                   (Math.Abs(this.y - other.y) < epsilon);
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

        /// <summary>
        /// Gets the component of the vector
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Element value at specified index</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private float GetElement(int index)
        {
            switch (index)
            {
                case 0:
                    return this.x;
                case 1:
                    return this.y;
            }

            throw new ArgumentOutOfRangeException("index", "Must be in the range 0-1");
        }

        /// <summary>
        /// Sets the element of a vector.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private void SetElement(int index, float value)
        {
            switch (index)
            {
                case 0:
                    this.x = value;
                    break;
                case 1:
                    this.y = value;
                    break;

                    throw new ArgumentOutOfRangeException("index", "Must be in the range 0-1");
            }
        }
    }
}
