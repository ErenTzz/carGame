using UnityEngine;

namespace FastMesh
{
    public static class Config
    {
        public static bool isTest = false;
        public static bool isVideo = false;
        public static bool isDebug = false; 
        public static bool isAdmin = false;
        
        public static readonly Color COLOR_TRANSPARENT = new Color(0f, 0f, 0f, 0f);
        public static readonly Color COLOR_PointerEnter = new Color(0.35f, 0.35f, 0.35f, 0.5f); 
        public static readonly Color COLOR_bgPopUp = new Color(0f, 0f, 0f, 0.4f); 
        public static readonly Color COLOR_bgForSnaphot = new Color(0.16f, 0.16f, 0.16f);
 
        public static readonly Color COLOR_GRID_BORDER = new Color(0.28f, 0.28f, 0.28f);
        public static readonly Color COLOR_GRID_BORDER_CENTER = new Color(0.35f, 0.35f, 0.35f);
        public static readonly Color COLOR_BG = new Color(0.21f, 0.21f, 0.21f);
        public static readonly Color COLOR_BG_HOVER = new Color(0.23f, 0.25f, 0.32f);
        public static readonly Color COLOR_BG_BLACK = new Color(0.192f, 0.192f, 0.192f);
        public static readonly Color COLOR_BG_VERY_BLACK = new Color(0.16f, 0.16f, 0.16f);
        public static readonly Color COLOR_BUTTON_ICON = new Color(0.79f, 0.79f, 0.79f);
        public static readonly Color COLOR_BUTTON_ICON_CLEAR = new Color(0.79f, 0.39f, 0.41f);
        public static readonly Color COLOR_BUTTON_TEXT = Color.white;
        public static readonly Color COLOR_BUTTON_TEXT_CLEAR = new Color(1f, 0f, 0.22f);
        public static readonly Color COLOR_BUTTON_BG = new Color(0.35f, 0.35f, 0.35f);
        public static readonly Color COLOR_BUTTON_BG_CLEAR = new Color(0.35f, 0.29f, 0.29f);
        public static readonly Color COLOR_BUTTON_BG_ACTIV = new Color(0.27f, 0.38f, 0.49f);
        
        public static readonly Color COLOR_CANVAS_PIXEL = new Color(0.58f, 0.59f, 0.58f);
        public static readonly Color COLOR_CANVAS_VREMJNKA_full = new Color(0.23f, 0.59f, 0.22f);
        public static readonly Color COLOR_CANVAS_VREMJNKA_half = new Color(0.23f, 0.59f, 0.22f, 0.3f);
        public static readonly Color COLOR_CANVAS_FADE = new Color(0.47f, 0.48f, 0.49f, 0.15f);
        public static readonly Color COLOR_CANVAS_MIRROR_LINE = new Color(0.13f, 0.78f, 0.62f, 0.7f);
        public static readonly Color COLOR_CANVAS_MIRROR_PIXEL = new Color(0.46f, 0.58f, 0.58f);
        public static readonly Color COLOR_CANVAS_ROUND_LOCK = new Color(0.192f, 0.192f, 0.192f, 0.7f);
        public static readonly Color COLOR_CANVAS_ROUND_LINE = new Color(0.22f, 0.63f, 0.78f, 0.7f);
        public static readonly Color COLOR_CANVAS_CUTOUT = new Color(0.75f, 0.47f, 0.48f);
        public static readonly Color COLOR_CANVAS_CUTOUT_MIRROR = new Color(0.51f, 0.37f, 0.39f);
        public static readonly Color COLOR_CANVAS_CUTOUT_FADE = new Color(0.54f, 0.23f, 0.23f, 0.3f);
        public static readonly Color COLOR_CANVAS_DEPTH3D_line = new Color(1f, 1f, 1f, 0.8f);
        public static readonly Color COLOR_CANVAS_DEPTH3D_lineBase = new Color(1f, 0.51f, 0.91f, 0.8f);
        
        public static readonly Color COLOR_TEXT = new Color(0.81f, 0.81f, 0.81f);


