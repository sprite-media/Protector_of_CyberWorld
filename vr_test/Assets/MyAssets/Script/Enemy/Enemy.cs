using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	private int pathType;
	private int pathIndex = 0;

	private Vector3 target = Vector3.zero;

	private bool isAttacking = false;
	private float attackTimer = 0.0f;
	protected float attackCoolTime = 0.0f;

	protected float hp = 0;
	protected float power = 0.0f;

	protected void Awake()
	{
		pathIndex = 0;
		isAttacking = false;

		pathType = transform.position.x == PathFinding.Path[0][0].x ? 0 : 1;
	}

	protected void Start()
	{
		UpdateTargetPath();
	}

	protected void Update()
	{
		if (!isAttacking)
		{
			if (Vector3.Distance(this.transform.position, target) < 0.1f)
			{
				pathIndex++;
				if (pathIndex >= PathFinding.Path[pathType].Length)
				{
					isAttacking = true;
				}
				else
				{
					UpdateTargetPath();
				}
			}
			Move();
		}
		else
		{
			Attack();
		}
	}

	private void UpdateTargetPath()
	{
		Vector3 temp = PathFinding.Path[pathType][pathIndex];
		target = new Vector3(temp.x, temp.y + transform.localScale.y / 2.0f, temp.z);
	}

	public virtual void Move()
	{
		transform.LookAt(target);
		transform.Translate(Vector3.forward * 5.0f * Time.deltaTime);
		// animation control
	}

	public virtual void Attack()
	{
		if (attackTimer > 2)
		{
			Base.TakeDamage(1);
			attackTimer = 0;
		}
		attackTimer += Time.deltaTime;
		//animation control
	}

	public virtual void Death()
	{
		Base.ReduceNumEnemy();
		//Show Effect
		Destroy(gameObject);
	}

	public void TakeDamage(float dmg)
	{
		hp -= dmg;
		if (hp <= 0.0f)
		{
			hp = 0.0f;
			Death();
		}
	}
}
