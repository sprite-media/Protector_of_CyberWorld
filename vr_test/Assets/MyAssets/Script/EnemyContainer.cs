using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
	private static GameObject[] enemies;
	public static GameObject[] Enemies { get { return enemies; } }
	public GameObject[] en;
	[SerializeField] string enemyTag;

	private void Awake()
	{
		enemyTag = "Enemy";
		InvokeRepeating("UpdateEnemyList", 0f, 0.5f);
	}
	private void UpdateEnemyList()
	{
		enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		en = enemies;
	}
}
