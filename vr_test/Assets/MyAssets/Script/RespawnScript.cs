using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    private GameObject[] enemy;
	private int enemyType = 0;

	private int maxEnmey; //max number of enemies per spawner
	private int[] maxVirus = { 30, 20 };
	private int numOfEnemy = 0; // number of spawned enemies
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 10.0f;
    private float timer = 0.0f; //timer
    private float spawnTime;
    private bool hasSpawn = true; //check if enemy has been spawned

	private void Awake()
	{
		maxEnmey = maxVirus[0] + maxVirus[1];
		enemy = new GameObject[2];
		enemy[0] = Resources.Load("V1", typeof(GameObject)) as GameObject;
		enemy[1] = Resources.Load("V2", typeof(GameObject)) as GameObject;
	}
	void Start()
    {
        RandomTime(); //set a spawn timer
        Base.SetTotalNumEnemy(maxEnmey * 2);
    }

    void Update()
    {
        timer += Time.deltaTime; //timing here
        //Debug.Log(timer);
        if (timer >= spawnTime)
        {
            hasSpawn = false;
        }
        if (!hasSpawn && numOfEnemy < maxEnmey)
        {
            timer = 0; //reset timer
            hasSpawn = true; //stop enemy spawning 
            RandomTime(); //reset spawn timer
            EnemySpawn();           
        }
    }
    
    public void RandomTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        //Debug.Log(spawnTime);
    }
    public void EnemySpawn()
    {
        if(numOfEnemy >= maxEnmey)
        {
            hasSpawn = false; //enable to spawn enemy   s
            return;
        }

		// Decide enemy type
		enemyType = Random.Range(0, 2);
		while (maxVirus[enemyType] == 0)
		{
			enemyType = Random.Range(0, enemy.Length);
		}
		maxVirus[enemyType]--;

		// spawn enemy
		//Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + enemy[enemyType].transform.localScale.y/2.0f, transform.position.z);
        Instantiate(enemy[enemyType], transform.position, transform.rotation); //enemy spawned here
        numOfEnemy++;
        //Debug.Log(numOfEnemy);
    }
}
