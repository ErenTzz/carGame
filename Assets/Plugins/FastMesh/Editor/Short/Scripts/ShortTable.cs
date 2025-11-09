using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastMesh
{
    
    public class ShortTable : ScriptableObject 
    {
        [HideInInspector] public List<Ser_Bytes> listBytes = new List<Ser_Bytes>();
        [HideInInspector] public int width;
        [HideInInspector] public int height;
        public double timeFrame = 0.1;

        public Texture2D[] textureS;

        private Dictionary<int, Texture2D> textureS_forWork = new Dictionary<int, Texture2D>();

        public Texture2D GetIndex(int index)
        {
            index = index % listBytes.Count;

            if (textureS_forWork.ContainsKey(index) == false)
            {
                var texture11 = new Texture2D(width, height);
                texture11.LoadImage(listBytes[index].bytes);
                textureS_forWork.Add(index, texture11);
            }
            else if (textureS_forWork[index] == null)
            {
                var texture11 = new Texture2D(width, height);
                texture11.LoadImage(listBytes[index].bytes);
                textureS_forWork[index] = texture11;
            }

            return textureS_forWork[index]; 
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

    }   
}

