using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HUD_HeadFollow : MonoBehaviour
{
	private Transform follow
	{
		get
		{
			return Player.instance.hmdTransform.Find("HUD_Follow");
		}
	}
	private Vector3 temp_pos;
	private Quaternion temp_rot;

	private float speed;
	private float distance;
	private const float distance_start = 0.01f;
	private const float distance_stop = 0.0005f;
	private Vector3 dir;
	private bool isMoving = false;

	private void Start()
	{
		UpdateTransform();
		temp_pos = Player.instance.trackingOriginTransform.position;
		temp_rot = Player.instance.trackingOriginTransform.rotation;
	}

	private void Update()
	{
		if (Player.instance.trackingOriginTransform.position != temp_pos ||
			Player.instance.trackingOriginTransform.rotation != temp_rot)
		{
			UpdateTransform();
		}

		dir = follow.position - transform.position;
		distance = dir.magnitude;
		speed = Mathf.Log(Mathf.Abs(distance * 5)+1);

		if (distance > distance_start)
			isMoving = true;
		else if (distance < distance_stop)
			isMoving = false;

		if(isMoving)
			transform.position = transform.position + dir.normalized * speed * Time.unscaledDeltaTime;

		transform.rotation = follow.rotation;
	}
	public void UpdateTransform()
	{
		transform.position = follow.position;
		transform.rotation = follow.rotation;
	}
}
