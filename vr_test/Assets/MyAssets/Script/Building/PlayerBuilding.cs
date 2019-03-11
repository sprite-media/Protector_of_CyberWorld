using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    [SerializeField] protected AudioSource audio;
    [SerializeField] protected GameObject DeathEffect;
	protected float hp = 0;
    public int number = 0;

    public virtual void TakeDamage(float amt)
	{
		hp = Mathf.Clamp(hp - amt, 0, hp);
		//Update Health bar
		if (hp <= 0)
		{
			Death();
		}
	}
    public virtual void Death()
    {
        Boss.Instance.UpdateTargetList(number);
        audio.Play();
    }
}
