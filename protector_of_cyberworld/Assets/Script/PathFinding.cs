using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

	private static Vector3[] path = null;
    private static Vector3[] spawn = null;
    private static Vector3[] path1 = null;
	private static Vector3[] path2 = null;
    private int spawnCnt = 0;
    private int tileCnt = 0;
    public static int numOfTile1 = 0, numOfTile2 = 0;
    int midLane, pathSize;
	public static Vector3[] Path1 { get { return path1; } }
	public static Vector3[] Path2 { get { return path2; } }



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
                   // Debug.Log(i + " " + j);
                }
            }
        }
        //v temp size
        pathSize = MapGenerator.Z * MapGenerator.X;

        path = new Vector3[pathSize];
        spawn = new Vector3[2];


        Pathfinding1();
		Pathfinding2();
    }



    void Pathfinding1()
    {
		//Debug.Log("PATH1: ");
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
                    //Debug.Log("s: " + spawn[spawnCnt]);
                    spawnCnt++;
                    
                }

            }
        }

        numOfTile1 = tileCnt;
		tileCnt = 0;
        path1 = new Vector3[numOfTile1];


        for (int i = 0; i < numOfTile1; i++) {
            path1[i] = path[i];
        }
		/* Debug
        for (int k = 0; k < path1.Length; k++)
        {
            Debug.Log(path1[k]);
        }
		//*/

    }

	void Pathfinding2()
	{
		//Debug.Log("PATH2: ");
		for (int i = MapGenerator.X - 1; i > midLane; i--)
		{
			for (int j = MapGenerator.Z - 1; j >= 0; j--)
			{
				if (MapGenerator.GetTileType(i, j) == 2)
				{
					path[tileCnt] = new Vector3(i, 0, j);
					tileCnt++;
				} else if (MapGenerator.GetTileType(i, j) == 5)
				{
					spawn[spawnCnt] = new Vector3(i, 0, j);
					//Debug.Log("s2: " + spawn[spawnCnt]);
					spawnCnt++;
				}
			}
		}

		numOfTile2 = tileCnt;
		path2 = new Vector3[numOfTile2];
		tileCnt = 0;


		for (int i = 0; i < numOfTile2; i++) {
			path2[i] = path[i];
		}
		/* Debug	
		for (int k = 0; k < path2.Length; k++)
		{
			Debug.Log(path2[k]);
		}
		//*/
	}

}



