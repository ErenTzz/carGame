using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastMesh
{
    public class LayerClass_Master : LayerClass
    {
        public List<Ser_M2Segment> listM2Segment_X = new List<Ser_M2Segment>();
        public List<Ser_M2Segment> listM2Segment_Y = new List<Ser_M2Segment>();
        public List<Ser_M2Segment> listM2Segment_Z = new List<Ser_M2Segment>();
        
        
        public TypeAxis typeAxisSide1 = TypeAxis.not;
        public TypeAxis typeAxisSide2 = TypeAxis.not;
        public double pixelDefaultVremjnkaFrom = Config.PIXEL_DEFAULT_VREMJNKA_FROM;
        public double pixelDefaultVremjnkaTo = Config.PIXEL_DEFAULT_VREMJNKA_TO;
        
        public List<Ser_M3Polygon> listM3Polygon = new List<Ser_M3Polygon>();
        
        
        

        public LayerClass_Master()
        {
            typeLayer = TypeLayer.Master;
        }
        
        public override void ClearAll()
        {
            base.ClearAll();
            Clear_onlyMaster();
        }
        public void Clear_onlyMaster()
        {
            listM2Segment_X.Clear();
            listM2Segment_Y.Clear();
            listM2Segment_Z.Clear();
            typeAxisSide1 = TypeAxis.not;
            typeAxisSide2 = TypeAxis.not;
            pixelDefaultVremjnkaFrom = Config.PIXEL_DEFAULT_VREMJNKA_FROM;
            pixelDefaultVremjnkaTo = Config.PIXEL_DEFAULT_VREMJNKA_TO;
            listM3Polygon.Clear();
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        public override void Copy(LayerData LayerDataFrom)
        {
            base.Copy(LayerDataFrom);
            
            if (LayerDataFrom.typeLayer != TypeLayer.Master)
                throw new ArgumentOutOfRangeException(nameof(TypeLayer));
            var LayerDataFromMaster = (LayerData_Master) LayerDataFrom;
            
            listM2Segment_X.AddRange(LayerDataFromMaster.listM2Segment_X);
            listM2Segment_Y.AddRange(LayerDataFromMaster.listM2Segment_Y);
            listM2Segment_Z.AddRange(LayerDataFromMaster.listM2Segment_Z);

            typeAxisSide1 = LayerDataFromMaster.typeAxisSide1;
            typeAxisSide2 = LayerDataFromMaster.typeAxisSide2;
            pixelDefaultVremjnkaFrom = LayerDataFromMaster.pixelDefaultVremjnkaFrom;
            pixelDefaultVremjnkaTo = LayerDataFromMaster.pixelDefaultVremjnkaTo;
            
            listM3Polygon.AddRange(LayerDataFromMaster.listM3Polygon);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}