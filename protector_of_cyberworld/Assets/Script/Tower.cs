using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected Transform target;

    public float range = 5.0f;
    public float fireRate = 1f;
    public float weaponDmg = 1f;
    public float fireCountdown = 0f;

    public string enemyTag = "Enemy";

    public Transform partToRatate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Inorder to not to call UpdateTarget function in every frame.
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
    abstract public void Shoot();
    abstract public void OnDrawGizmosSelected();
    abstract public void PreparedToShoot();
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

}
