using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FastMesh
{
    [Serializable]
    public abstract class LayerData : ScriptableObject
    {
        [HideInInspector] public TypeLayer typeLayer; 
        
        
        [HideInInspector] public int indexModeler = -1;

        
        [HideInInspector] public byte[] snapshot = Array.Empty<byte>();
        [HideInInspector] public string nameLayer;

        [HideInInspector] public bool isModif_mirrorX = false;
        [HideInInspector] public bool isModif_mirrorY = false;
        [HideInInspector] public bool isModif_mirrorZ = false;
        public bool isModif_mirror => isModif_mirrorX || isModif_mirrorY || isModif_mirrorZ;
        
        [HideInInspector] public bool isModif_bevelX = false;
        [HideInInspector] public bool isModif_bevelY = false;
        [HideInInspector] public bool isModif_bevelZ = false;
        public bool isModif_bevel => isModif_bevelX || isModif_bevelY || isModif_bevelZ;
        [HideInInspector] public int modif_bevelSize = Config.BEVEL_SIZE_STD;

        [HideInInspector] public bool isModif_revolve = false;
        [HideInInspector] public int modif_revolveSegm = Config.REVOLVE_SEGMENTS_STD;

        
        [HideInInspector] public bool isModif_depth3D = false;
        [HideInInspector] public int modif_depth3DSegm = Config.DEPTH3D_SEGMENTS_STD;
        [HideInInspector] public List<double> listDepth3D_Slices = new List<double>(); 
        [HideInInspector] public int modifDepth3D_MaxSize = Config.DEPTH3DMAX_START;

        
        [HideInInspector] public bool isModif_cutout = false;
        
        [HideInInspector] public double modif_kofSimpleCurve = 1; 
        
        
        [HideInInspector] public bool isModif_Shape3D = false;
        
        public virtual void Init()
        {
        }

        public virtual void Copy(LayerData LayerDataFrom) 
        {
            ClearAll();
            CopyOnlyModif(LayerDataFrom);
        }
        public virtual void Copy(LayerClass LayerDataFrom) 
        {
            ClearAll();
            CopyOnlyModif(LayerDataFrom);
        }
        
        public void CopyOnlyModif(LayerClass LayerDataFrom)
        {
            snapshot = LayerDataFrom.snapshot;
            nameLayer = LayerDataFrom.nameLayer;
            isModif_mirrorX = LayerDataFrom.isModif_mirrorX;
            isModif_mirrorY = LayerDataFrom.isModif_mirrorY;
            isModif_mirrorZ = LayerDataFrom.isModif_mirrorZ;
            isModif_bevelX = LayerDataFrom.isModif_bevelX;
            isModif_bevelY = LayerDataFrom.isModif_bevelY;
            isModif_bevelZ = LayerDataFrom.isModif_bevelZ;
            modif_bevelSize = LayerDataFrom.modif_bevelSize;
            isModif_revolve = LayerDataFrom.isModif_round;
            modif_revolveSegm = LayerDataFrom.modif_roundSegm;
            isModif_depth3D = LayerDataFrom.isModif_depth3D;
            modif_depth3DSegm = LayerDataFrom.modif_depth3DSegm;
            modifDepth3D_MaxSize = LayerDataFrom.modifDepth3D_MaxSize;
            isModif_cutout = LayerDataFrom.isModif_cutout;
            modif_kofSimpleCurve = LayerDataFrom.modif_kofSimpleCurve;
            
            listDepth3D_Slices.Clear();
            listDepth3D_Slices.AddRange(LayerDataFrom.listDepth3D_Slices);

            isModif_Shape3D = LayerDataFrom.isModif_Shape3D;
        }
        
        public void CopyOnlyModif(LayerData LayerDataFrom)
        {
            snapshot = LayerDataFrom.snapshot;
            nameLayer = LayerDataFrom.nameLayer;
            isModif_mirrorX = LayerDataFrom.isModif_mirrorX;
            isModif_mirrorY = LayerDataFrom.isModif_mirrorY;
            isModif_mirrorZ = LayerDataFrom.isModif_mirrorZ;
            isModif_bevelX = LayerDataFrom.isModif_bevelX;
            isModif_bevelY = LayerDataFrom.isModif_bevelY;
            isModif_bevelZ = LayerDataFrom.isModif_bevelZ;
            modif_bevelSize = LayerDataFrom.modif_bevelSize;
            isModif_revolve = LayerDataFrom.isModif_revolve;
            modif_revolveSegm = LayerDataFrom.modif_revolveSegm;
            isModif_depth3D = LayerDataFrom.isModif_depth3D;
            modif_depth3DSegm = LayerDataFrom.modif_depth3DSegm;
            modifDepth3D_MaxSize = LayerDataFrom.modifDepth3D_MaxSize;
            isModif_cutout = LayerDataFrom.isModif_cutout;
            modif_kofSimpleCurve = LayerDataFrom.modif_kofSimpleCurve;
            
            listDepth3D_Slices.Clear();
            listDepth3D_Slices.AddRange(LayerDataFrom.listDepth3D_Slices);
            
            isModif_Shape3D = LayerDataFrom.isModif_Shape3D;
        }
        
        public virtual void ClearAll()
        {
            snapshot = Array.Empty<byte>();
            nameLayer = this.name;
            isModif_mirrorX = false;
            isModif_mirrorY = false;
            isModif_mirrorZ = false;
            isModif_bevelX = false;
            isModif_bevelY = false;
            isModif_bevelZ = false;
            modif_bevelSize = Config.BEVEL_SIZE_STD;
            isModif_revolve = false;
            modif_revolveSegm = Config.REVOLVE_SEGMENTS_STD;
            isModif_depth3D = false;
            modif_depth3DSegm = Config.DEPTH3D_SEGMENTS_STD;
            modifDepth3D_MaxSize = Config.DEPTH3DMAX_START;
            isModif_cutout = false;
            modif_kofSimpleCurve = 1;
            
            listDepth3D_Slices.Clear();

            isModif_Shape3D = false;
        }

        public abstract bool IsEmpty();

    }
}