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
 
    }

    new void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
		base.Shoot();
        //GameObject bulletGO = (GameObject)Instantiate(fireEffect, firePoint.position, firePoint.rotation);
        //Bullet bullet = bulletGO.GetComponent<Bullet>();

        //if (bullet != null)
        //{
            //bullet.Seek(target);
        //}
    }

	public override void TakeDamage(float amt)
    {
        hp -= amt;
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

 
}
