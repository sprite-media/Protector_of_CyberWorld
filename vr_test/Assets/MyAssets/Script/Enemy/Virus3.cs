using UnityEngine;

public class Virus3 : Enemy
{
	private new void Awake()
	{
		base.Awake();
		hp = 15.0f;
		power = 5.0f;
	}
	protected new void Start()
	{
		base.Start();
		priority = new int[] { -1, 0 };
	}
}
