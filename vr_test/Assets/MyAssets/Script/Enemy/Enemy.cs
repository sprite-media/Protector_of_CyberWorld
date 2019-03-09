using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	protected float hp = 0;
    protected float damage = 0.0f;

    public float GetHealth() { return hp; }
    public float GetDamage() { return damage; }

	public virtual void Death()
	{
        Base.Instance.ReduceNumEnemy();
		Destroy(gameObject);
	}

	public virtual void TakeDamage(float dmg)
	{
		hp -= dmg;
		if (hp <= 0.0f)
		{
			hp = 0.0f;
			Death();
		}
	}
}
