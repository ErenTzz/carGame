using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FastMesh
{ 
    public abstract class LayerClass
    {
        public LayerData base_LayerData;
        
        public TypeLayer typeLayer;

        public byte[] snapshot = Array.Empty<byte>();
        public string nameLayer;

        public bool isModif_mirrorX = false;
        public bool isModif_mirrorY = false;
        public bool isModif_mirrorZ = false;
        
        public bool isModif_bevelX = false;
        public bool isModif_bevelY = false;
        public bool isModif_bevelZ = false;
        public int modif_bevelSize = Config.BEVEL_SIZE_STD;

        public bool isModif_round = false;
        public int modif_roundSegm = Config.REVOLVE_SEGMENTS_STD;
        
        public bool isModif_depth3D = false;
        public int modif_depth3DSegm = Config.DEPTH3D_SEGMENTS_STD;
        public List<double> listDepth3D_Slices = new List<double>();
        public int modifDepth3D_MaxSize = Config.DEPTH3DMAX_START;
        
        
        public bool isModif_cutout = false;
        
        public double modif_kofSimpleCurve = 1; 
        
        
        public bool isModif_Shape3D = false;

        public virtual void Copy(LayerData LayerDataFrom) 
        {
            ClearAll();

            base_LayerData = LayerDataFrom;
            
            snapshot = LayerDataFrom.snapshot;
            nameLayer = LayerDataFrom.nameLayer;
            isModif_mirrorX = LayerDataFrom.isModif_mirrorX;
            isModif_mirrorY = LayerDataFrom.isModif_mirrorY;
            isModif_mirrorZ = LayerDataFrom.isModif_mirrorZ;
            isModif_bevelX = LayerDataFrom.isModif_bevelX;
            isModif_bevelY = LayerDataFrom.isModif_bevelY;
            isModif_bevelZ = LayerDataFrom.isModif_bevelZ;
            modif_bevelSize = LayerDataFrom.modif_bevelSize;
            isModif_round = LayerDataFrom.isModif_revolve;
            modif_roundSegm = LayerDataFrom.modif_revolveSegm;
            isModif_depth3D = LayerDataFrom.isModif_depth3D;
            modif_depth3DSegm = LayerDataFrom.modif_depth3DSegm;
            listDepth3D_Slices.AddRange(LayerDataFrom.listDepth3D_Slices);
            modifDepth3D_MaxSize = LayerDataFrom.modifDepth3D_MaxSize;
            isModif_cutout = LayerDataFrom.isModif_cutout;
            modif_kofSimpleCurve = LayerDataFrom.modif_kofSimpleCurve;
            
            isModif_Shape3D = LayerDataFrom.isModif_Shape3D;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public virtual void ClearAll()
        {
            base_LayerData = null;
            
            snapshot = Array.Empty<byte>();
            nameLayer = "Layer";
            isModif_mirrorX = false;
            isModif_mirrorY = false;
            isModif_mirrorZ = false;
            isModif_bevelX = false;
            isModif_bevelY = false;
            isModif_bevelZ = false;
            modif_bevelSize = Config.BEVEL_SIZE_STD;
            isModif_round = false;
            modif_roundSegm = Config.REVOLVE_SEGMENTS_STD;
            isModif_depth3D = false;
            modif_depth3DSegm = Config.DEPTH3D_SEGMENTS_STD;
            listDepth3D_Slices.Clear();
            modifDepth3D_MaxSize = Config.DEPTH3DMAX_START;
            isModif_cutout = false;
            modif_kofSimpleCurve = 1;

            isModif_Shape3D = false;
        }
        
    }
}