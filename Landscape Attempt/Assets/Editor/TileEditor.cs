using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

class TileEditor : EditorWindow {
    private Dictionary<string, List<string>> fileData;

    [MenuItem("Window/TileEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TileEditor));
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
    }

    void SceneGUI(SceneView sceneView)
    {
        // This will have scene events including mouse down on scenes objects
        Event cur = Event.current;

        if (cur.ToString().StartsWith ("Event: mouseDown"))
        {
            Debug.Log(cur.ToString());
        }
        
    }

    void OnGUI()
    {
        if (GUILayout.Button("Reload Assets"))
        {
            fileData = retrive_AssetsByType();
        } else
        {
            GUILayout.Button("");
        }

        if (fileData != null)
        {
            foreach (var key in fileData.Keys)
            {
                //
                foreach (var file in fileData[key])
                {

                    Texture2D myTexture = AssetPreview.GetAssetPreview(Resources.Load(file.Split('.')[0]));
                    GUILayout.Button(myTexture, GUILayout.Width(95), GUILayout.Height(95));
                }
            }
        }
    }

    private Dictionary<string, List<string>> retrive_AssetsByType()
    {
        var assets = new Dictionary<string, List<string>> { };
        var fileNames = new DirectoryInfo("Assets\\Resources");

        foreach (string file in Directory.GetFiles("Assets\\Resources")) {
            
            var fileName = Path.GetFileName(file);
            var ext = fileName.Split('.');
            var type = fileName.Split('_');

            //Debug.Log($"{fileName} {ext} {type}");

            if (ext[ext.Length - 1] == "meta")
            {
                // pass
            }
            else if (!assets.ContainsKey(type[0]))
            {
                assets.Add(type[0], new List<string> { fileName });
            } else
            {
                assets[type[0]].Add(fileName);
            }
        }

        return assets;
    }
}