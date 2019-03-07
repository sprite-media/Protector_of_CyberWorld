using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : PlayerBuilding
{
	private void Awake()
	{
		hp = 5;
	}

	public override void Death()
	{
		base.Death();
		//Debug.Log(gameObject.name + " Destroyed");
		Destroy(gameObject);
	}
}
