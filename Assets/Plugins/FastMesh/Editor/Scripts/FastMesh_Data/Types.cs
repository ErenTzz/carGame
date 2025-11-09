namespace FastMesh
{
    public enum TypeAxis
    {
        not = 0,
        Z = 1,
        mZ = 2,
        X = 3,
        mX = 4,
        Y = 5,
        mY = 6,
    }
    
    public enum TypeXYZ
    {
        not = 0,
        X = 1,
        Y = 2,
        Z = 3,
    }
    
    public enum TypeSystem
    {
        Not = 0,
        Edit_Setka = 101,
        Edit_Fugure3D = 102,
        Edit_Master = 103,
        
        Home = 150,
        Modeler = 160,
    }
    
    public enum TypeLayer
    {
        Not = 0,
        Setka = 1,
        Fugure3D = 2,
        Master = 3,
    }

    public enum TypeToolBar
    {
        Not = 0,
        Master_draw = 1, 
        Master_drawTwoSide = 2, 
        Master_draw2Dto3D = 3, 
        Master_drawShape3D = 4, 
        
        Master_trans = 20, 
        Master_transMove = 21, 
        Master_transRot = 22, 
        Master_transScale = 23, 
        Master_transRect = 24, 
        
        Master_Split = 30,
    }
    
    public enum TypeRightInfo
    {
        Not = 0,
        
        Info_Bevel = 11,
        Info_Cutout = 12,
        Info_Revolve = 13,
        Info_Eraser = 14,
        Info_LayerMode = 15,
        Info_2Dto3D_Slice = 16,
        Info_SplitLayer = 17,
        Info_Texture = 18,
    }
    
    public enum TypeHierarchyOne
    {
        Not = 0,
        Std = 1, 
    }
    
    public enum TypePopUpOne
    {
        Not = 0,
        
        RenameLayer = 1,
        RenameProject = 2,
        SetShapeMode = 3,
        MoreLayers = 4,
        
        Home_CreateDirectory = 10,
        Home_Project = 11,
        
        Guide = 21,
    }
    
    public enum TypeTool
    {
        not = 0,
        
        Pen_Circle = 101,
        Pen_Square = 102,
        Pen_Triangle = 103,
        Pen_Romb = 104,
        Pen_Pentangle = 105,
        Pen_Hexagon1 = 106,
        Pen_Hexagon2 = 107,
        Pen_Line = 110,
        
        LasoShape_Square = 201,
        LasoShape_Circle = 202,
        LasoDrow_Line = 211,
        LasoDrow_Just = 212,
        
        Depth3D_line = 301,
        Depth3D_clear = 302,
        
        
        
        
        
    }

    public enum TypeFigure3D
    {
        not = 0,
        
        Figure3D_Sphere = 301,
        Figure3D_Cube = 302,
        Figure3D_Capsule = 303,
        Figure3D_Cylinder = 304,
    }

    
    
    
    public enum TypeTrans
    {
        not = 0,
        
        Trans_Move = 101,
        Trans_Rot = 102,
        Trans_Scale = 103,
        Trans_Rect = 104,
    }

    public enum TypeToolWind
    {
        not = 0,
 
        Edit_Setka = 101,
        Edit_Fugure3D = 102,
        Edit_Master = 103,
    }
    
    public enum TypeDisplayState
    {
        Not = 0,
        CanvasPen = 10,
        Trans = 21,
        TransAuto = 22,
        
        TwistMove = 31,
        TwistAuto = 32,
        TwistStop = 33,
        TwistWhere = 34,
        
        ClickTool = 41,
        
        CutStop = 51,
        CutMove = 52,
        CutAuto = 53,
        CutWhere = 54,
    }

    public enum TypeUndor
    {
        Not = 0,
        Layer_delete_or_sort = 1, 
        
        Project_all = 5,

        Master_open = 10, 
        Master_data = 11, 
        Master_create = 12, 
        Master_cutout = 13, 
        Master_transAllModel = 14, 
    }

    public enum TypeModeler
    {
        OneLayer_OneMesh = 1,
        AllLayer_OneMEsh = 2,
        Custom = 10,
    }
    public enum TypeModelerPivot
    {
        Bottom = 1,
        Center = 2,
        Custom = 10,
    }
}