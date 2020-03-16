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

    void OnGUI()
    {
        if (GUILayout.Button("Reload Assets"))
        {
            fileData = retrive_AssetsByType();
        }

        if (fileData != null)
        {
            foreach (var key in fileData.Keys)
            {
                //
                foreach (var file in fileData[key])
                {
                    Debug.Log(file.Split('.')[0]);
                    Texture2D myTexture = AssetPreview.GetAssetPreview(Resources.Load(file.Split('.')[0]));
                    GUILayout.Button(myTexture, GUILayout.Width(95), GUILayout.Height(95));
                }
            }
        }
    }

    void OnClick(SceneView scene)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 2)
        {
            Debug.Log("Middle Mouse was pressed");

            Vector3 mousePos = e.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = scene.camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Do something, ---Example---
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = hit.point;
                Debug.Log("Instantiated Primitive " + hit.point);
            }
            e.Use();
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

            Debug.Log($"{fileName} {ext} {type}");

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