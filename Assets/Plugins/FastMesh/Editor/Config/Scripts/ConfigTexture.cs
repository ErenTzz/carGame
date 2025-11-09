using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FastMesh
{
    
    public class ConfigTexture : ScriptableObject
    {
        private static ConfigTexture _instance;
        public static ConfigTexture Instance
        {
            get
            {
                if (_instance == null)
                {
                    string path = "Assets/Plugins/FastMesh/Editor/Config/ConfigTexture.asset";
                    _instance = AssetDatabase.LoadAssetAtPath<ConfigTexture>(path);
        
                    if (_instance == null)
                    {
                        string[] guids = AssetDatabase.FindAssets("t:ConfigTexture");
                        if (guids.Length == 0)
                        {
                            Debug.LogError("Config 'ConfigTexture' not found");
                            return null;
                        }
                        
                        string path1 = AssetDatabase.GUIDToAssetPath(guids[0]);
                        
                        _instance = AssetDatabase.LoadAssetAtPath<ConfigTexture>(path1);
                    }
                }
        
                return _instance;
            }
        }
        
        public StyleSheet styleSheet1;

        
        public Texture2D toonPenCircle;
        public Texture2D toonPenSquare;
        public Texture2D toonPenTriangle;
        public Texture2D toonPenRomb;
        public Texture2D toonPenPentangle;
        public Texture2D toonPenHexagon1;
        public Texture2D toonPenHexagon2;
        public Texture2D toonPenLine;
        public Texture2D toonSliceLine;
        
        public Texture2D toonLasoSquare;
        public Texture2D toonLasoCircle;
        public Texture2D toonLasoLine;
        public Texture2D toonLasoJust;
        
        public Texture2D toonFigure3dSphere;
        public Texture2D toonFigure3dCube;
        public Texture2D toonFigure3dCapsule;
        public Texture2D toonFigure3dCylinder;

        public Texture2D displayX;
        public Texture2D displayY;
        public Texture2D displayZ;
        public Texture2D displayTransform;
        
        public Texture2D displaySplit;
        
        public Texture2D AxisIcon_X;
        public Texture2D AxisIcon_mX;
        public Texture2D AxisIcon_Y;
        public Texture2D AxisIcon_mY;
        public Texture2D AxisIcon_Z;
        public Texture2D AxisIcon_mZ;
    
        public Texture2D TransMove;
        public Texture2D TransRot;
        public Texture2D TransScale;
        public Texture2D TransRect;
        
        public Texture2D mouseL;
        public Texture2D mouseR;
        public Texture2D mouseC;
        public Texture2D MouseShift;
        public Texture2D MouseAlt;

        public Texture2D iconMenu1;
        public Texture2D iconMenu2;
        public Texture2D iconMenu3;
        public Texture2D iconCircl;
        public Texture2D iconFastMesh;
        public Texture2D iconSetting;
        public Texture2D iconPlus;
        public Texture2D iconGuide;
        public Texture2D iconMesh;
        public Texture2D iconCross;
       
        public Texture2D imgShape;
        public Texture2D imgTowSide;
        public Texture2D imgNotFoto;
        public Texture2D imgDirectory;
        public Texture2D imgBack;
        public Texture2D iconInfo;
    }
}