using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    private GameObject enemy;

    public int maxEnmey = 5;
    private int numOfEnemy = 0;
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 10.0f;
    private float timer = 0.0f; //timer
    private float spawnTime;
    private bool hasSpawn = true; //check if enemy has been spawned

	private void Awake()
	{
		enemy = Resources.Load("Enemy", typeof(GameObject)) as GameObject;
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
		Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + enemy.transform.localScale.y/2.0f, transform.position.z);
        GameObject Enemy = Instantiate(enemy, spawnPosition, transform.rotation); //enemy spawned here
        numOfEnemy++;
        //Debug.Log(numOfEnemy);
    }
}
