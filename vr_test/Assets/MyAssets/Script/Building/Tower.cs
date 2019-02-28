using System.Collections;
using UnityEngine;

public abstract class Tower : PlayerBuilding
{
    protected Transform target;

    public float range = 5.0f;
    public float weaponDmg = 1f;

    public float fireRate = 0.5f;
    public float fireCountdown = 0f;

    public string enemyTag = "Enemy";

    protected Transform partToRatate;
    protected Transform firePoint;

    public float turnSpeed = 10f;

    abstract public void Shoot();
    abstract public void OnDrawGizmosSelected();
    abstract public void PreparedToShoot();

    // Start is called before the first frame update
    protected void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Inorder to not to call UpdateTarget function in every frame.
        partToRatate = transform.Find("PartRotation");
        firePoint = transform.Find("PartRotation").Find("Sphere").Find("Firepoint");
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRatate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRatate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        PreparedToShoot();
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
		Debug.Log(gameObject.name + " Destroyed.");
		Destroy(gameObject);
	}

}
