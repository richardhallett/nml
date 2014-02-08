using System;
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
        /// The axis component of the quaternion as a <see cref="Vector3"/> 
        /// </summary>
        public Vector3 xyz { get { return new Vector3(x, y, z); } }

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
        /// Transform a <see cref="Vector3"/> by this quaternion.
        /// </summary>
        /// <param name="vec">A vector to rotate.</param>
        /// <returns>The resulting <see cref="Vector3"/> transformed by this quaternion</returns>
        public Vector3 Transform(Vector3 vec)
        {
            // Local variables for faster lookup.
            Vector3 xyz = this.xyz;
            float w = this.w;

            var t = Vector3.Cross(xyz, vec) * 2;
            var r = vec + (t * w) + Vector3.Cross(xyz, t);

            return r;
        }

        /// <summary>
        /// Get a rotation quaternion around the specified axis by the desired angle in radians.
        /// </summary>
        /// <param name="axis">Normalised axis vector.</param>
        /// <param name="angle">Angle in radians.</param>
        /// <returns></returns>
        public static Quaternion RotateAxis(Vector3 axis, float angle)
        {
            angle *= 0.5f;
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            return new Quaternion(axis.x * sin, axis.y * sin, axis.z * sin, cos);
        }

        /// <summary>
        /// Get a rotation quaternion around the specifieid euler angles.
        /// </summary>
        /// <param name="yaw">Yaw in radians.</param>
        /// <param name="pitch">Pitch in radians.</param>
        /// <param name="roll">Roll in radians.</param>
        /// <returns></returns>
        public static Quaternion RotateEuler(float yaw, float pitch, float roll)
        {
            float hYaw = yaw * 0.5f;
            float hPitch = pitch * 0.5f;            
            float hRoll = roll * 0.5f;

            float sinRoll = (float)System.Math.Sin(hPitch);
            float cosRoll = (float)System.Math.Cos(hPitch);
            float sinPitch = (float)System.Math.Sin(hPitch);
            float cosPitch = (float)System.Math.Cos(hPitch);
            float sinYaw = (float)System.Math.Sin(hYaw);
            float cosYaw = (float)System.Math.Cos(hYaw);

            return new Quaternion(
                    (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
                    (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
                    (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll),
                    (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll)
            );                       
        }

        /// <summary>
        /// Get a vector representation of the axis and angle of a quaternion.
        /// </summary>
        /// <param name="quat">The quaternion we want to get the axis and angle from.</param>
        /// <returns>A vector4 representing the axis and angle.</returns>
        public static Vector4 GetAxisAngle(Quaternion quat)
        {
            quat.Normalise();
            float x = quat.x;
            float y = quat.y;
            float z = quat.z;
            float w = quat.w;

            float angle = 2.0f * (float)System.Math.Acos(w);
            float sin = (float)System.Math.Sin(angle * 0.5f) ;

            if (System.Math.Abs(sin) > 1e-4f)
            {
                float sin1 = 1.0f / sin;
                return new Vector4(x * sin1, y * sin1, z * sin1, angle);
            }
            else
            {
                // The angle is too close to 0 to be able to be represented, therefore to avoid blowing up we just return a Unit Vector.
                return Vector4.UnitX;
            }                       
        }        

        /// <summary>
        /// Get a vector representation of the axis and angle of this quaternion.
        /// </summary>
        /// <returns>A vector4 representing the axis and angle.</returns>
        public Vector4 GetAxisAngle()
        {
            return Quaternion.GetAxisAngle(this);
        }

        /// <summary>
        /// Get a rotation <see cref="Matrix4" /> representing a quaternion.
        /// Note: The quaternion must be a unit length quaternion, the results are undefined otherwise.
        /// </summary>
        /// <param name="quat">A normalised quaternion.</param>
        /// <returns></returns>
        public static Matrix4 GetMatrix4(Quaternion quat)
        {            
            float x = quat.x;
            float y = quat.y;
            float z = quat.z;
            float w = quat.w;

            float x2 = x * x;
	        float y2 = y * y;
	        float z2 = z * z;
	        float xy = x * y;
	        float xz = x * z;
	        float yz = y * z;
	        float wx = w * x;
	        float wy = w * y;
	        float wz = w * z;

            return new Matrix4(new float[] { 1.0f - 2.0f * (y2 + z2),     2.0f * (xy - wz),           2.0f * (xz + wy),           0.0f,
                                             2.0f * (xy + wz),            1.0f - 2.0f * (x2 + z2),    2.0f * (yz - wx),           0.0f,
                                             2.0f * (xz - wy),            2.0f * (yz + wx),           1.0f - 2.0f * (x2 + y2),    0.0f,
                                             0.0f,                        0.0f,                       0.0f,                       1.0f }
            );
        }

        /// <summary>
        /// Get a rotation <see cref="Matrix4" /> representing this quaternion.
        /// Note: The quaternion must be a unit length quaternion, the results are undefined otherwise.
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetMatrix4()
        {
            return Quaternion.GetMatrix4(this);
        }
       
        /// <summary>
        /// Transform a normalised <see cref="Vector3"/> by this quaternion.
        /// </summary>
        /// <param name="vec">A normalised vector to rotate.</param>
        /// <returns>The resulting <see cref="Vector3"/> transformed by this quaternion</returns>
        public static Vector3 operator *(Quaternion quat, Vector3 vec)
        {
            return quat.Transform(vec);
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
        /// Returns the components of a quaternion in an array.
        /// </summary>
        /// <returns>An array with the components of the quaternion.</returns>
        public float[] ToArray()
        {
            return new float[] { x, y, z, w };
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
