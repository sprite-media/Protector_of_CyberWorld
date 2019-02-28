using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeTower : Tower
{
    [SerializeField]private GameObject fireEffect;

    new void Start()
    {
        base.Start();
        hp = 4.0f;
        fireEffect = Resources.Load("Granade", typeof(GameObject)) as GameObject;
        fireRate = 1.0f;
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

    public override void TakeDamage(int _dmg)
    {
        hp -= _dmg;
        if (hp <= 0)
        {
            //Effect for disappearing tower
            //Instantiate effect.........
            StartCoroutine("waitForDeath");
        }
    }
    IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(0.2f);
        Death();
    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
