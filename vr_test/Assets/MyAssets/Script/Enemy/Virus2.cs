using UnityEngine;

public class Virus2 : Enemy
{
	private new void Awake()
	{
		base.Awake();
		hp = 3.0f;
		power = 1.0f;
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<Virus1>() != null)
		{
			Debug.Log("Combine");
		}
	}
}
