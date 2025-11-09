using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace FastMesh
{
    [Serializable]
    public struct Ser_M3Polygon
    {
        public List<Ser_M3Point> pS;
        
        public Ser_M3Point sA;
        public Ser_M3Point sB;
        public Ser_M3Point sC;
        public Ser_Guid g;
        
        
        
        
        
        
        
        
        
        
        
        

        public Ser_M3Polygon(List<Ser_M3Point> pS, Ser_M3Point sA, Ser_M3Point sB, Ser_M3Point sC, Ser_Guid g)
        {
            this.pS = pS;
            this.sA = sA;
            this.sB = sB;
            this.sC = sC;
            this.g = g;
        }

    }
}