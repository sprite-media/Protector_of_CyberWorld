using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_NumEnemy : MonoBehaviour
{
	private static HUD_NumEnemy _instance;
	public static HUD_NumEnemy instance { get { return _instance; } }

	private MeshRenderer[] numPlane = null;

	private void Awake()
	{
		_instance = this;

		numPlane = new MeshRenderer[3];
		numPlane[0] = transform.Find("1").GetComponent<MeshRenderer>();
		numPlane[1] = transform.Find("10").GetComponent<MeshRenderer>();
		numPlane[2] = transform.Find("100").GetComponent<MeshRenderer>();
	}

	public void UpdateNumEnemy(int num)
	{
		int hundred = num / 100;
		int ten = (num / 10) % 10;
		int one = num % 10;


		numPlane[0].material.mainTextureOffset = offset(one);
		numPlane[1].material.mainTextureOffset = offset(ten);
		numPlane[2].material.mainTextureOffset = offset(hundred);
	}
	private Vector2 offset(int num)
	{
		float y = 2 - (int)(num/4);
		float x = num - (2 - y) * 4;
		return new Vector2(x * 0.25f, y * 0.333f);
	}
}