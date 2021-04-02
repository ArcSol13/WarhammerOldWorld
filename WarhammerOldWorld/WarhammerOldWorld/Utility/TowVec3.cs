using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace WarhammerOldWorld.Utility
{
    [Serializable]
    public class TowVec3
    {
        private Vec3 innerVec;

        public TowVec3(Vector3 vector3)
        {
            innerVec = new Vec3(vector3.X, vector3.Y, vector3.Z, -1f);
        }

        public TowVec3(Vec3 c, float w = -1f)
        {
            innerVec = new Vec3(c, w);
        }

        public TowVec3(Vec2 xy, float z = 0f, float w = -1f)
        {
            innerVec = new Vec3(xy, z, w);
        }

        public TowVec3(float x = 0f, float y = 0f, float z = 0f, float w = -1f)
        {
            innerVec = new Vec3(x, y, z, w);
        }

        public static readonly TowVec3 Side = new TowVec3(Vec3.Side);
        public static readonly TowVec3 Forward = new TowVec3(Vec3.Forward);
        public static readonly TowVec3 Up = new TowVec3(Vec3.Up);
        public static readonly TowVec3 One = new TowVec3(Vec3.One);
        public static readonly TowVec3 Zero = new TowVec3(Vec3.Zero);
        public static readonly TowVec3 Invalid = new TowVec3(Vec3.Invalid);
        [XmlAttribute]
        public float x;
        [XmlAttribute]
        public float y;
        [XmlAttribute]
        public float z;
        [XmlAttribute]
        public float w;

        public float this[int i]
        {
            get
            {
                return innerVec[i];
            }
            set
            {
                innerVec[i] = value;
            }
        }

        public float X { get { return innerVec.X; } }
        public uint ToARGB { get { return innerVec.ToARGB; } }
        public Vec2 AsVec2 { get { return innerVec.AsVec2; } set { innerVec.AsVec2 = value; } }
        public bool IsNonZero { get { return innerVec.IsNonZero; } }
        public bool IsUnit { get { return innerVec.IsUnit; } }
        public bool IsValidXYZW { get { return innerVec.IsValidXYZW; } }
        public bool IsValid { get { return innerVec.IsValid; } }
        public float LengthSquared { get { return innerVec.LengthSquared; } }
        public float Length { get { return innerVec.Length; } }
        public float Z { get { return innerVec.Z; } }
        public float Y { get { return innerVec.Y; } }
        public float RotationZ { get { return innerVec.RotationZ; } }
        public float RotationX { get { return innerVec.RotationX; } }

        public static TowVec3 Abs(TowVec3 vec)
        {
            return new TowVec3(Vec3.Abs(vec.innerVec));
        }
        public static float AngleBetweenTwoVectors(TowVec3 v1, TowVec3 v2)
        {
            return Vec3.AngleBetweenTwoVectors(v1.innerVec, v2.innerVec);
        }
        public static TowVec3 CrossProduct(TowVec3 va, TowVec3 vb)
        {
            return new TowVec3(Vec3.CrossProduct(va.innerVec, vb.innerVec));
        }
        public static float DotProduct(TowVec3 v1, TowVec3 v2)
        {
            return Vec3.DotProduct(v1.innerVec, v2.innerVec);
        }
        public static TowVec3 Lerp(TowVec3 v1, TowVec3 v2, float alpha)
        {
            return new TowVec3(Vec3.Lerp(v1.innerVec, v2.innerVec, alpha));
        }
        public static TowVec3 Parse(string input)
        {
            return new TowVec3(Vec3.Parse(input));
        }
        public static TowVec3 Slerp(TowVec3 start, TowVec3 end, float percent)
        {
            return new TowVec3(Vec3.Slerp(start.innerVec, end.innerVec, percent));
        }
        public static TowVec3 Vec3Max(TowVec3 v1, TowVec3 v2)
        {
            return new TowVec3(Vec3.Vec3Max(v1.innerVec, v2.innerVec));
        }
        public static TowVec3 Vec3Min(TowVec3 v1, TowVec3 v2)
        {
            return new TowVec3(Vec3.Vec3Min(v1.innerVec, v2.innerVec));
        }
        public TowVec3 ClampedCopy(float min, float max)
        {
            return new TowVec3(innerVec.ClampedCopy(min, max));
        }
        public TowVec3 ClampedCopy(float min, float max, out bool valueClamped)
        {
            return new TowVec3(innerVec.ClampedCopy(min, max, out valueClamped));
        }
        public float Distance(TowVec3 v)
        {
            return innerVec.Distance(v.innerVec);
        }
        public float DistanceSquared(TowVec3 v)
        {
            return innerVec.DistanceSquared(v.innerVec);
        }
        public override bool Equals(object obj)
        {
            return innerVec.Equals(obj);
        }
        public override int GetHashCode()
        {
            return innerVec.GetHashCode();
        }
        public bool NearlyEquals(TowVec3 v, float epsilon = 1E-05F)
        {
            return innerVec.NearlyEquals(v.innerVec, epsilon);
        }
        public float Normalize()
        {
            return innerVec.Normalize();
        }
        public TowVec3 NormalizedCopy()
        {
            return new TowVec3(innerVec.NormalizedCopy());
        }
        public void NormalizeWithoutChangingZ()
        {
            innerVec.NormalizeWithoutChangingZ();
        }
        public TowVec3 ProjectOnUnitVector(TowVec3 ov)
        {
            return new TowVec3(innerVec.ProjectOnUnitVector(ov.innerVec));
        }
        public TowVec3 Reflect(TowVec3 normal)
        {
            return new TowVec3(innerVec.Reflect(normal.innerVec));
        }
        public TowVec3 RotateAboutAnArbitraryVector(TowVec3 vec, float a)
        {
            return new TowVec3(innerVec.RotateAboutAnArbitraryVector(vec.innerVec, a));
        }
        public void RotateAboutX(float a)
        {
            innerVec.RotateAboutX(a);
        }
        public void RotateAboutY(float a)
        {
            innerVec.RotateAboutY(a);
        }
        public void RotateAboutZ(float a)
        {
            innerVec.RotateAboutZ(a);
        }
        public override string ToString()
        {
            return innerVec.ToString();
        }

        public static TowVec3 operator +(TowVec3 v1, TowVec3 v2)
        {
            return new TowVec3(v1.innerVec + v2.innerVec);
        }
        public static TowVec3 operator -(TowVec3 v)
        {
            return new TowVec3(-v.innerVec);
        }
        public static TowVec3 operator -(TowVec3 v1, TowVec3 v2)
        {
            return new TowVec3(v1.innerVec - v2.innerVec);
        }
        public static TowVec3 operator *(TowVec3 v, float f)
        {
            return new TowVec3(v.innerVec * f);
        }
        public static TowVec3 operator *(TowVec3 v, MatrixFrame frame)
        {
            return new TowVec3(v.innerVec * frame);
        }
        public static TowVec3 operator *(float f, TowVec3 v)
        {
            return new TowVec3(f * v.innerVec);
        }
        public static TowVec3 operator /(TowVec3 v, float f)
        {
            return new TowVec3(v.innerVec / f);
        }
        public static bool operator ==(TowVec3 v1, TowVec3 v2)
        {
            return v1.innerVec == v2.innerVec;
        }
        public static bool operator !=(TowVec3 v1, TowVec3 v2)
        {
            return v1.innerVec != v2.innerVec;
        }

        public static explicit operator Vector3(TowVec3 v)
        {
            return new Vector3(v.innerVec.x, v.innerVec.y, v.innerVec.z);
        }
    }
}
