using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private int numOfTile1 = 0, numOfTile2 = 0;

	private static Vector3[][] path = null;
	public static Vector3[][] Path { get { return path; } }

    [SerializeField] Vector3[] path1;
    [SerializeField] Vector3[] path2;

    void Start()
    {
        InitPath();
        Pathfinding(0);
        Pathfinding(1);
        path1 = new Vector3[numOfTile1];
        for (int i = 0; i < numOfTile1; i++)
        {
            path1[i] = path[0][i];
        }

        path2 = new Vector3[numOfTile2];
        for (int i = 0; i < numOfTile2; i++)
        {
            path2[i] = path[1][i];
        }
    }

    void InitPath()
    {
        for (int i = 0; i < MapGenerator.X; i++)
        {
            for (int j =0; j < MapGenerator.Z; j++)
            {
                if (MapGenerator.GetTileType(i, j) == 1)
                {
                    numOfTile1++;
                }
                else if (MapGenerator.GetTileType(i, j) == 2)
                {
                    numOfTile2++;
                }
            }
        }
        path = new Vector3[2][];
        path[0] = new Vector3[numOfTile1];
        path[1] = new Vector3[numOfTile2];

        
    }

    void Pathfinding(int pathType)
    {
        int pathID = pathType == 0 ? 1 : 2;
        int pathCount = 0;
        Vector3 startPoint;
        for (int i = 0; i < MapGenerator.X; i++)
        {
            if (MapGenerator.GetTileType(i, 0) == pathID)
            {
                
                startPoint = new Vector3(i, 0, 0);
                path[pathType][0] = startPoint;
                
            }
        }

        for (int i = 0; i < path[pathType].Length; i++)
        {
            Vector3 current = path[pathType][pathCount];
            //up
            if (MapGenerator.GetTileType(current.x - 1, current.z) == pathID)
            {
                if (!isExisting(pathType, new Vector3(current.x - 1, 0, current.z)))
                {
                    pathCount++;
                    path[pathType][pathCount] = new Vector3(current.x - 1, 0, current.z);
                }
            }
      
            //down
            if(current.x != 0)
            {
                if (MapGenerator.GetTileType(current.x + 1, current.z) == pathID)
                {
                    if (!isExisting(pathType, new Vector3(current.x + 1, 0, current.z)))
                    {
                        pathCount++;
                        path[pathType][pathCount] = new Vector3(current.x + 1, 0, current.z);
                    }
                }
            }
         
            //left
            if(current.z != 0)
            {
                if (MapGenerator.GetTileType(current.x, current.z - 1) == pathID)
                {
                    if (!isExisting(pathType, new Vector3(current.x, 0, current.z - 1)))
                    {
                        pathCount++;
                        path[pathType][pathCount] = new Vector3(current.x, 0,current.z - 1);
                    }
                }
            }          
            
            //right
            if (MapGenerator.GetTileType(current.x, current.z + 1) == pathID)
            {
                if (!isExisting(pathType, new Vector3(current.x, 0, current.z + 1)))
                {
                    pathCount++;
                    path[pathType][pathCount] = new Vector3(current.x, 0, current.z + 1);
                }
            }
        }
        Array.Reverse(path[pathType], 0, path[pathType].Length);

    }

    bool isExisting(int _pathType, Vector3 _pos)
    {
        for (int i = 0; i < path[_pathType].Length; i++)
        {
            if (path[_pathType][i] == _pos)
            {
                return true;
            }
        }
        return false;
    }
}



