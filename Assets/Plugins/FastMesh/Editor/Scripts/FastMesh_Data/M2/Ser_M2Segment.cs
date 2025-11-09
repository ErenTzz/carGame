using System;
using UnityEngine.Serialization;

namespace FastMesh
{
    [Serializable]
    public struct Ser_M2Segment
    {
        public Ser_M2Point p1;
        public Ser_M2Point p2;
        
        public Ser_M2Point sA;
        public Ser_M2Point sB;
        public Ser_Guid g;
        
        
        
        
        
        
        
        
        
        public Ser_M2Segment(Ser_M2Point p1, Ser_M2Point p2, Ser_M2Point sA, Ser_M2Point sB, Ser_Guid g)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.sA = sA;
            this.sB = sB;
            this.g = g;
        }
    }
}