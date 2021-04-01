using System;
using System.Numerics;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace WarhammerOldWorld.Utility
{
    [Serializable]
    public struct TowVec2
    {
        private Vec2 innerVec;

        
       // public float X { get { return innerVec.X; } }
        
      //  public float Y { get { return innerVec.Y; } }
        
        public TowVec2(Vector2 vector2)
        {
            innerVec = new Vec2(vector2.X, vector2.Y);
        }

        public TowVec2(Vec2 c)
        {
            innerVec = new Vec2(c);
        }

        public TowVec2(float x, float y)
        {
            innerVec=new Vec2(x, y);
        }
        
        public TowVec3 ToVec3(float z = 0.0f)
        {
            return new TowVec3(innerVec.ToVec3(z));
        } 
        
        public static readonly TowVec2 Side = new TowVec2(Vec2.Side);
        public static readonly TowVec2 Forward = new TowVec2(Vec2.Forward);
        public static readonly TowVec2 One = new TowVec2(Vec2.One);
        public static readonly TowVec2 Zero = new TowVec2(Vec2.Zero);
        public static readonly TowVec2 Invalid = new TowVec2(Vec2.Invalid);
        
        public static explicit operator Vector2(TowVec2 vec2) => new Vector2(vec2.innerVec.x, vec2.innerVec.y);
        
        public static implicit operator TowVec2(Vector2 vec2) => new TowVec2(vec2.X, vec2.Y);
        
        
        public float this[int i]
        {
            get => innerVec[i];
            set => innerVec[i] = value;
        }
        
        public float Normalize()
        {
            return innerVec.Normalize();
        }

        public Vec2 Normalized()
        {
            return innerVec.Normalized();
        }

        public static WindingOrder GetWindingOrder(TowVec2 first, TowVec2 second, TowVec2 third)
        {
           return Vec2.GetWindingOrder(first.innerVec, second.innerVec, third.innerVec);
        }

        public static float CCW(TowVec2 va, TowVec2 vb)
        {
            return Vec2.CCW(va.innerVec, vb.innerVec);
        }


        public float Length
        {
            get { return innerVec.Length; }
        }

        public float LengthSquared => innerVec.LengthSquared;

        public override bool Equals(object obj)
        {
            return innerVec.Equals(obj);
        }

        public override int GetHashCode()
        {
            return innerVec.GetHashCode();
        }

        public static bool operator ==(TowVec2 v1, TowVec2 v2)
        {
            return v1.innerVec == v2.innerVec;
        }
        
        public static bool operator !=(TowVec2 v1, TowVec2 v2)
        {
            return v1.innerVec != v2.innerVec;
        }
        
        
        public static TowVec2 operator +(TowVec2 v1, TowVec2 v2)
        {
            return new TowVec2(v1.innerVec + v2.innerVec);
        }

        public static TowVec2 operator -(TowVec2 v)
        {
            return new TowVec2(-v.innerVec);
        }

        public static TowVec2 operator -(TowVec2 v1, TowVec2 v2)
        {
            return new TowVec2(v1.innerVec - v2.innerVec);
        }

        public static TowVec2 operator *(TowVec2 v, float f)
        {
            return new TowVec2(v.innerVec * f);
        }

        public static TowVec2 operator *(float f, TowVec2 v)
        {
            return new TowVec2(f*v.innerVec);
        }


        
    }
}