        public static readonly Color[] COLOR_ALL = new Color[]   
        {
            new Color(0.51f, 0.97f, 0.62f), new Color(0.75f, 0.51f, 0.97f), new Color(0.75f, 0.45f, 0.39f),
            new Color(0.44f, 0.71f, 0.36f), new Color(0.37f, 0.62f, 0.71f), new Color(0.73f, 0.36f, 0.53f), 
            new Color(0.73f, 0.75f, 0.37f), new Color(0.36f, 0.71f, 0.53f), new Color(0.35f, 0.35f, 0.71f), 
            new Color(0.59f, 0.35f, 0.7f), new Color(0.41f, 0.31f, 0.2f), new Color(0.2f, 0.37f, 0.2f),
            new Color(0.97f, 0.51f, 0.51f), new Color(0.75f, 0.97f, 0.51f), new Color(0.51f, 0.97f, 0.95f),
            new Color(0.51f, 0.62f, 0.97f), new Color(0.97f, 0.51f, 0.85f), new Color(0.96f, 0.85f, 0.51f),
            new Color(0.22f, 0.32f, 0.43f), new Color(0.42f, 0.21f, 0.4f), new Color(0.34f, 0.4f, 0.2f),
            new Color(0.2f, 0.4f, 0.34f), new Color(0.25f, 0.2f, 0.39f), new Color(0.44f, 0.22f, 0.26f),
        };
        

        public static readonly Color COLOR_CURSOR_PEN = Color.white;
        public static readonly Color COLOR_CURSOR_CLEAR = Color.red;
        public static readonly Color COLOR_CURSOR_CUT = Color.yellow;

        
        public const int DISPLAY_INDEX_MOUSE_BUTTON__PEN = 0;
        
        public const int DISPLAY_INDEX_MOUSE_BUTTON__TWIST_1 = 1;
        public const int DISPLAY_INDEX_MOUSE_BUTTON__TWIST_2 = 2;
        
        
        public const int PEN_SIZE_MIN_draw = 10;
        public const int PEN_SIZE_MIN_clean = 2;
        public const int PEN_SIZE_START = 50;
        public const int PEN_SIZE_MAX = 900;
        
        
        public const int DEPTH3DMAX_MIN = 10;
        public const int DEPTH3DMAX_START = 500;
        public const int DEPTH3DMAX_MAX = 1000;
        public const int DEPTH3D_SEGMENTS_MIN = 1; 
        public const int DEPTH3D_SEGMENTS_STD = 2;
        public const int DEPTH3D_SEGMENTS_MAX = 4;

        public const int BEVEL_SIZE_MIN = 2;
        public const int BEVEL_SIZE_STD = 5;
        public const int BEVEL_SIZE_MAX = 50;
        public const double BEVEL_OFFSET = 4; 
        public const double BEVEL_NEED_PLUS_SIZE = 5.6; 
    
        
        public const int REVOLVE_SEGMENTS_MIN = 4;
        public const int REVOLVE_SEGMENTS_STD = 8;
        public const int REVOLVE_SEGMENTS_MAX = 16; 

        

        public const int PIXEL_THS = 1000;
        public const int PIXEL_THS_M1 = 999; 
        public const int PIXEL_THS_HALF = 500;
        public const int PIXEL_THS_10Procent = 100;
        
        public const int PIXEL_DEFAULT_VREMJNKA_FROM = 450;
        public const int PIXEL_DEFAULT_VREMJNKA_TO = 550;
        public const int PIXEL_DEFAULT_VREMJNKA_FROM__roundZ = 100;
        public const int PIXEL_DEFAULT_VREMJNKA_TO__roundZ = 900;
        
        public const float FRAME_width_hierarchy = 150f;
        public const float FRAME_width_toolBar = 200f;
        public const float FRAME_widthPR_canvas = 0.7f;
        public const float FRAME_widthPR_right = 0.3f;
        public const float FRAME_height_logo = 30f;

        public const float FRAME_TOP_H = 0f;
        public const float FRAME_CEN_TOP_BOTTOM = 30f;
        public const float FRAME_LEFT_W = FRAME_width_toolBar;

        public const float KOF_POGRESH_THSNIZ = 0.1f;
        public const float KOF_POGRESH_THSNIZ_sqr = 0.01f;
        
        
        
        
        public const float k_Epsilon = 0.001f;
        public const float k_Epsilon_sqr = 0.00001f;
        public const double d_Epsilon = 0.001;
        public const float k_EpsilonNormal = 0.00002f; 
        public const double d_EpsilonNormal = 0.00002; 

    }
}

