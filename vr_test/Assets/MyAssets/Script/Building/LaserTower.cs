using System.Collections;
using UnityEngine;

public class LaserTower : Tower
{
	//private static GameObject particle = null;
    //private LineRenderer laser;
    //private float dist;
    //public float lineDrawSpeed = 6.0f;
    //public float lineCool = 1.0f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        hp = 3.0f;

		//if(particle == null)
			//particle = Resources.Load("Particle_LaserHit", typeof(GameObject)) as GameObject;
	}

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        ///Debug.Log("SSSSS");
        //laser.enabled = true;
        //laser.SetPosition(0, firePoint.position);
        //laser.SetPosition(1, target.position);

		//Ray ray = new Ray(firePoint.position, target.position);
		//RaycastHit hit;
		//GameObject tempParticle;
		///tempParticle = (GameObject)Instantiate(particle, target.position, target.rotation);
		//tempParticle.SetActive(true);
		//Destroy(tempParticle, 1.4f);
		//if (Physics.Raycast(ray, out hit))
		//{
			//tempParticle.transform.position = hit.point;
		//}

        //target.GetComponent<Enemy>().TakeDamage(weaponDmg);
        //StartCoroutine("TurnOffLaser");
	}

    //IEnumerator TurnOffLaser()
    //{
        //yield return new WaitForSeconds(0.1f);
        //laser.enabled = false;
	//}


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
