﻿using UnityEngine;

public class Virus1 : Enemy
{
	private new void Awake()
	{
		base.Awake();
		hp = 3.0f;
		power = 1.0f;
	}
}
