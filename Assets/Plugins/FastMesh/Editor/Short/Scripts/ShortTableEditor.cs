using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FastMesh
{
    [CustomEditor(typeof(ShortTable))]
    public class ShortTableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            shortTableMain = (ShortTable)target;

            DrawDefaultInspector();
        }

        private ShortTable shortTableMain;
        private VisualElement container;
        private VisualElement block1;
        private Image img;
        
        public override VisualElement CreateInspectorGUI()
        {
            shortTableMain = (ShortTable)target;

            container = new VisualElement();
            container.Add(new IMGUIContainer(OnInspectorGUI));

            var buttonCreate = new Button(ClickCreate);
            buttonCreate.text = "Create";
            container.Add(buttonCreate);
            var buttonClear = new Button(ClickClear);
            buttonClear.text = "Clear Texture";
            container.Add(buttonClear);
            
            block1 = new VisualElement();
            container.Add(block1);
            
            
            container.Add(new Label($"height = {shortTableMain.height}"));
            container.Add(new Label($"width = {shortTableMain.width}"));
            container.Add(new Label("shortTableMain.listBytes.Count = " + shortTableMain.listBytes.Count.ToString()));
            
            var buttonShowShorts = new Button(ClickShowShorts);
            buttonShowShorts.text = "Show Shorts";
            container.Add(buttonShowShorts);

            img = new Image();
            img.style.height = 15f;
            img.style.width = 15f;
            
            container.Add(img);
 
            return container;
        }

        private void ClickClear()
        {
            shortTableMain.textureS = Array.Empty<Texture2D>();
            EditorUtility.SetDirty(shortTableMain);
            AssetDatabase.SaveAssets();
        }

        void ClickCreate()
        {
            var textureS = shortTableMain.textureS;
            var listBytes = shortTableMain.listBytes;
            if (textureS.Length == 0) return;
            
            listBytes.Clear();
            shortTableMain.width = textureS[0].width;
            shortTableMain.height = textureS[0].height;

            foreach (var texture2D in textureS)
                listBytes.Add(new Ser_Bytes(texture2D.EncodeToJPG()));
            
            EditorUtility.SetDirty(shortTableMain);
            AssetDatabase.SaveAssets();
            
            block1.Add(new Label("shortTableMain.listBytes.Count = " + shortTableMain.listBytes.Count.ToString()));
        }
        
        

        private void ClickShowShorts()
        {
            var listBytes = shortTableMain.listBytes;
            if (listBytes.Count == 0)
            {
                block1.Add(new Label("shortTableMain.listBytes.Count = " + shortTableMain.listBytes.Count.ToString()));
                return;
            }

            var width = shortTableMain.width;
            var height = shortTableMain.height;

            img.style.width = width;
            img.style.height = height;
            
            var texture = new Texture2D(width, height);
            texture.LoadImage(listBytes[0].bytes);
            img.image = texture;

            int indexNext = 1;
            double nextTime = EditorApplication.timeSinceStartup + shortTableMain.timeFrame;
            EditorApplication.update += Update;
            
            void Update()
            {
                if (EditorApplication.timeSinceStartup < nextTime) return;

                if (indexNext >= listBytes.Count)
                {
                    EditorApplication.update -= Update;
                    return;
                }

                var texture11 = new Texture2D(width, height);
                texture11.LoadImage(listBytes[indexNext].bytes);
                img.image = texture11;

                indexNext++;
                nextTime = EditorApplication.timeSinceStartup + shortTableMain.timeFrame;
            }
        }



    } 
}


