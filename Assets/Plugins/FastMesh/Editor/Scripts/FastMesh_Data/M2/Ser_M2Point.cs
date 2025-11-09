using System;
using UnityEngine.Serialization;

namespace FastMesh
{
    [Serializable]
    public struct Ser_M2Point
    {
        public double x;
        public double y;
        public int kX;
        public int kY;

        
        
        
        
        
        
        
        
        public Ser_M2Point(double x, double y, int kX, int kY)
        {
            this.x = x;
            this.y = y;
            this.kX = kX;
            this.kY = kY;
        }
        
        public Ser_M2Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            kX = (int) Math.Round(x / Config.d_Epsilon);
            kY = (int) Math.Round(y / Config.d_Epsilon);
        }
        
    }
}