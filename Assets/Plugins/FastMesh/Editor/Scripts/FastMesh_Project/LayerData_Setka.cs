using System;
using UnityEngine;

namespace FastMesh
{
    [Serializable]
    public class LayerData_Setka : LayerData
    {
        
        
        [HideInInspector] public bool[] linePixel_Z;
        [HideInInspector] public bool[] linePixel_X;
        [HideInInspector] public bool[] linePixel_Y;
        
        
        public TypeAxis mainTypeAxis = TypeAxis.not;


        public override void Init()
        {
            base.Init();
            typeLayer = TypeLayer.Setka;
            linePixel_Z = new bool[Config.PIXEL_THS * Config.PIXEL_THS];
            linePixel_X = new bool[Config.PIXEL_THS * Config.PIXEL_THS];
            linePixel_Y = new bool[Config.PIXEL_THS * Config.PIXEL_THS];
        }
        
        
        
        
        
        
        
        public void Save(TypeAxis typeAxis, bool[,] arrPixel)
        {
            if (typeAxis == TypeAxis.X)
                Save(arrPixel, linePixel_X);
            else if (typeAxis == TypeAxis.Y) 
                Save(arrPixel, linePixel_Y);
            else if (typeAxis == TypeAxis.Z) 
                Save(arrPixel, linePixel_Z);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        void Save(bool[,] arrPixel, bool[] linePixel)
        {
            int index = 0;
            for (int x = 0; x < Config.PIXEL_THS; x++)
            for (int y = 0; y < Config.PIXEL_THS; y++)
                linePixel[index++] = arrPixel[x, y];
        }

        public void Get_X(bool[,] arrPixel) => Get(linePixel_X, arrPixel);
        public void Get_Y(bool[,] arrPixel) => Get(linePixel_Y, arrPixel);
        public void Get_Z(bool[,] arrPixel) => Get(linePixel_Z, arrPixel);
        
        bool[,] Get(bool[] linePixel, bool[,] arrPixel)
        {
            

            int index = 0;
            for (int x = 0; x < Config.PIXEL_THS; x++)
            for (int y = 0; y < Config.PIXEL_THS; y++)
                arrPixel[x, y] = linePixel[index++];
            
            return arrPixel;
        }

        public void Set_mainTypeAxis(TypeAxis typeAxisNew)
        {
            mainTypeAxis = typeAxisNew;
        }
        
        public override void Copy(LayerData LayerDataFrom)
        {
            base.Copy(LayerDataFrom);
            
            if (LayerDataFrom.typeLayer != TypeLayer.Setka)
                throw new ArgumentOutOfRangeException(nameof(TypeLayer));
            var LayerDataFromSetka = (LayerData_Setka) LayerDataFrom;

            mainTypeAxis = LayerDataFromSetka.mainTypeAxis;
            Array.Copy(LayerDataFromSetka.linePixel_X, 0, linePixel_X, 0, linePixel_X.Length);
            Array.Copy(LayerDataFromSetka.linePixel_Y, 0, linePixel_Y, 0, linePixel_Y.Length);
            Array.Copy(LayerDataFromSetka.linePixel_Z, 0, linePixel_Z, 0, linePixel_Z.Length);
        }

        public override bool IsEmpty()
        {
            return false;
        }

    }
}