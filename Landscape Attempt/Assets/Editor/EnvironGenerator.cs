using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
//using TerranBuilder;

class EnvironGenerator : EditorWindow
{
    public float dimensionOfCube = 200;

    private Dictionary<string, List<string>> fileData;
    private string x = "0";
    private string z = "0";
    private bool randomize = false;
    private int n = 0;

    [MenuItem("Window/+World Builder/Environment Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EnvironGenerator));
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
    }

    void SceneGUI(SceneView sceneView)
    {
        // This will have scene events including mouse down on scenes objects
        Event cur = Event.current;

        if (cur.ToString().StartsWith("Event: mouseDown"))
        {
            Debug.Log(cur.ToString());
        }

    }

    void OnGUI()
    {
        x = EditorGUILayout.TextField("Num. of elements", x);
        randomize = EditorGUILayout.Toggle("Randomize", randomize);
        if (GUILayout.Button("Generate"))
        {
            generateEnvironment();
        }
    }

    void generateEnvironment()
    {
        GameObject generatedLand = new GameObject($"generatedLand_{n}");

        for (int _z = 0; _z < int.Parse(z); _z++)
        {
            for (int _x = 0; _x < int.Parse(z); _x++)
            {
                // Get handle on the gameObject we wish to create...
                GameObject land = Instantiate(Resources.Load("land_generic", typeof(GameObject))) as GameObject;

                // Let's adjust the location of where this 
                Vector3 location = new Vector3(0, 0, 0);

                // Annoate the floor dimension
                float cubeDimension = land.transform.localScale.x;

                var newZ = _z * cubeDimension; // take half of the dim so it sits nicely!
                var newX = _x * cubeDimension;

                land.transform.position = new Vector3(newX, 0, newZ);
                land.transform.parent = generatedLand.transform;
            }
        }

        n++;
    }
}