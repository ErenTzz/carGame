using System;

namespace FastMesh
{
    [Serializable]
    public struct Ser_M3Point
    {
        public double x;
        public double y;
        public double z;
        public int kX;
        public int kY;
        public int kZ;

        public Ser_M3Point(double x, double y, double z, int kX, int kY, int kZ)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.kX = kX;
            this.kY = kY;
            this.kZ = kZ;
        }
        
        public Ser_M3Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            kX = (int) Math.Round(x / Config.d_Epsilon);
            kY = (int) Math.Round(y / Config.d_Epsilon);
            kZ = (int) Math.Round(z / Config.d_Epsilon);
        }
        
    }
}