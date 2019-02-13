using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus1 : MonoBehaviour
{
	private Vector3[] path;

	private int pathIndex = 0;
	private Vector3 target;
	bool isAttacking;
	float attackTimer = 0;
    

	private void Awake()
	{
		pathIndex = 0;
		isAttacking = false;

		if (this.transform.position.x == PathFinding.Path1[0].x)
			path = PathFinding.Path1;
		else
			path = PathFinding.Path2;

        /*
        for (int i = 0; i < path.Length; i++)
        {
            Debug.Log(path[i]);
        }
        //*/
	}

    // Start is called before the first frame update
	void Start()
	{
		target = new Vector3(path[pathIndex].x , path[pathIndex].y + transform.localScale.y/2.0f, path[pathIndex].z);
	}

    // Update is called once per frame
	void Update()
	{
		if (!isAttacking)
		{
			if (Vector3.Distance(this.transform.position, target) < 0.1f)
			{
				pathIndex++;
				if (pathIndex >= path.Length)
				{
					isAttacking = true;
				}
				else
                {

					target = new Vector3(path[pathIndex].x, path[pathIndex].y + transform.localScale.y / 2.0f, path[pathIndex].z);
				}
			}
			Move();
        }
		else
		{
			Attack();
		}
    }

    void Move()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * 5.0f * Time.deltaTime);
    }

    void Attack()
	{
        if (attackTimer > 2)
        {
            Base.TakeDamage(1);
            attackTimer = 0;
        }
        attackTimer += Time.deltaTime;
    }
}
