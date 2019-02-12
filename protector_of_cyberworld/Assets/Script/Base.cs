using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
	private static float hp = 0;
	private static int numEnemiesLeft = 0;
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

	private void Start()
    {
		//*/ assigning temp value
		numEnemiesLeft = 10;
		/*/
		numEnemiesLeft = Spawner.NumTotalEnemy;
		//*/
    }

	private void Win()
	{
		//Display Win text
		Debug.Log("Win");
	}
	private void Lose()
	{
		// Display Lose text
		Debug.Log("Lose");
	}

	// Called when enemy is attacking
	public static void TakeDamage(float amt)
	{
		hp = Mathf.Clamp(hp - amt, 0, hp);
		Debug.Log(hp);
		//Update Health bar
		if (hp == 0)
		{
			GameObject.Find("Base").GetComponent<Base>().Lose();
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
			GameObject.Find("Base").GetComponent<Base>().Win();
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
