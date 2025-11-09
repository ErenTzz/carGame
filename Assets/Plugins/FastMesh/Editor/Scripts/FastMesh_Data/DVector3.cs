using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FastMesh
{
    public struct DVector3 : IFormattable
    {
        
        
        public double x;
        public double y;
        public double z;
        private static readonly DVector3 zeroVector = new DVector3(0.0, 0.0, 0.0);
        private static readonly DVector3 oneVector = new DVector3(1, 1, 1);
        private static readonly DVector3 upVector = new DVector3(0.0, 1, 0.0);
        private static readonly DVector3 downVector = new DVector3(0.0, -1, 0.0);
        private static readonly DVector3 leftVector = new DVector3(-1, 0.0, 0.0);
        private static readonly DVector3 rightVector = new DVector3(1, 0.0, 0.0);
        private static readonly DVector3 forwardVector = new DVector3(0.0, 0.0, 1);
        private static readonly DVector3 backVector = new DVector3(0.0, 0.0, -1);
        private static readonly DVector3 vector500 = new DVector3(500.0, 500.0, 500.0);
        private static readonly DVector3 vector001 = new DVector3(0.01, 0.01, 0.01);
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 Lerp(DVector3 a, DVector3 b, double t)
        {
          if (t < 0.0) t = 0.0;
          else t = t > 1.0 ? 1 : t;
          return new DVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public DVector3(double x, double y, double z)
        {
          this.x = x;
          this.y = y;
          this.z = z;
        }
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public DVector3(double x, double y)
        {
          this.x = x;
          this.y = y;
          this.z = 0;
        }
        [MethodImpl((MethodImplOptions) 256)]
        public DVector3(Vector3 vector3)
        {
          this.x = (double) vector3.x;
          this.y = (double) vector3.y;
          this.z = (double) vector3.z;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 Cross(DVector3 lhs, DVector3 rhs) => new DVector3( 
            (lhs.y * rhs.z - lhs.z * rhs.y), 
            (lhs.z * rhs.x - lhs.x * rhs.z), 
              (lhs.x * rhs.y - lhs.y * rhs.x));        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public override bool Equals(object other) => other is DVector3 other1 && this.Equals(other1);
        
        [MethodImpl((MethodImplOptions) 256)]
        public bool Equals(DVector3 other) => this.x == other.x && this.y == other.y && this.z == other.z;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 Normalize(DVector3 value)
        {
          double num = DVector3.Magnitude(value);
          return num > 9.99999974737875E-06 ? value / num : DVector3.zero;
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public void Normalize()
        {
          double num = DVector3.Magnitude(this);
          if (num > 9.99999974737875E-06)
            this = this / num;
          else
            this = DVector3.zero;
        }
        
        
        
        
        public DVector3 normalized
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.Normalize(this);
        }
        public Vector3 vector3
        {
          [MethodImpl((MethodImplOptions) 256)] get => new Vector3((float)this.x, (float)this.y, (float)this.z);
        }
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double Dot(DVector3 lhs, DVector3 rhs) => (lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z);
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 ProjectOnPlane(DVector3 vector, DVector3 planeNormal)
        {
          double num1 = Dot(planeNormal, planeNormal);
          if (num1 < Mathf.Epsilon)
            return vector;
          double num2 = Dot(vector, planeNormal);
          return new DVector3(vector.x - planeNormal.x * num2 / num1, vector.y - planeNormal.y * num2 / num1, vector.z - planeNormal.z * num2 / num1);
        }
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double Angle(DVector3 from, DVector3 to)
        {
          double num = Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
          return num < 1.00000000362749E-15 ? 0.0 : Math.Acos(Clamp(Dot(from, to) / num, -1, 1)) * 57.29578;
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        static double Clamp(double value, double min, double max)
        {
          if (value < min)
            value = min;
          else if (value > max)
            value = max;
          return value;
        }
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double SignedAngle(DVector3 from, DVector3 to, DVector3 axis)
        {
          double num1 = Angle(from, to);
          double num2 = from.y * to.z - from.z * to.y;
          double num3 = from.z * to.x - from.x * to.z;
          double num4 = from.x * to.y - from.y * to.x;
          double num5 = Sign(axis.x * num2 + axis.y * num3 + axis.z * num4);
          return num1 * num5;
        }
        static double Sign(double f) => f >= 0.0 ? 1 : -1;


        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double Distance(DVector3 a, DVector3 b)
        {
          double num1 = a.x - b.x;
          double num2 = a.y - b.y;
          double num3 = a.z - b.z;
          return Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double Magnitude(DVector3 vector) => Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        
        
        
        
        public double magnitude
        {
          [MethodImpl((MethodImplOptions) 256)] get => Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public static double SqrMagnitude(Vector3 vector) =>  vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        
        
        
        
        public double sqrMagnitude
        {
          [MethodImpl((MethodImplOptions) 256)] get => this.x * this.x + this.y * this.y + this.z * this.z;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public static DVector3 zero
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.zeroVector;
        }
        
        
        
        
        public static DVector3 one
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.oneVector;
        }
        
        
        
        
        public static DVector3 forward
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.forwardVector;
        }
        
        
        
        
        public static DVector3 back
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.backVector;
        }       
        
        public static DVector3 v500
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.vector500;
        }        
        public static DVector3 v001
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.vector001;
        }
        
        
        
        
        public static DVector3 up
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.upVector;
        }
        
        
        
        
        public static DVector3 down
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.downVector;
        }
        
        
        
        
        public static DVector3 left
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.leftVector;
        }
        
        
        
        
        public static DVector3 right
        {
          [MethodImpl((MethodImplOptions) 256)] get => DVector3.rightVector;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 operator +(DVector3 a, DVector3 b) => new DVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 operator -(DVector3 a, DVector3 b) => new DVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 operator -(DVector3 a) => new DVector3(-a.x, -a.y, -a.z);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 operator *(DVector3 a, double d) => new DVector3(a.x * d, a.y * d, a.z * d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 operator *(double d, DVector3 a) => new DVector3(a.x * d, a.y * d, a.z * d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static DVector3 operator /(DVector3 a, double d) => new DVector3(a.x / d, a.y / d, a.z / d);
        
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(DVector3 lhs, DVector3 rhs)
        {
          double num1 = lhs.x - rhs.x;
          double num2 = lhs.y - rhs.y;
          double num3 = lhs.z - rhs.z;
          return num1 * num1 + num2 * num2 + num3 * num3 < 9.99999943962493E-11;
        }
        
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(DVector3 lhs, DVector3 rhs) => !(lhs == rhs);
        
        
        
        
        
        
        public override string ToString() => this.ToString((string) null, (IFormatProvider) null);
        
        
        
        
        
        
        public string ToString(string format) => this.ToString(format, (IFormatProvider) null);
        
        
        
        
        
        
        public string ToString(string format, IFormatProvider formatProvider)
        {
          if (string.IsNullOrEmpty(format))
            format = "F10";
          if (formatProvider == null)
            formatProvider = (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat;
          return UnityString1.Format("({0}, {1}, {2})", (object) this.x.ToString(format, formatProvider), (object) this.y.ToString(format, formatProvider), (object) this.z.ToString(format, formatProvider));
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
    
    sealed class UnityString1
    {
      public static string Format(string fmt, params object[] args) => string.Format((IFormatProvider) CultureInfo.InvariantCulture.NumberFormat, fmt, args);
    }
}



