﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace nml
{
    /// <summary>
    /// Represents a rotation or orientation in 3d.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        /// <summary>
        /// Construct a new quaternion from a vector and w component.
        /// </summary>
        /// <param name="v">The vector component.</param>
        /// <param name="w">The w component.</param>
        public Quaternion(Vector3 v, float w)
            : this()
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = w;
        }

        /// <summary>
        /// Construct a new quaternion.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        /// <param name="z">Z component.</param>
        /// <param name="w">W component.</param>
        public Quaternion(float x, float y, float z, float w)
            : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Return an identity quaternion.
        /// </summary>
        public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        /// The x component part of the quaternion.
        /// </summary>
        public float x { get; set; }

        /// <summary>
        /// The y component part of the quaternion.
        /// </summary>
        public float y { get; set; }

        /// <summary>
        /// The z component part of the quaternion.
        /// </summary>
        public float z { get; set; }

        /// <summary>
        /// The scalar component part of the quaternion.
        /// </summary>
        public float w { get; set; }

        /// <summary>
        /// Calculate the length of this quaternion.
        /// </summary>
        public float Length
        {
            get
            {
                float x = this.x;
                float y = this.y;
                float z = this.z;
                float w = this.w;
                return (float)Math.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
            }
        }

        /// <summary>
        /// Calculate the squared length of this quaternion.
        /// </summary>
        public float LengthSquared
        {
            get
            {
                float x = this.x;
                float y = this.y;
                float z = this.z;
                float w = this.w;
                return (float)(x * x) + (y * y) + (z * z) + (w * w);
            }
        }

        /// <summary>
        /// Is this vector a unit quaternion.
        /// </summary>
        public bool IsNormalised
        {
            get
            {
                return Math.Abs(this.LengthSquared - 1.0f) < 1e-6f;
            }
        }

        /// <summary>
        /// Normalise passed in quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to normalise</param>
        /// <returns>The normalised quaternion.</returns>
        public static Quaternion Normalise(Quaternion quat)
        {
            float length = quat.LengthSquared;
            if (length > 1e-6f)
            {
                float inv = 1.0f / quat.Length;
                float x = quat.x * inv;
                float y = quat.y * inv;
                float z = quat.z * inv;
                float w = quat.w * inv;

                return new Quaternion(x, y, z, w);
            }
            else
            {
                // If the length is greater than the tolerance then we just force a return of a unit vector.
                // Not 100% sure on this.
                return new Quaternion(1.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Normalise this quaternion.
        /// </summary>
        public void Normalise()
        {
            this = Quaternion.Normalise(this);
        }

        /// <summary>
        /// Get the conjugate of the passed in quaternion.
        /// </summary>
        /// <param name="quat">A quaternion</param>
        /// <returns>The conjugated quaternion.</returns>
        public static Quaternion Conjugate(Quaternion quat)
        {
            float x = -quat.x;
            float y = -quat.y;
            float z = -quat.z;
            float w = quat.w;

            return new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// Get the conjugate of this quaternion.
        /// </summary>
        public void Conjugate()
        {
            this = Quaternion.Conjugate(this);
        }

        /// <summary>
        /// Inverts the given quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to invert.</param>
        /// <returns>The inverted quaternion.</returns>
        public static Quaternion Invert(Quaternion quat)
        {
            float inv = 1.0f / quat.LengthSquared;
            float x = -quat.x * inv;
            float y = -quat.y * inv;
            float z = -quat.z * inv;
            float w = quat.w * inv;

            return new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// Multiply two quaternions together (a*b).
        /// Note: Quaternion multiplication is not commutative.
        /// </summary>
        /// <param name="a">First quaternion</param>
        /// <param name="b">Second quaternion</param>
        /// <returns>The product of the two quaternions.</returns>
        /// 
        public static Quaternion Multiply(Quaternion a, Quaternion b)
        {
            float ax = a.x;
            float ay = a.y;
            float az = a.z;
            float aw = a.w;
            float bx = b.x;
            float by = b.y;
            float bz = b.z;
            float bw = b.w;

            return new Quaternion(aw * bx + ax * bw + ay * bz - az * by,
                                  aw * by + ay * bw + az * bx - ax * bz,
                                  aw * bz + az * bw + ax * by - ay * bx,
                                  aw * bw - ax * bx - ay * by - az * bz);
        }

        /// <summary>
        /// Multiply quaternion components by scalar.
        /// </summary>
        /// <param name="quaternion">Quaternion to scale</param>
        /// <param name="scalar">Amount to scale by</param>
        /// <returns>The scaled quaternion.</returns>
        /// 
        public static Quaternion Multiply(Quaternion quaternion, float scalar)
        {
            return new Quaternion(quaternion.x * scalar, quaternion.y * scalar, quaternion.z * scalar, quaternion.w * scalar);
        }

        /// <summary>
        /// Multiply two quaternions together (a*b).
        /// Note: Quaternion multiplication is not commutative i.e. (a*b is not the same as b*a)
        /// </summary>
        /// <param name="a">First quaternion</param>
        /// <param name="b">Second quaternion</param>
        /// <returns>The product of the two quaternions.</returns>
        /// 
        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return Quaternion.Multiply(a, b);
        }

        /// <summary>
        /// Multiply quaternion components by scalar.
        /// </summary>
        /// <param name="quaternion">Quaternion to scale</param>
        /// <param name="scalar">Amount to scale by</param>
        /// <returns>The scaled quaternion.</returns>
        /// 
        public static Quaternion operator *(Quaternion quaternion, float scalar)
        {
            return Quaternion.Multiply(quaternion, scalar);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Quaternion"/> is exactly equal to this instance.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns><c>true</c> if the two quaternions are equal <c>false</c> otherwise.</returns>
        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Quaternion"/> is not equal to this instance.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns><c>true</c> if the two quaternions are not equal <c>false</c> otherwise.</returns>
        public static bool operator !=(Quaternion a, Quaternion b)
        {
            return !(a.Equals(b));
        }

        /// <summary>
        /// Return a string representation of the quaternion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + String.Format(": ({0}, {1}, {2}, {3})", this.x, this.y, this.x, this.w);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
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
        /// Determines whether the specified <see cref="Quaternion"/> is exactly equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Quaternion"/> to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Quaternion"/> is equal to this instance; otherwise, <c>false</c>.
        /// <returns><c>true</c> if the two quaternions are equal <c>false</c> otherwise.</returns>
        public bool Equals(Quaternion other)
        {
            return (this.x == other.x) && (this.y == other.y) && (this.z == other.z) && (this.w == other.w);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Quaternion"/> is equal to this instance up to the given precision.
        /// </summary>
        /// <param name="other">The <see cref="Quaternion"/> to compare with.</param>
        /// <param name="epsilon">The precision.</param>
        /// <returns><c>true</c> if the two quaternions are equal up to the given epsilon <c>false</c> otherwise.</returns>
        public bool Equals(Quaternion other, float epsilon)
        {
            return (Math.Abs(this.x - other.x) < epsilon) &&
                   (Math.Abs(this.y - other.y) < epsilon) &&
                   (Math.Abs(this.z - other.z) < epsilon) &&
                   (Math.Abs(this.w - other.w) < epsilon);
        }

        /// <summary>
        /// Returns a hash code.
        /// </summary>
        /// <returns>
        /// A hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        }
    }
}
