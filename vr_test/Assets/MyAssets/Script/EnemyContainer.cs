using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
	private static GameObject[] enemies;
	public static GameObject[] Enemies { get { return enemies; } }
	[SerializeField] string enemyTag;

	private void Awake()
	{
		enemyTag = "Enemy";
		InvokeRepeating("UpdateEnemyList", 0f, 0.5f);
	}
	private void UpdateEnemyList()
	{
		 enemies = GameObject.FindGameObjectsWithTag(enemyTag);
	}
}
