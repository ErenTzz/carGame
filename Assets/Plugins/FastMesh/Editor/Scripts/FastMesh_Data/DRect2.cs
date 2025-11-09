using System;
using System.Globalization;

namespace FastMesh
{
    public struct DRect2
    { 
        private double m_XMin;
        private double m_YMin;
        private double m_Width;
        private double m_Height;

        public DRect2(double x, double y, double width, double height)
        {
          this.m_XMin = x;
          this.m_YMin = y;
          this.m_Width = width;
          this.m_Height = height;
        }

        public DRect2(DVector2 position, DVector2 size)
        {
          this.m_XMin = position.x;
          this.m_YMin = position.y;
          this.m_Width = size.x;
          this.m_Height = size.y;
        }

        public DRect2(DRect2 source)
        {
          this.m_XMin = source.m_XMin;
          this.m_YMin = source.m_YMin;
          this.m_Width = source.m_Width;
          this.m_Height = source.m_Height;
        }

        public static DRect2 zero => new DRect2(0.0, 0.0, 0.0, 0.0);
        public static DRect2 full => new DRect2(0.0, 0.0, Config.PIXEL_THS, Config.PIXEL_THS);

        
        
        
        
        
        
        
        
        
        
        public static DRect2 MinMaxRect(double xmin, double ymin, double xmax, double ymax) => new DRect2(xmin, ymin, xmax - xmin, ymax - ymin);

        public void Set(double x, double y, double width, double height)
        {
          this.m_XMin = x;
          this.m_YMin = y;
          this.m_Width = width;
          this.m_Height = height;
        }

        
        
        
        public double x
        {
          get => this.m_XMin;
          set => this.m_XMin = value;
        }

        
        
        
        public double y
        {
          get => this.m_YMin;
          set => this.m_YMin = value;
        }

        
        
        
        public DVector2 position
        {
          get => new DVector2(this.m_XMin, this.m_YMin);
          set
          {
            this.m_XMin = value.x;
            this.m_YMin = value.y;
          }
        }

        
        
        
        public DVector2 center
        {
          get => new DVector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
          set
          {
            this.m_XMin = value.x - this.m_Width / 2f;
            this.m_YMin = value.y - this.m_Height / 2f;
          }
        }

        
        
        
        public DVector2 min
        {
          get => new DVector2(this.xMin, this.yMin);
          set
          {
            this.xMin = value.x;
            this.yMin = value.y;
          }
        }

        
        
        
        public DVector2 max
        {
          get => new DVector2(this.xMax, this.yMax);
          set
          {
            this.xMax = value.x;
            this.yMax = value.y;
          }
        }

        
        
        
        public double width
        {
          get => this.m_Width;
          set => this.m_Width = value;
        }

        
        
        
        public double height
        {
          get => this.m_Height;
          set => this.m_Height = value;
        }

        
        
        
        public DVector2 size
        {
          get => new DVector2(this.m_Width, this.m_Height);
          set
          {
            this.m_Width = value.x;
            this.m_Height = value.y;
          }
        }

        
        
        
        public double xMin
        {
          get => this.m_XMin;
          set
          {
            double xMax = this.xMax;
            this.m_XMin = value;
            this.m_Width = xMax - this.m_XMin;
          }
        }

        
        
        
        public double yMin
        {
          get => this.m_YMin;
          set
          {
            double yMax = this.yMax;
            this.m_YMin = value;
            this.m_Height = yMax - this.m_YMin;
          }
        }

        
        
        
        public double xMax
        {
          get => this.m_Width + this.m_XMin;
          set => this.m_Width = value - this.m_XMin;
        }

        
        
        
        public double yMax
        {
          get => this.m_Height + this.m_YMin;
          set => this.m_Height = value - this.m_YMin;
        }

        
        
        
        
        
        
        
        
        public bool Contains(DVector2 point) => point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;

        
        
        
        
        
        
        
        
