using UnityEngine;

public class Virus3 : Virus
{
	private new void Awake()
	{
		base.Awake();
		hp = 5.0f;
        damage = 5.0f;
		detectRange = 3.0f;
		attackRange = 1.5f;
	}
	protected new void Start()
	{
		base.Start();
		priority = new int[] { -1, 0 };
	}
	public override void Death()
	{
		particle.SetActive(true);
		particle.transform.parent = null;
		if (audio)
			audio.Play();
		Destroy(particle, 3);
		if (CanAffectTotalNumber)
		{
			Base.Instance.ReduceNumEnemy();
			Base.Instance.ReduceNumEnemy();
			CanAffectTotalNumber = false;
		}
		Destroy(gameObject);
	}
}
