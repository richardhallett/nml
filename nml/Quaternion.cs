using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nml
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
        public Vector3 xyz
        {
            get { return new Vector3(x, y, z); }
            set
            {
                this.x = value.x;
                this.y = value.y;
                this.z = value.z;
            }
        }

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
        /// Adds two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns>The addition of the two quaternions (a.x+b.x, a.y+b.y, a.z+b.z, a.w+b.w)</returns>
        public static Quaternion Add(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Add(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="result">The addition of the two quaternions (a.x+b.x, a.y+b.y, a.z+b.z, a.w+b.w)</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Quaternion a, ref Quaternion b, out Quaternion result)
        {
            result = new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns>The subtraction of the two quaternions (a.x-b.x, a.y-b.y, a.z-b.z, a.w-b.w)</returns>
        public static Quaternion Subtract(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Subtract(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="result">The subtraction of the two quaternions (a.x-b.x, a.y-b.y, a.z-b.z, a.w-b.w)</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Quaternion a, ref Quaternion b, out Quaternion result)
        {
            result = new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        /// <summary>
        /// Calculates the dot product of two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns>The dot product of the two quaternions (a.x*b.x + a.y*b.y + a.z*b.z + a.w*b.w)</returns>
        public static float Dot(Quaternion a, Quaternion b)
        {
            float result;
            Quaternion.Dot(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Calculates the dot product of two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="result">The dot product of the two quaternions (a.x*b.x + a.y*b.y + a.z*b.z + a.w*b.w)</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Quaternion a, ref Quaternion b, out float result)
        {
            result = (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        /// <summary>
        /// Negate a quaternion so it faces the opposite direction, keeping the same orientation.
        /// Note: Negating a quaternion does not give you the opposite rotation use <see cref="Quaternion.Invert(Nml.Quaternion)"/>.
        /// </summary>
        /// <param name="quat">The quaternion to negate.</param>
        /// <returns>A quaternion facing the opposite direction.</returns>
        public static Quaternion Negate(Quaternion quat)
        {
            return new Quaternion(-quat.x, -quat.y, -quat.z, -quat.w);
        }

        /// <summary>
        /// Negate a quaternion so it faces the opposite direction, keeping the same orientation.
        /// Note: Negating a quaternion does not give you the opposite rotation use <see cref="Quaternion.Invert(Nml.Quaternion)"/>.
        /// </summary>
        /// <param name="quat">The quaternion to negate.</param>
        /// <param name="result">A quaternion facing the opposite direction.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Negate(Quaternion quat, ref Quaternion result)
        {
            result = new Quaternion(-quat.x, -quat.y, -quat.z, -quat.w);
        }

        /// <summary>
        /// Negate this quaternion so it faces the opposite direction, keeping the same orientation.
        /// Note: Negating a quaternion does not give you the opposite rotation use <see cref="Inverse"/>.
        /// </summary>
        public void Negate()
        {
            this = Quaternion.Negate(this);
        }

        /// <summary>
        /// Normalise passed in quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to normalise</param>
        /// <returns>The normalised quaternion.</returns>
        public static Quaternion Normalise(Quaternion quat)
        {
            Quaternion result;
            Normalise(ref quat, out result);
            return result;
        }

        /// <summary>
        /// Normalise passed in quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to normalise</param>
        /// <param name="result">The normalised quaternion.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Normalise(ref Quaternion quat, out Quaternion result)
        {
            float length = quat.LengthSquared;
            if (length > 1e-6f)
            {
                float inv = 1.0f / quat.Length;
                float x = quat.x * inv;
                float y = quat.y * inv;
                float z = quat.z * inv;
                float w = quat.w * inv;

                result = new Quaternion(x, y, z, w);
            }
            else
            {
                // If the length is greater than the tolerance then we just force a return of a unit vector.
                result = new Quaternion(1.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Normalise this quaternion.
        /// </summary>
        public void Normalise()
        {
            Quaternion result;
            Quaternion.Normalise(ref this, out result);
            this = result;
        }

        /// <summary>
        /// Get the conjugate of the passed in quaternion.
        /// </summary>
        /// <param name="quat">A quaternion</param>
        /// <returns>The conjugated quaternion.</returns>
        public static Quaternion Conjugate(Quaternion quat)
        {
            Quaternion result;
            Quaternion.Conjugate(ref quat, out result);
            return result;
        }

        /// <summary>
        /// Get the conjugate of the passed in quaternion.
        /// </summary>
        /// <param name="quat">A quaternion</param>
        /// <param name="result">The conjugated quaternion.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Conjugate(ref Quaternion quat, out Quaternion result)
        {
            float x = -quat.x;
            float y = -quat.y;
            float z = -quat.z;
            float w = quat.w;

            result = new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// Get the conjugate of this quaternion.
        /// </summary>
        public void Conjugate()
        {
            Quaternion result;
            Quaternion.Conjugate(ref this, out result);
            this = result;
        }

        /// <summary>
        /// Inverts the given quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to invert.</param>
        /// <returns>The inverted quaternion.</returns>
        public static Quaternion Invert(Quaternion quat)
        {
            Quaternion result;
            Quaternion.Invert(ref quat, out result);
            return result;
        }

        /// <summary>
        /// Inverts the given quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to invert.</param>
        /// <param name="result">The inverted quaternion.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Invert(ref Quaternion quat, out Quaternion result)
        {
            float inv = 1.0f / quat.LengthSquared;
            float x = -quat.x * inv;
            float y = -quat.y * inv;
            float z = -quat.z * inv;
            float w = quat.w * inv;

            result = new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// Get the inverse of this quaternion.
        /// </summary>
        public void Inverse()
        {
            Quaternion result;
            Quaternion.Invert(ref this, out result);
            this = result;
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
            Quaternion result;
            Quaternion.Multiply(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Multiply two quaternions together (a*b).
        /// Note: Quaternion multiplication is not commutative.
        /// </summary>
        /// <param name="a">First quaternion</param>
        /// <param name="b">Second quaternion</param>
        /// <param name="result">The product of the two quaternions.</param>
        /// 
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Quaternion a, ref Quaternion b, out Quaternion result)
        {
            float ax = a.x;
            float ay = a.y;
            float az = a.z;
            float aw = a.w;
            float bx = b.x;
            float by = b.y;
            float bz = b.z;
            float bw = b.w;

            result = new Quaternion(aw * bx + ax * bw + ay * bz - az * by,
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
            Quaternion result;
            Quaternion.Multiply(ref quaternion, scalar, out result);
            return result;
        }

        /// <summary>
        /// Multiply quaternion components by scalar.
        /// </summary>
        /// <param name="quaternion">Quaternion to scale</param>
        /// <param name="scalar">Amount to scale by</param>
        /// <param name="result">The scaled quaternion.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Quaternion quaternion, float scalar, out Quaternion result)
        {
            result = new Quaternion(quaternion.x * scalar, quaternion.y * scalar, quaternion.z * scalar, quaternion.w * scalar);
        }

        /// <summary>
        /// Transform a <see cref="Vector3"/> by this quaternion.
        /// </summary>
        /// <param name="vec">A vector to rotate.</param>
        /// <returns>The resulting <see cref="Vector3"/> transformed by this quaternion</returns>
        public Vector3 Transform(Vector3 vec)
        {
            Vector3 result;
            Quaternion.Transform(ref this, ref vec, out result);
            return result;
        }

        /// <summary>
        /// Transform a <see cref="Vector3"/> by a quaternion.
        /// </summary>
        /// <param name="quat">A quaternion to transform with.</param>
        /// <param name="vec">A vector to rotate.</param>
        /// <param name="result">The resulting <see cref="Vector3"/> transformed by this quaternion</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Quaternion quat, ref Vector3 vec, out Vector3 result)
        {
            // Local variables for faster lookup.
            Vector3 xyz = quat.xyz;
            float w = quat.w;

            var t = Vector3.Cross(xyz, vec) * 2;
            var r = vec + (t * w) + Vector3.Cross(xyz, t);

            result = r;
        }

        /// <summary>
        /// Get a rotation quaternion around the specified axis by the desired angle in radians.
        /// </summary>
        /// <param name="axis">Normalised axis vector.</param>
        /// <param name="angle">Angle in radians.</param>
        /// <returns>A quaternion representing a rotation.</returns>
        public static Quaternion RotateAxis(Vector3 axis, float angle)
        {
            Quaternion result;
            Quaternion.RotateAxis(ref axis, angle, out result);
            return result;
        }

        /// <summary>
        /// Get a rotation quaternion around the specified axis by the desired angle in radians.
        /// </summary>
        /// <param name="axis">Normalised axis vector.</param>
        /// <param name="angle">Angle in radians.</param>
        /// <param name="result">A quaternion representing a rotation.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void RotateAxis(ref Vector3 axis, float angle, out Quaternion result)
        {
            angle *= 0.5f;
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            result = new Quaternion(axis.x * sin, axis.y * sin, axis.z * sin, cos);
        }

        /// <summary>
        /// Get a rotation quaternion around the specifieid euler angles.
        /// The order of the applied rotation is Yaw first, then pitch and finally roll.
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

            float sinRoll = (float)System.Math.Sin(hRoll);
            float cosRoll = (float)System.Math.Cos(hRoll);
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
            Vector4 result;
            Quaternion.GetAxisAngle(ref quat, out result);
            return result;
        }

        /// <summary>
        /// Get a vector representation of the axis and angle of a quaternion.
        /// </summary>
        /// <param name="quat">The quaternion we want to get the axis and angle from.</param>
        /// <param name="result">A vector4 representing the axis and angle.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void GetAxisAngle(ref Quaternion quat, out Vector4 result)
        {
            quat.Normalise();
            float x = quat.x;
            float y = quat.y;
            float z = quat.z;
            float w = quat.w;

            float angle = 2.0f * (float)System.Math.Acos(w);
            float sin = (float)System.Math.Sin(angle * 0.5f);

            if (System.Math.Abs(sin) > 1e-4f)
            {
                float sin1 = 1.0f / sin;
                result = new Vector4(x * sin1, y * sin1, z * sin1, angle);
            }
            else
            {
                // The angle is too close to 0 to be able to be represented, therefore to avoid blowing up we just return a Unit Vector.
                result = Vector4.UnitX;
            }
        }        

        /// <summary>
        /// Get a vector representation of the axis and angle of this quaternion.
        /// </summary>
        /// <returns>A vector4 representing the axis and angle.</returns>
        public Vector4 GetAxisAngle()
        {
            Vector4 result;
            Quaternion.GetAxisAngle(ref this, out result);
            return result;
        }        

        /// <summary>
        /// Get a rotation <see cref="Matrix4x4" /> representing a quaternion.
        /// Note: The quaternion must be a unit length quaternion, the results are undefined otherwise.
        /// </summary>
        /// <param name="quat">A normalised quaternion.</param>
        /// <returns>A matrix representing the quaternion</returns>
        public static Matrix4x4 GetMatrix4x4(Quaternion quat)
        {
            Matrix4x4 result;
            Quaternion.GetMatrix4x4(ref quat, out result);
            return result;
        }

        /// <summary>
        /// Get a rotation <see cref="Matrix4x4" /> representing a quaternion.
        /// Note: The quaternion must be a unit length quaternion, the results are undefined otherwise.
        /// </summary>
        /// <param name="quat">A normalised quaternion.</param>
        /// <param name="result">A matrix representing the quaternion</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void GetMatrix4x4(ref Quaternion quat, out Matrix4x4 result)
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

            result = new Matrix4x4(new float[] { 1.0f - 2.0f * (y2 + z2),     2.0f * (xy - wz),           2.0f * (xz + wy),           0.0f,
                                                 2.0f * (xy + wz),            1.0f - 2.0f * (x2 + z2),    2.0f * (yz - wx),           0.0f,
                                                 2.0f * (xz - wy),            2.0f * (yz + wx),           1.0f - 2.0f * (x2 + y2),    0.0f,
                                                 0.0f,                        0.0f,                       0.0f,                       1.0f }
            );
        }

        /// <summary>
        /// Get a rotation <see cref="Matrix4x4" /> representing this quaternion.
        /// Note: The quaternion must be a unit length quaternion, the results are undefined otherwise.
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 GetMatrix4x4()
        {
            Matrix4x4 result;
            Quaternion.GetMatrix4x4(ref this, out result);
            return result;
        }

        /// <summary>
        /// Linearly interpolate between two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Quaternion A and 1 is Quaternion B</param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion result;
            Quaternion.Lerp(ref a, ref b, t, out result);
            return result;
        }

        /// <summary>
        /// Linearly interpolate between two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Quaternion A and 1 is Quaternion B</param>
        /// <param name="result">A linear combination: a when t=0 or b when t=1 else a point between.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Lerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion result)
        {
            Quaternion r = new Quaternion();
            r.x = a.x + (b.x - a.x) * t;
            r.y = a.y + (b.y - a.y) * t;
            r.z = a.z + (b.z - a.z) * t;
            r.w = a.w + (b.w - a.w) * t;
            result = r;
        }

        /// <summary>
        /// Linearly interpolate between this and another quaternion.
        /// </summary>
        /// <param name="quat">Quaternion to interpolate with.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is this and 1 is the other Quaternion</param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public Quaternion Lerp(Quaternion quat, float t)
        {
            Quaternion result;
            Quaternion.Lerp(ref this, ref quat, t, out result);
            return result;
        }

        /// <summary>
        /// Linearly interpolate between two quaternions; Then normalise the result;
        /// This is a conveniance function as it is useful and sometimes deseriable to us normalised interpolation, 
        /// this is due to the fact at small differences it can be pretty close to doing spherical interpolation but more performant.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Quaternion A and 1 is Quaternion B</param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>        
        public static Quaternion NLerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion result;
            Quaternion.NLerp(ref a, ref b, t, out result);
            return result;
        }

        /// <summary>
        /// Linearly interpolate between two quaternions; Then normalise the result;
        /// This is a conveniance function as it is useful and sometimes deseriable to us normalised interpolation, 
        /// this is due to the fact at small differences it can be pretty close to doing spherical interpolation but more performant.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Quaternion A and 1 is Quaternion B</param>
        /// <param name="result">A linear combination: a when t=0 or b when t=1 else a point between.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void NLerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion result)
        {
            Quaternion.Lerp(ref a, ref b, t, out result);           
            Quaternion.Normalise(ref result, out result);
        }

        /// <summary>
        /// Linearly interpolate between this and another quaternion; and normalises the result.
        /// This is a conveniance function as it is useful and sometimes deseriable to us normalised interpolation, 
        /// this is due to the fact at small differences it can be pretty close to doing spherical interpolation but more performant.
        /// </summary>
        /// <param name="quat">Quaternion to interpolate with.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is this and 1 is the other Quaternion</param>
        /// <returns>A linear combination: a when t=0 or b when t=1 else a point between.</returns>
        public Quaternion NLerp(Quaternion quat, float t)
        {
            Quaternion result;
            Quaternion.NLerp(ref this, ref quat, t, out result);
            return result;
        }

        /// <summary>
        /// Spherical interpolation between two quaternions.
        /// Note: Spherical interpolation is non commutative Slerp(a,b) is not the same as Slerp(b,a).
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Quaternion A and 1 is Quaternion B</param>
        /// <returns>An interpolated quaternion between the two points.</returns>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion result;
            Quaternion.Slerp(ref a, ref b, t, out result);
            return result;
        }

        /// <summary>
        /// Spherical interpolation between two quaternions.
        /// Note: Spherical interpolation is non commutative Slerp(a,b) is not the same as Slerp(b,a).
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is Quaternion A and 1 is Quaternion B</param>
        /// <param name="result">An interpolated quaternion between the two points.</param>
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public static void Slerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion result)
        {
            // Get the dot product.
            float product;
            Quaternion.Dot(ref a, ref b, out product);

            if (product > 0.9995f)
            {
                // If the values are so close then it's not worth slerping
                // Do normalised lerp instead.
                result = Quaternion.NLerp(a, b, t);
                return;
            }

            // Just in case we slip outside our range let's ensure we clamp to prevent undefined behaviour.
            product = Common.Clamp(product, -1.0f, 1.0f);

            float theta = (float)Math.Acos(product) * t;

            float sin = (float)Math.Sin(theta);
            float cos = (float)Math.Cos(theta);

            Quaternion c = new Quaternion();
            c.x = b.x - (a.x * product);
            c.y = b.y - (a.y * product);
            c.z = b.z - (a.z * product);
            c.w = b.w - (a.w * product);
            c.Normalise();

            float rx = (a.x * cos) + (c.x * sin);
            float ry = (a.y * cos) + (c.y * sin);
            float rz = (a.z * cos) + (c.z * sin);
            float rw = (a.w * cos) + (c.w * sin);

            result = new Quaternion(rx, ry, rz, rw);
        }

        /// <summary>
        /// Spherical interpolation between this and another quaternion.
        /// Note: Spherical interpolation is non commutative Slerp(a,b) is not the same as Slerp(b,a).
        /// </summary>
        /// <param name="quat">Quaternion to interpolate with.</param>
        /// <param name="t">The interpolation weighting applied in the range 0 to 1, where 0 is this and 1 is the other Quaternion</param>
        /// <returns>An interpolated quaternion between the two points.</returns>
        public Quaternion Slerp(Quaternion quat, float t)
        {
            Quaternion result;
            Quaternion.Slerp(ref this, ref quat, t, out result);
            return result;
        }
       
        /// <summary>
        /// Transform a normalised <see cref="Vector3"/> by this quaternion.
        /// </summary>
        /// <param name="quat">The quaternion to transform by.</param>
        /// <param name="vec">A normalised vector to rotate.</param>
        /// <returns>The resulting <see cref="Vector3"/> transformed by this quaternion</returns>
        public static Vector3 operator *(Quaternion quat, Vector3 vec)
        {
            Vector3 result;
            Quaternion.Transform(ref quat, ref vec, out result);
            return result;
        }

        /// <summary>
        /// Multiply two quaternions together (a*b).
        /// Note: Quaternion multiplication is not commutative i.e. (a*b is not the same as b*a)
        /// </summary>
        /// <param name="a">First quaternion</param>
        /// <param name="b">Second quaternion</param>
        /// <returns>The product of the two quaternions.</returns>
        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Multiply(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Multiply quaternion components by scalar.
        /// </summary>
        /// <param name="quaternion">Quaternion to scale</param>
        /// <param name="scalar">Amount to scale by</param>
        /// <returns>The scaled quaternion.</returns>
        public static Quaternion operator *(Quaternion quaternion, float scalar)
        {
            Quaternion result;
            Quaternion.Multiply(ref quaternion, scalar, out result);
            return result;
        }

        /// <summary>
        /// Subtracts two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns>The subtraction of the two quaternions (a.x-b.x, a.y-b.y, a.z-b.z, a.w-b.w)</returns>
        public static Quaternion operator -(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Subtract(ref a, ref b, out result);
            return result;
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="a">First quaternion.</param>
        /// <param name="b">Second quaternion.</param>
        /// <returns>The addition of the two quaternions (a.x+b.x, a.y+b.y, a.z+b.z, a.w+b.w)</returns>
        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Add(ref a, ref b, out result);
            return result;
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
            return base.ToString() + String.Format(": ({0}, {1}, {2}, {3})", this.x, this.y, this.z, this.w);
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
        /// Determines whether the specified <see cref="Quaternion"/> is exactly equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Quaternion"/> to compare with.</param>
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
