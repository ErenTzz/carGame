using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FastMesh
{
    public struct DVector2 : IFormattable
    {
        public double x;
        public double y;
        
        private static readonly DVector2 zeroVector = new DVector2(0.0f, 0.0f);
        private static readonly DVector2 oneVector = new DVector2(1f, 1f);
        private static readonly DVector2 upVector = new DVector2(0.0f, 1f);
        private static readonly DVector2 downVector = new DVector2(0.0f, -1f);
        private static readonly DVector2 leftVector = new DVector2(-1f, 0.0f);
        private static readonly DVector2 rightVector = new DVector2(1f, 0.0f);
        private static readonly DVector2 positiveInfinityVector = new DVector2(double.PositiveInfinity, double.PositiveInfinity);
        private static readonly DVector2 negativeInfinityVector = new DVector2(double.NegativeInfinity, double.NegativeInfinity);
        public const double kEpsilon = 1E-05;
        public const double kEpsilonNormalSqrt = 1E-15;

        public double this[int index]
        {
          [MethodImpl((MethodImplOptions) 256)] get
          {
            switch (index)
            {
              case 0:
                return this.x;
              case 1:
                return this.y;
              default:
                throw new IndexOutOfRangeException("Invalid DVector2 index!");
            }
          }
          [MethodImpl((MethodImplOptions) 256)] set
          {
            switch (index)
            {
              case 0:
                this.x = value;
                break;
              case 1:
                this.y = value;
                break;
              default:
                throw new IndexOutOfRangeException("Invalid DVector2 index!");
            }
          }
        }

        [MethodImpl((MethodImplOptions) 256)]
        public DVector2(double x, double y)
        {
          this.x = x;
          this.y = y;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public void Set(double newX, double newY)
        {
          this.x = newX;
          this.y = newY;
        }
        
        public Vector2 vector2
        {
          [MethodImpl((MethodImplOptions) 256)] get => new Vector2((float)this.x, (float)this.y);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Lerp(DVector2 a, DVector2 b, double t)
        {
          t = Clamp01(t);
          return new DVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double Clamp01(double value)
        {
          if (value < 0.0) return 0.0;
          return value > 1.0 ? 1 : value;
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 LerpUnclamped(DVector2 a, DVector2 b, double t) => new DVector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 MoveTowards(DVector2 current, DVector2 target, double maxDistanceDelta)
        {
          double num1 = target.x - current.x;
          double num2 = target.y - current.y;
          double d =  (num1 * num1 + num2 * num2);
          if (d == 0.0 || maxDistanceDelta >= 0.0 && d <= maxDistanceDelta * maxDistanceDelta) return target;
          double num3 = Math.Sqrt(d);
          return new DVector2(current.x + num1 / num3 * maxDistanceDelta, current.y + num2 / num3 * maxDistanceDelta);
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Scale(DVector2 a, DVector2 b) => new DVector2(a.x * b.x, a.y * b.y);
        
        [MethodImpl((MethodImplOptions) 256)]
        public void Scale(DVector2 scale)
        {
          this.x *= scale.x;
          this.y *= scale.y;
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public void Normalize()
        {
          double magnitude1 = this.magnitude;
          if (magnitude1 > 9.99999974737875E-06)
            this = this / magnitude1;
          else
            this = DVector2.zero;
        }

        public DVector2 normalized
        {
          [MethodImpl((MethodImplOptions) 256)] get
          {
            DVector2 normalized = new DVector2(this.x, this.y);
            normalized.Normalize();
            return normalized;
          }
        }

        public override string ToString() => this.ToString((string) null, (IFormatProvider) null);
        public string ToString(string format) => this.ToString(format, (IFormatProvider) null);
        public string ToString(string format, IFormatProvider formatProvider)
        {
          if (string.IsNullOrEmpty(format))
            format = "F4";
          if (formatProvider == null)
            formatProvider = (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat;
          return UnityString1.Format("({0}, {1})", (object) this.x.ToString(format, formatProvider), (object) this.y.ToString(format, formatProvider));
        }

        [MethodImpl((MethodImplOptions) 256)]
        public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2;

        [MethodImpl((MethodImplOptions) 256)]
        public override bool Equals(object other) => other is DVector2 other1 && this.Equals(other1);

        [MethodImpl((MethodImplOptions) 256)]
        public bool Equals(DVector2 other) => this.x == other.x && this.y == other.y;

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Reflect(DVector2 inDirection, DVector2 inNormal)
        {
          double num = -2f * DVector2.Dot(inNormal, inDirection);
          return new DVector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
        }

        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Perpendicular(DVector2 inDirection) => new DVector2(-inDirection.y, inDirection.x);
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Perpendicular_poChasovoj(DVector2 inDirection) => new DVector2(inDirection.y, -inDirection.x);

        [MethodImpl((MethodImplOptions) 256)]
        public static double Dot(DVector2 lhs, DVector2 rhs) =>(lhs.x * rhs.x + lhs.y * rhs.y);

        public double magnitude
        {
          [MethodImpl((MethodImplOptions) 256)] get => Math.Sqrt(this.x * this.x + this.y * this.y);
        }
        public double sqrMagnitude
        {
          [MethodImpl((MethodImplOptions) 256)] get => (this.x * this.x + this.y * this.y);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static double Angle(DVector2 from, DVector2 to)
        {
          double num = Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
          return num < 1.00000000362749E-15 ? 0.0 : Math.Acos(Clamp(DVector2.Dot(from, to) / num, -1, 1)) * 57.29578;
        }
        [MethodImpl((MethodImplOptions) 256)]
        public static double Clamp(double value, double min, double max)
        {
          if (value < min) value = min; else if (value > max) value = max;
          return value;
        }
        [MethodImpl((MethodImplOptions) 256)]
        public static double SignedAngle(DVector2 from, DVector2 to) => 
          DVector2.Angle(from, to) * Sign((from.x * to.y - from.y * to.x));

        [MethodImpl((MethodImplOptions) 256)]
        public static double Sign(double f) => f >= 0.0 ? 1 : -1;
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double Distance(DVector2 a, DVector2 b)
        {
          double num1 = a.x - b.x;
          double num2 = a.y - b.y;
          return Math.Sqrt(num1 * num1 + num2 * num2);
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 ClampMagnitude(DVector2 vector, double maxLength)
        {
          double sqrMagnitude = vector.sqrMagnitude;
          if (sqrMagnitude <= maxLength * maxLength)
            return vector;
          double num1 = Math.Sqrt(sqrMagnitude);
          double num2 = vector.x / num1;
          double num3 = vector.y / num1;
          return new DVector2(num2 * maxLength, num3 * maxLength);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static double SqrMagnitude(DVector2 a) => (a.x * a.x + a.y * a.y);

        [MethodImpl((MethodImplOptions) 256)]
        public double SqrMagnitude() => (this.x * this.x + this.y * this.y);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Min(DVector2 lhs, DVector2 rhs) => new DVector2(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 Max(DVector2 lhs, DVector2 rhs) => new DVector2(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));

        
        
        
        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator +(DVector2 a, DVector2 b) => new DVector2(a.x + b.x, a.y + b.y);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator -(DVector2 a, DVector2 b) => new DVector2(a.x - b.x, a.y - b.y);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator *(DVector2 a, DVector2 b) => new DVector2(a.x * b.x, a.y * b.y);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator /(DVector2 a, DVector2 b) => new DVector2(a.x / b.x, a.y / b.y);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator -(DVector2 a) => new DVector2(-a.x, -a.y);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator *(DVector2 a, double d) => new DVector2(a.x * d, a.y * d);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator *(double d, DVector2 a) => new DVector2(a.x * d, a.y * d);

        [MethodImpl((MethodImplOptions) 256)]
        public static DVector2 operator /(DVector2 a, double d) => new DVector2(a.x / d, a.y / d);

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(DVector2 lhs, DVector2 rhs)
        {
          double num1 = lhs.x - rhs.x;
          double num2 = lhs.y - rhs.y;
          return num1 * num1 + num2 * num2 < 9.99999943962493E-11;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(DVector2 lhs, DVector2 rhs) => !(lhs == rhs);

        [MethodImpl((MethodImplOptions) 256)]
        public static implicit operator DVector2(DVector3 v) => new DVector2(v.x, v.y);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static implicit operator DVector3(DVector2 v) => new DVector3(v.x, v.y, 0.0f);

        public static DVector2 zero
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.zeroVector;
        }
        public static DVector2 one
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.oneVector;
        }
        public static DVector2 up
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.upVector;
        }
        public static DVector2 down
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.downVector;
        }
        public static DVector2 left
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.leftVector;
        }
        public static DVector2 right
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.rightVector;
        }
        public static DVector2 positiveInfinity
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.positiveInfinityVector;
        }
        public static DVector2 negativeInfinity
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector2.negativeInfinityVector;
        }
    }
}