        public bool Contains(DVector3 point) => point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;

        
        
        
        
        
        
        
        
        public bool Contains(DVector3 point, bool allowInverse) => !allowInverse ? this.Contains(point) : 
          (this.width < 0.0 && point.x <= this.xMin && point.x > this.xMax || this.width >= 0.0 && point.x >= this.xMin && point.x < this.xMax) 
          & (this.height < 0.0 && point.y <= this.yMin && point.y > this.yMax || this.height >= 0.0 && point.y >= this.yMin && point.y < this.yMax);

        private static DRect2 OrderMinMax(DRect2 rect)
        {
          if (rect.xMin > rect.xMax)
          {
            (rect.xMin, rect.xMax) = (rect.xMax, rect.xMin);
          }
          if (rect.yMin > rect.yMax)
          {
            (rect.yMin, rect.yMax) = (rect.yMax, rect.yMin);
          }
          return rect;
        }

        
        
        
        
        
        public bool Overlaps(DRect2 other) => other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;

        
        
        
        
        
        public bool Overlaps(DRect2 other, bool allowInverse)
        {
          DRect2 rect = this;
          if (allowInverse)
          {
            rect = DRect2.OrderMinMax(rect);
            other = DRect2.OrderMinMax(other);
          }
          return rect.Overlaps(other);
        }

        
        
        
        
        
        
        
        
        
        
        

        
        
        
        
        
        

        public static bool operator !=(DRect2 lhs, DRect2 rhs) => !(lhs == rhs);

        public static bool operator ==(DRect2 lhs, DRect2 rhs) =>lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;

        public override int GetHashCode()
        {
          double num1 = this.x;
          int hashCode = num1.GetHashCode();
          num1 = this.width;
          int num2 = num1.GetHashCode() << 2;
          int num3 = hashCode ^ num2;
          num1 = this.y;
          int num4 = num1.GetHashCode() >> 2;
          int num5 = num3 ^ num4;
          num1 = this.height;
          int num6 = num1.GetHashCode() >> 1;
          return num5 ^ num6;
        }

        public override bool Equals(object other) => other is DRect2 other1 && this.Equals(other1);

        public bool Equals(DRect2 other)
        {
          int num1;
          if (this.x.Equals(other.x))
          {
            double num2 = this.y;
            if (num2.Equals(other.y))
            {
              num2 = this.width;
              if (num2.Equals(other.width))
              {
                num2 = this.height;
                num1 = num2.Equals(other.height) ? 1 : 0;
                goto label_5;
              }
            }
          }
          num1 = 0;
    label_5:
          return num1 != 0;
        }

        
        
        
        
        
        public override string ToString() => this.ToString((string) null, (IFormatProvider) null);

        
        
        
        
        
        public string ToString(string format) => this.ToString(format, (IFormatProvider) null);

        
        
        
        
        
        public string ToString(string format, IFormatProvider formatProvider)
        {
          if (string.IsNullOrEmpty(format))
            format = "F2";
          if (formatProvider == null)
            formatProvider = (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat;
          object[] objArray = new object[4]
          {
            (object) this.x.ToString(format, formatProvider),
            null,
            null,
            null
          };
          double num = this.y;
          objArray[1] = (object) num.ToString(format, formatProvider);
          num = this.width;
          objArray[2] = (object) num.ToString(format, formatProvider);
          num = this.height;
          objArray[3] = (object) num.ToString(format, formatProvider);
          return UnityString1.Format("(x:{0}, y:{1}, width:{2}, height:{3})", objArray);
        }

        [Obsolete("use xMin")]
        public double left => this.m_XMin;

        [Obsolete("use xMax")]
        public double right => this.m_XMin + this.m_Width;

        [Obsolete("use yMin")]
        public double top => this.m_YMin;

        [Obsolete("use yMax")]
        public double bottom => this.m_YMin + this.m_Height;
    }
}