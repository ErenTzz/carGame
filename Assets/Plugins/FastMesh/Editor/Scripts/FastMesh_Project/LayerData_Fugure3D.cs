using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastMesh
{
    [Serializable]
    public class LayerData_Fugure3D : LayerData
    {
        

        public List<DVector3> listDVertices = new List<DVector3>();
        public List<int> listTriangles = new List<int>();
        public List<Guid> listGuid = new List<Guid>(); 
        
        public TypeTool typeToolFigure3D = TypeTool.not;
        
        public override void Init()
        {
            base.Init();
            typeLayer = TypeLayer.Fugure3D;
        }
        
        public override void Copy(LayerData LayerDataFrom)
        {
            base.Copy(LayerDataFrom);
            
            if (LayerDataFrom.typeLayer != TypeLayer.Fugure3D)
                throw new ArgumentOutOfRangeException(nameof(TypeLayer));

            var LayerDataFromFugure3D = (LayerData_Fugure3D) LayerDataFrom;

            listDVertices.AddRange(LayerDataFromFugure3D.listDVertices);
            listTriangles.AddRange(LayerDataFromFugure3D.listTriangles);
            listGuid.AddRange(LayerDataFromFugure3D.listGuid);
            typeToolFigure3D = LayerDataFromFugure3D.typeToolFigure3D;
        }
        
        public override bool IsEmpty()
        {
            return false;
        }
    }
}