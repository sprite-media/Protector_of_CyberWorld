using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeTower : Tower
{
    private GameObject fireEffect;

    void Start()
    {
        fireEffect = Resources.Load("Granade", typeof(GameObject)) as GameObject;
        firePoint = transform.Find("PartRotation").Find("Firepoint");
    }

    new void Update()
    {
        base.Update();
    }
    public override void PreparedToShoot()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
    public override void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(fireEffect, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
