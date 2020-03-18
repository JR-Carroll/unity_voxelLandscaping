using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
//using TerranBuilder;

class TerrainGenerator : EditorWindow {
    public float dimensionOfCube = 200;

    private Dictionary<string, List<string>> fileData;
    private string x = "0";
    private string z = "0";
    private string y = "0";
    private int n = 0;
    private bool randomizeEnvironment = true;
    private bool clearPreviousEnvironment = false;

    private GameObject sceneObj = null;

    [MenuItem("Window/+World Builder/Terrain Editor")]
    public static void ShowWindow() 
    {
        GetWindow(typeof(TerrainGenerator));

    }

    void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
    }

    void SceneGUI(SceneView sceneView)
    {
        // This will have scene events including mouse down on scenes objects
        Event cur = Event.current;

        if (cur.ToString().StartsWith ("Event: mouseUp"))
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
            buildLand();
        }

        y = EditorGUILayout.TextField("Num. of elements", y);
        randomizeEnvironment = EditorGUILayout.Toggle("Randomize", randomizeEnvironment);
        clearPreviousEnvironment = EditorGUILayout.Toggle("Clear SceneObj First", clearPreviousEnvironment);
        if (GUILayout.Button("Generate"))
        {
            generateEnvironment();
        }
    }

    void buildLand()
    {
        GameObject generatedLand = new GameObject($"generatedLand_{n}");

        for (int _z = 0; _z < int.Parse(z); _z++)
        {
            for (int _x = 0; _x < int.Parse(x); _x++)
            {
                // Get handle on the gameObject we wish to create...
                GameObject land = Instantiate(Resources.Load("land_generic", typeof(GameObject))) as GameObject;
                land.tag = "floor";

                // Let's adjust the location of where this 
                Vector3 location = new Vector3(0, 0, 0);

                // Annoate the floor dimension
                float cubeDimension = land.transform.localScale.x;

                var newZ = _z* cubeDimension; // take half of the dim so it sits nicely!
                var newX = _x*cubeDimension;

                land.transform.position = new Vector3(newX, 0, newZ);
                land.transform.parent = generatedLand.transform;
            }
        }

        n++;
    }

    void generateEnvironment()
    {
        
        GameObject rock = Resources.Load("int_rock", typeof(GameObject)) as GameObject;
        GameObject tree = Resources.Load("int_tree", typeof(GameObject)) as GameObject;
        GameObject cage = Resources.Load("int_cage", typeof(GameObject)) as GameObject;
        GameObject[] avaialbleObjs = { rock, tree, cage };

        GameObject[] landObj = GameObject.FindGameObjectsWithTag("floor"); 

        if (!sceneObj)
        {
            sceneObj = new GameObject("sceneObjects");
        }
        
        if (clearPreviousEnvironment)
        {
            for (int i = sceneObj.transform.childCount; i > 0; --i)
                DestroyImmediate(sceneObj.transform.GetChild(0).gameObject);
        }

        for (var j = 0; j < int.Parse(y); j++)
        {
            // Create objects and place them!
            if (randomizeEnvironment)
            {
                GameObject el = Instantiate(avaialbleObjs[Random.Range(0, avaialbleObjs.Length)]);
                el.transform.parent = sceneObj.transform;
                GameObject randLand = landObj[Random.Range(0, landObj.Length - 1)];
                Vector3 landTransform = randLand.transform.position;

                el.transform.position = new Vector3(landTransform.x, randLand.transform.localScale.y/2, landTransform.z);
            }
        }

    }
}