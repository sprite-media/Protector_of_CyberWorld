using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	private Vector3[] path = null;
    private Vector3[] path1 = null;
	private Vector3[] path2 = null;

    private int tileCnt = 0;
    private int numOfTile1 = 0, numOfTile2 = 0;
    private int midLane;

	private static Vector3[][] totalPath = null;
	public static Vector3[][] Path { get { return totalPath; } }

    void Start()
    {
		//Finding mid point
        for (int i = 0; i < MapGenerator.X; i++)
        {
            for (int j = 0; j < MapGenerator.Z; j++)
            {
                if (j == 0 && MapGenerator.GetTileType(i, j) == 1)
                {
                    midLane = i;
                }
            }
        }

        // Calculating total path size and create array
        int pathSize = MapGenerator.Z * MapGenerator.X;
        path = new Vector3[pathSize];

        Pathfinding1();
		Pathfinding2();

		totalPath = new Vector3[2][];
		totalPath[0] = new Vector3[numOfTile1];
		totalPath[1] = new Vector3[numOfTile2];
		for (int i = 0; i < numOfTile1; i++)
		{
			totalPath[0][i] = path1[i];
		}
		for (int i = 0; i < numOfTile2; i++)
		{
			totalPath[1][i] = path2[i];
		}
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
                }

            }
        }

        numOfTile1 = tileCnt;
		tileCnt = 0;
        path1 = new Vector3[numOfTile1];


        for (int i = 0; i < numOfTile1; i++) {
            path1[i] = path[i];
        } 
    }

	void Pathfinding2()
	{
		for (int i = MapGenerator.X - 1; i > midLane; i--)
		{
			for (int j = MapGenerator.Z - 1; j >= 0; j--)
			{
				if (MapGenerator.GetTileType(i, j) == 2)
				{
					path[tileCnt] = new Vector3(i, 0, j);
					tileCnt++;
				}
			}
		}

		numOfTile2 = tileCnt;
		path2 = new Vector3[numOfTile2];
		tileCnt = 0;


		for (int i = 0; i < numOfTile2; i++) {
			path2[i] = path[i];
		}
	}

}



