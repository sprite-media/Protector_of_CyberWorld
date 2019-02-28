using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : PlayerBuilding
{
	public static Base BASE { get { return GameObject.Find("Base").GetComponent<Base>(); } }
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
	private void Win()
	{
		//Display Win text
		Debug.Log("Win");
	}
	public override void Death()
	{
		// Display Lose text
		Debug.Log("Lose");
	}

	// Called when enemy is attacking
	public override void TakeDamage(float amt)
	{
		base.TakeDamage(amt);
		Debug.Log(hp);
		//Update Health bar
	}

	public static void SetTotalNumEnemy(int num)
	{
		numEnemiesLeft = num;
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
}
