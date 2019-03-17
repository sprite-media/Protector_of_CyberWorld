using UnityEngine;

public class Virus1 : Virus
{
	private new void Awake()
	{
		base.Awake();
		hp = 0.7f;
        damage = 1.0f;
	}

	protected new void Start()
	{
		base.Start();
		priority = new int[]{ -1, 0 };
	}
}
