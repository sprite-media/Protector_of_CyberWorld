using UnityEngine;

public class Virus3 : Enemy
{
	private new void Awake()
	{
		base.Awake();
		hp = 15.0f;
		power = 5.0f;
	}
}
