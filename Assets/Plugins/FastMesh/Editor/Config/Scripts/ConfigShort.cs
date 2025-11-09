using UnityEditor;
using UnityEngine;

namespace FastMesh
{
    
    public class ConfigShort : ScriptableObject
    {
        private static ConfigShort _instance;
        public static ConfigShort Instance
        {
            get
            {
                if (_instance == null)
                {
                    string path = "Assets/Plugins/FastMesh/Editor/Config/ConfigShort.asset";
                    _instance = AssetDatabase.LoadAssetAtPath<ConfigShort>(path);
        
                    if (_instance == null)
                    {
                        string[] guids = AssetDatabase.FindAssets("t:ConfigShort");
                        if (guids.Length == 0)
                        {
                            Debug.LogError("Config 'ConfigShort' not found");
                            return null;
                        }
                        
                        string path1 = AssetDatabase.GUIDToAssetPath(guids[0]);
                        
                        _instance = AssetDatabase.LoadAssetAtPath<ConfigShort>(path1);
                    }
                }
        
                return _instance;
            }
        }
        
        public ShortTable S2Dto3D_1;
        public ShortTable S2Dto3D_2;
        public ShortTable S2Dto3D_3;
        public ShortTable Bevel_1;
        public ShortTable Bevel_2;
        public ShortTable Cutout_1;
        public ShortTable Cutout_2;
        public ShortTable EraserTool_1;
        public ShortTable Examle_1;
        public ShortTable Revolve_1;
        public ShortTable Split_1;
        public ShortTable Texture_1;
        public ShortTable MoreLayers;
        public ShortTable FirstStep_1;
        public ShortTable FirstStep_2_bevel;

    }
}