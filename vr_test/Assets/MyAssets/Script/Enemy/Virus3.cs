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
}
