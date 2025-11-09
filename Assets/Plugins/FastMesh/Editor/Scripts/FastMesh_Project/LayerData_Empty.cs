using System;
using UnityEngine;

namespace FastMesh
{
    [Serializable]
    public class LayerData_Empty : LayerData
    {
        

        public override void Init()
        {
            base.Init();
            typeLayer = TypeLayer.Not;
        }
        
        public override bool IsEmpty()
        {
            return false;
        }
    }
}