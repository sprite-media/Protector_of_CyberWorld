using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private static int[,] mapData = null;
	private GameObject[] tile = null;
	private GameObject playerBase = null;
	private static int x;
	public static int X { get { return x; } }
	private static int z;
	public static int Z { get { return z; } }
	

	private void Awake()
	{
		tile = new GameObject[6];
		tile[0] = Resources.Load("Tile", typeof(GameObject)) as GameObject;
		tile[1] = Resources.Load("Path", typeof(GameObject)) as GameObject;
		tile[2] = tile[1];
		/*
		tile[3] = Resources.Load("Tower", typeof(GameObject)) as GameObject;
		tile[4] = Resources.Load("Trap", typeof(GameObject)) as GameObject;
		//*/
		tile[5] = Resources.Load("Spawner", typeof(GameObject)) as GameObject;

		playerBase = Resources.Load("Base", typeof(GameObject)) as GameObject;

		LoadMapData();
	}

	private void LoadMapData()
	{
		if (mapData == null)
		{
			TextAsset mapCSV = Resources.Load("MapData", typeof(TextAsset)) as TextAsset;
			string[] column = mapCSV.text.Split('\n');
			string[] row = column[0].Split(',');
			mapData = new int[column.Length,row.Length];
			x = column.Length;
			z = row.Length;
			GameObject tempBase = null;

			//* convert map data text into int and generate map
			for (int i = 0; i < column.Length; i++)
			{
				row = column[i].Split(',');
				for (int j = 0; j < row.Length; j++)
				{
					mapData[i, j] = int.Parse(row[j]);

					if (mapData[i, j] < 3) // 0 : empty tile   1 : enemy path1    2 : enemy path2
					{
						GameObject temp = (GameObject)Instantiate(tile[mapData[i, j]], new Vector3(i, 0, j), tile[mapData[i, j]].transform.rotation);
						temp.transform.parent = transform;

						// Base
						if (tempBase == null && j == 0 && mapData[i, j] == 1)
						{
							tempBase = (GameObject)Instantiate(playerBase, new Vector3(i+0.5f, 1, j - 1), playerBase.transform.rotation);
						}
					}
					else if (mapData[i, j] < 5) // 3 : Tower    4 : something on enemy path
					{
						//Instantiating Tower/Trap
						GameObject tempB = (GameObject)Instantiate(tile[mapData[i, j]], new Vector3(i, 0, j), tile[mapData[i, j]].transform.rotation);
						tempB.transform.parent = transform;

						//Instantiating floor tile under tower
						int tempIndex = mapData[i, j] == 3 ? 0 : 1;
						if (tempIndex == 1 && (mapData[i, j - 1] == 2 || mapData[i, j + 1] == 2))
							tempIndex = 2;
						GameObject tempF = (GameObject)Instantiate(tile[tempIndex], new Vector3(i, 0, j), tile[mapData[i, j]].transform.rotation);
						tempF.transform.parent = transform;
					}
					else // 5 : Enemy spawner
					{
						GameObject temp = (GameObject)Instantiate(tile[mapData[i, j]], new Vector3(i, 0, j), tile[mapData[i, j]].transform.rotation);
						temp.transform.parent = transform;
					}
				}
			}
			//*/
		}
		else
			return;
	}


	public static int GetTileType(int x, int z)
	{
		return mapData[x,z];
	}
	public static int GetTileType(float x, float z)
	{
		return mapData[(int)x, (int)z];
	}
	public static int GetTileType(Vector3 position)
	{
		return GetTileType(position.x, position.z);
	}
}