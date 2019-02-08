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
		if (mapData == null)
		{
			tile = new GameObject[6];
			tile[0] = Resources.Load("Tile", typeof(GameObject)) as GameObject;
			tile[1] = Resources.Load("Path", typeof(GameObject)) as GameObject;
			tile[2] = tile[1];

			tile[3] = Resources.Load("Tower", typeof(GameObject)) as GameObject;
			/*
			  tile[4] = Resources.Load("Trap", typeof(GameObject)) as GameObject;
			//*/
			tile[5] = Resources.Load("Spawner", typeof(GameObject)) as GameObject;

			playerBase = Resources.Load("Base", typeof(GameObject)) as GameObject;

			LoadMapData();
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}

	private void LoadMapData()
	{
		TextAsset mapCSV = Resources.Load("MapData", typeof(TextAsset)) as TextAsset;
		string[] column = mapCSV.text.Split('\n');
		string[] row = column[0].Split(',');
			mapData = new int[column.Length,row.Length];
		x = column.Length;
		z = row.Length;
		GameObject temp = null;

		// convert map data text into int and generate map
		for (int i = 0; i < x; i++)
		{
			row = column[i].Split(',');
			for (int j = 0; j < z; j++)
			{
				mapData[i, j] = int.Parse(row[j]);
				temp = (GameObject)Instantiate(tile[mapData[i, j]], new Vector3(i, 0, j), tile[mapData[i, j]].transform.rotation);
				temp.transform.parent = transform;

				switch (mapData[i, j])
				{
					case 0:// Empty
					case 1:// Path1
					case 2:// Path2
					{
						/*Transform adjustment if needed
						temp.transform.position = new Vector3(temp.transform.position.x, VALUE YOU WANT, temp.transform.position.z);
						//*/

						// Base
						if (j == 0 && mapData[i, j] == 1)
						{
							Instantiate(playerBase, new Vector3(i + 0.5f, 1, j - 1), playerBase.transform.rotation);
						}
							
						break;
					}
					case 3://Tower
					{
						//*Transform adjustment if needed
						temp.transform.position = new Vector3(temp.transform.position.x, 0.5f, temp.transform.position.z);
						//*/
						//Creating tile under the tower
						GameObject tempF = (GameObject)Instantiate(tile[0], new Vector3(i, 0, j), tile[0].transform.rotation);
						tempF.transform.parent = transform;
						break;
					}
					case 4://Trap
					{
						/*Transform adjustment if needed
						temp.transform.position = new Vector3(temp.transform.position.x, VALUE YOU WANT, temp.transform.position.z);
						//*/

						// Create enemy path under the trap and change the map data to enemy path
						int tempIndex = 1;
						if (mapData[i, j - 1] == 2 || mapData[i, j + 1] == 2)
							tempIndex = 2;
						GameObject tempF = (GameObject)Instantiate(tile[tempIndex], new Vector3(i, 0, j), tile[mapData[i, j]].transform.rotation);
						tempF.transform.parent = transform;
						mapData[i, j] = tempIndex;
						break;
					}
					case 5://Spawner
					{
						/*Transform adjustment if needed
						temp.transform.position = new Vector3(temp.transform.position.x, VALUE YOU WANT, temp.transform.position.z);
						//*/
						break;
					}
				}
				//temp = null;
			}
		}
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