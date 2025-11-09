using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace FastMesh
{
    [Serializable]
    public class ProjectData : ScriptableObject
    {
        [HideInInspector] public byte[] snapshot = Array.Empty<byte>();
        public string nameProject;
        [HideInInspector] public string dataCreate;
        [HideInInspector] public List<LayerData> listLayerData = new List<LayerData>();

        [HideInInspector] public TypeModeler typeModeler = TypeModeler.OneLayer_OneMesh;
        [HideInInspector] public TypeModelerPivot typeModelerPivot = TypeModelerPivot.Bottom;
        
        [HideInInspector] public Ser_M3Point pivot = new Ser_M3Point(0, 0, 0);
        [HideInInspector] public string outputFileName = "";
        
        
        
        
        public int Init() 
        {
            dataCreate = DateTime.Now.ToString("MMMM dd, yyyy, hh:mm tt");
            
            var numberProject = EditorPrefs.GetInt("FastMesh numberProject", 1);
            
            nameProject = $"Project №{numberProject}";
            EditorPrefs.SetInt("FastMesh numberProject", numberProject + 1);

            return numberProject;
        }
        
        
        
        
        

        
        public void Restore(ProjectClass projectClass)
        {
            if (this != projectClass.BaseProjectData)
            {
                return;
            }
            
            listLayerData.Clear();
            
            foreach (var layerClass in projectClass.listLayerClass)
            {
                var layerData = layerClass.base_LayerData;
                listLayerData.Add(layerData);
                
                
                layerData.Copy(layerClass);
                
                EditorUtility.SetDirty(layerData);
            }
        }
        
    }
}