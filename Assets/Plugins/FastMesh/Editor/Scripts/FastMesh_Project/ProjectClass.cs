using System;
using System.Collections.Generic;

namespace FastMesh
{
    public class ProjectClass
    {
        public ProjectData BaseProjectData;
        
        public List<LayerClass> listLayerClass = new List<LayerClass>();

        public void Clear()
        {
            BaseProjectData = null;
            listLayerClass.Clear();
        }
        
        public void Copy(ProjectData projectData)
        {
            Clear();

            BaseProjectData = projectData;
            
            if (projectData.listLayerData.Count == 0) return;
            
            foreach (var layerData in projectData.listLayerData)
            {
                switch (layerData.typeLayer)
                {
                    case TypeLayer.Master:
                        var layerClass_Master = new LayerClass_Master();
                        layerClass_Master.Copy((LayerData_Master)layerData);
                        listLayerClass.Add(layerClass_Master);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }
}