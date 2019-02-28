using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Base : MonoBehaviour
{
    public static float hp = 0;
	public static int numEnemiesLeft = 0;
    
	//UI text for win
	//UI text for lose
	//UI for hp
	//UI for resource
	//UI for numEnemiesLeft

	private void Awake()
	{
        if (hp != 0)// only 1 base can exist in hierarchy
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			hp = 10;// temp value
			gameObject.name = "Base";
		}
	}
    public static void SetTotalNumEnemy(int num)
    {
        numEnemiesLeft = num;
    }

	// Called when enemy is attacking
	public static void TakeDamage(float amt)
	{
		hp = Mathf.Clamp(hp - amt, 0, hp);
		Debug.Log(hp);
		//Update Health bar
		if (hp == 0)
		{
			//ameObject.Find("Base").GetComponent<Base>().Lose();
		}
	}

	// Called when an enemy is dead
	public static void ReduceNumEnemy()
	{
		numEnemiesLeft--;
        //Debug.Log(numEnemiesLeft);
        //Update indicator
        if (numEnemiesLeft == 0)
		{
			//GameObject.Find("Base").GetComponent<Base>().Win();
		}
	}


	/*	For Test
	private float testTimer = 1.0f;
	private float testCounter = 0.0f;
	private void Update()
	{
		testCounter += Time.deltaTime;
		if (testCounter >= testTimer)
		{
			testCounter = 0.0f;
			
			//TakeDamage(1.0f);
			
			//ReduceNumEnemy();
			
		}
	}
	//*/
}
