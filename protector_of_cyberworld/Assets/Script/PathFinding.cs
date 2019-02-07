using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    private static Vector3[] path = null;
    public static Vector3[] Path1 { get { return path1; } }
    private static Vector3[] spawn = null;
    private static Vector3[] path1 = null;
    private int spawnCnt = 0;
    private int tileCnt = 0;
    public static int numOfTile1 = 0;
    int midLane, path1Size;
    private void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MapGenerator.X; i++)
        {
            for (int j = 0; j < MapGenerator.Z; j++)
            {
                if (j == 0 && MapGenerator.GetTileType(i, j) == 1)
                {
                    midLane = i;
                    Debug.Log(i + " " + j);
                }
            }
        }
        //v temp size
        path1Size = MapGenerator.Z * (midLane + 1);
        path = new Vector3[path1Size];
        spawn = new Vector3[2];


        Pathfinding1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Pathfinding1()
    {


        for (int i = 0; i < midLane + 1; i++)
        {
            for (int j = MapGenerator.Z - 1; j >= 0; j--)
            {
                if (MapGenerator.GetTileType(i, j) == 1)
                {
                    path[tileCnt] = new Vector3(i, 0, j);
                    tileCnt++;
                } else if (MapGenerator.GetTileType(i, j) == 5)
                {
                    spawn[spawnCnt] = new Vector3(i, 0, j);
                    Debug.Log("s: " + spawn[spawnCnt]);
                    spawnCnt++;
                    
                }

            }
        }

        numOfTile1 = tileCnt;
        path1 = new Vector3[numOfTile1];

        //Debug.Log(path1Size);
        //Debug.Log(tile1Cnt);



        //sorting

        for (int i = 0; i < numOfTile1; i++) {
            path1[i] = path[i];
        }


        for (int k = 0; k < path1.Length; k++)
        {
            Debug.Log(path1[k]);
        }

    }

}



