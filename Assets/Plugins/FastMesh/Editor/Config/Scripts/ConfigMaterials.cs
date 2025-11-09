using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FastMesh
{
    
    public class ConfigMaterials : ScriptableObject
    {
        private static ConfigMaterials _instance;
        public static ConfigMaterials Instance
        {
            get
            {
                if (_instance == null)
                {
                    string path = "Assets/Plugins/FastMesh/Editor/Config/ConfigMaterials.asset";
                    _instance = AssetDatabase.LoadAssetAtPath<ConfigMaterials>(path);
        
                    if (_instance == null)
                    {
                        string[] guids = AssetDatabase.FindAssets("t:ConfigMaterials");
                        if (guids.Length == 0)
                        {
                            Debug.LogError("Config 'ConfigMaterials' not found");
                            return null;
                        }
                        
                        string path1 = AssetDatabase.GUIDToAssetPath(guids[0]);
                        
                        _instance = AssetDatabase.LoadAssetAtPath<ConfigMaterials>(path1);
                    }
                }
        
                return _instance;
            }
        }
        
        
        public Material material_view_pixel;
        public Material material_view_pixelMirror;
        public Material material_view_vremjnka;
        public Material material_view_fade;
        public Material material_view_bevel;
        public Material material_RED;

        public Material material_Display_pixel;
        public Material material_Display_bevel;
        public Material material_Display_pixelMirror;
        public Material material_Display_vremjnka;
        public Material material_Display_fade;
        public Material material_Display_cutout;
        public Material material_Display_cutoutMirror;
        public Material material_Display_cutoutFade;
        
        public Material Trans_MaterialZ;
        public Material Trans_MaterialX;
        public Material Trans_MaterialY;
        public Material Trans_MaterialNOT;
        public Material Trans_MaterialBlue;
        public Material Trans_MaterialGray;

        public Material Modeler_Pixel;
        public Material Modeler_Fade;

        private static Dictionary<int, Material> modeler_Pixel_ALL = new Dictionary<int, Material>();
        public Material Get_Modeler_Pixel_ALL(int index)
        {
            index = index % Config.COLOR_ALL.Length;

            if (modeler_Pixel_ALL.ContainsKey(index) == true)
            {
                if (modeler_Pixel_ALL[index] == null)
                    modeler_Pixel_ALL.Clear();
                else
                    return modeler_Pixel_ALL[index];
            }
            
            var newManetial = new Material(Modeler_Pixel);
            newManetial.color = Config.COLOR_ALL[index];

            modeler_Pixel_ALL.Add(index, newManetial);

            return newManetial;
        }
    }
}