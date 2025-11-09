using UnityEditor;
using UnityEngine;

namespace FastMesh
{
    
    public class ConfigMeshs : ScriptableObject
    {
        private static ConfigMeshs _instance;
        public static ConfigMeshs Instance
        {
            get
            {
                if (_instance == null)
                {
                    string path = "Assets/Plugins/FastMesh/Editor/Config/ConfigMeshs.asset";
                    _instance = AssetDatabase.LoadAssetAtPath<ConfigMeshs>(path);
        
                    if (_instance == null)
                    {
                        string[] guids = AssetDatabase.FindAssets("t:ConfigMeshs");
                        if (guids.Length == 0)
                        {
                            Debug.LogError("Config 'ConfigMeshs' not found");
                            return null;
                        }
                        
                        string path1 = AssetDatabase.GUIDToAssetPath(guids[0]);
                        
                        _instance = AssetDatabase.LoadAssetAtPath<ConfigMeshs>(path1);
                    }
                }
        
                return _instance;
            }
        }
        
        
        public Mesh Mesh_Figure3D_Sphere;
        public Mesh Mesh_Figure3D_Cube;
        public Mesh Mesh_Figure3D_Capsule;
        public Mesh Mesh_Figure3D_Cylinder;
        
        public Mesh TransMove_MeshArray;
        public Mesh TransRot_Mesh;
        public Mesh TransScale_Mesh;
        public Mesh TransRect_Mesh;
        public Mesh TransRectLine_Mesh;
        
        public Mesh Pivot_Mesh;
        
        public GameObject prefab_Grid;
        public GameObject prefab_Grid_mirrorX;
        public GameObject prefab_Grid_mirrorY;
        public GameObject prefab_Grid_mirrorZ;
        
        public GameObject prefab_Revolve_Center;
        public GameObject prefab_Revolve_TopBottom;
        public GameObject prefab_Revolve_Wall;

    }
}