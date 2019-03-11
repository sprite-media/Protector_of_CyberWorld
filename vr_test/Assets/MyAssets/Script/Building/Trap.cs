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
        if (DeathEffect)
        {
            DeathEffect.SetActive(true);
            DeathEffect.transform.parent = null;
            Destroy(DeathEffect, 1.5f);
        }
        Destroy(gameObject);
	}
}
