using System.Collections.Generic;

namespace FastMesh
{
    public class Ser_M2Side
    {
        
        public bool draw_isDraw = false;
        public List<Ser_M2Segment> draw_listSerM2Segment = new List<Ser_M2Segment>();

        
        public bool vremjnka_isDraw = false;
        public List<Ser_M2Segment> vremjnka_listSerM2Segment = new List<Ser_M2Segment>();

        public TypeAxis typeAxis;
        public DRect2 zonaDrowTHSNIZ;

        public void Clear()
        {
            draw_isDraw = false;
            draw_listSerM2Segment.Clear();
            vremjnka_isDraw = false;
            vremjnka_listSerM2Segment.Clear();
            typeAxis = TypeAxis.not;
            zonaDrowTHSNIZ = DRect2.zero;
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}