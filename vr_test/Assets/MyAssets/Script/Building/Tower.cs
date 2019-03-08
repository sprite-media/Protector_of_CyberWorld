using System.Collections;
using UnityEngine;
using MoenenGames.VoxelRobot;

public class Tower : PlayerBuilding
{
    [SerializeField] bool lookUp;
	Weapon[] weapons = null;
    public AudioSource aud;

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

    protected Transform partToRatate;

    public float turnSpeed = 10f;


    // Start is called before the first frame update
    protected void Start()
    {
		hp = 20.0f;
		InvokeRepeating("UpdateTarget", 0f, 0.5f); // Inorder to not to call UpdateTarget function in every frame.
        partToRatate = transform.Find("Base");
        //firePoint = transform.Find("PartRotation").Find("Sphere").Find("Firepoint");
		weapons = transform.Find("Base").Find("Turret").GetComponentsInChildren<Weapon>();
    }

    // Update is called once per frame
    public void Update()
    {
			if (target == null)
				return;

			Vector3 dir = target.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			Vector3 rotation = Quaternion.Lerp(partToRatate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        if(lookUp)
			partToRatate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        else
            partToRatate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Base.Instance.GetTotalNumEnemy() <= 0)
        {
            target = null;
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
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in EnemyContainer.Enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (Base.Instance.GetTotalNumEnemy() == 1)
        {
            range = 15;
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
		//Debug.Log(gameObject.name + " Destroyed.");
		Destroy(gameObject);
	}

}
