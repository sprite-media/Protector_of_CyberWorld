using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    private LineRenderer laser;
    private float dist;
    public float lineDrawSpeed = 6.0f;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        laser = transform.Find("Laser").GetComponent<LineRenderer>();        
        fireRate = 0.5f;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        laser.enabled = true;
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, target.position);
    }

    public override void PreparedToShoot()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        else
            laser.enabled = false;

        fireCountdown -= Time.deltaTime;
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
