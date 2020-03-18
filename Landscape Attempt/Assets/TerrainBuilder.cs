using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerranBuilder
{
    public class TerrainBuilder: MonoBehaviour
    {
        public enum BuildStartType
        {
            CenterLinear,
            CenterSpiral, 
            Random,
            PlayerStart,
            EdgesInward,
            AllAtOnce
        };

        public int playFieldSizeX = 5;
        public int playFieldSizeY = 5;
        public GameObject gameCube = null;
        public BuildStartType buildUsing;
        private GameObject[,] land = null;
        private Vector3 start;
        private Transform floorDimension;
        private float _adjustYFloor;

        private float newX;
        private float newZ;

        // Start is called before the first frame update
        void Start()
        {
            // Create the 2D array that will house the land objects for reference.
            land = new GameObject[playFieldSizeX, playFieldSizeY];
            // Mark starting location
            start = new Vector3(0, 0, 0);
            // Annoate the floor dimension
            floorDimension = gameCube.transform;
            _adjustYFloor = (floorDimension.localScale.y / 2);
            start.y = _adjustYFloor;

            for (int y = 0; y < playFieldSizeY; y++)
            {
                for (int x = 0; x < playFieldSizeX; x++)
                {
                    //GameObject cube = Instantiate(gameCube);
                    //cube.transform.SetParent(gameObject.transform);
                    //placeNewFloor(cube, x, y);
                    //land[x, y] = cube;
                }
            }

            return;
        }

        public void placeNewFloor(GameObject cube, float x, float y)
        {
            // Need to adjust cub down to the floor.
            newX = 200 * x;
            newZ = 200 * y;
            cube.transform.position = new Vector3(-newX, -start.y, -newZ);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}