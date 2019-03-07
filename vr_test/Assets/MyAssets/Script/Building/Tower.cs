using System.Collections;
using UnityEngine;
using MoenenGames.VoxelRobot;

public class Tower : PlayerBuilding
{
	Weapon[] weapons = null;

    protected Transform target;

	public bool HasTarget {
		get
		{
			if (target != null)
				return true;
			else
				return false;
		}
	}

    public float range = 5.0f;

    public string enemyTag = "Enemy";

    protected Transform partToRatate;
	private bool isDead = false;

    public float turnSpeed = 10f;


    // Start is called before the first frame update
    protected void Start()
    {
		hp = 10.0f;
		InvokeRepeating("UpdateTarget", 0f, 0.5f); // Inorder to not to call UpdateTarget function in every frame.
        partToRatate = transform.Find("Base");
        //firePoint = transform.Find("PartRotation").Find("Sphere").Find("Firepoint");
		weapons = transform.Find("Base").Find("Turret").GetComponentsInChildren<Weapon>();
    }

    // Update is called once per frame
    public void Update()
    {
		if (!isDead)
		{
			UpdateTarget();

			if (target == null)
				return;

			Vector3 dir = target.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			Vector3 rotation = Quaternion.Lerp(partToRatate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
			partToRatate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		}
    }
	public virtual void Shoot()
	{
		foreach (Weapon w in weapons)
		{
			if (w.ReadyToShoot)
			{
				w.Attack();
			}
		}
	}
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
	public override void Death()
	{
		base.Death();
		isDead = true;
		Debug.Log(gameObject.name + " Destroyed.");
		//Destroy(partToRatate.gameObject);
	}

}
