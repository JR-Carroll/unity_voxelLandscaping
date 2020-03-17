using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TerranBuilder;

class TileEditor : EditorWindow {
    private Dictionary<string, List<string>> fileData;
    string x = "0";
    string z = "0";

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
        x = EditorGUILayout.TextField("Width of Floor (x)", x);
        z = EditorGUILayout.TextField("Depth of Floor (z)", z);

        if (GUILayout.Button("Build Land"))
        {
            TerrainBuilder land = new TerrainBuilder();
            land.playFieldSizeX = int.Parse(x);
            land.playFieldSizeY = int.Parse(z);
            //land.placeNewFloor(Resources.Load("land_generic"), int.Parse(x), int.Parse(z));
        }
    }
}