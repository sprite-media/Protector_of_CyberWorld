using UnityEngine;

namespace Hyukin
{
    public class RayGun : GunParent
    {
        LineRenderer gunLine;
        public float effectsDisplayTime = 0.2f; // how long lineRenderer will stay in a scene
        int shootableMask; //enemy or should be shootable

		private static GameObject particle = null;

        private void Start()
        {
            gunLine = GetComponent<LineRenderer>();
			if(particle == null)
				particle = Resources.Load("Particle_LaserHit", typeof(GameObject)) as GameObject;
		}

        public override void Update()
        {
            base.Update();
            if(timer >= reFireTime * effectsDisplayTime)
            {
                DisableEffects();
            }
        }

        public void DisableEffects()
        {
            gunLine.enabled = false;
        }

        protected override void Shoot()
        {
            base.Shoot();

            gunLine.enabled = true;
            Transform shotTransform = transform; 
            gunLine.SetPosition(0, shotTransform.position);
           
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                gunLine.SetPosition(1, hit.point);
				InstantiateParticle(hit);

				if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(damage);
				}
                else if(hit.transform.tag == "Boss")
                {
                    hit.transform.GetComponent<Boss>().TakeDamage(damage);
                }
            }
            else
            {
                gunLine.SetPosition(1, shotTransform.position + shotTransform.forward * range);
            }
        }

		private void InstantiateParticle(RaycastHit hit)
		{
			GameObject tempParticle = (GameObject)Instantiate(particle, hit.point, hit.transform.rotation);
			tempParticle.transform.LookAt(transform, Vector3.up);
			tempParticle.SetActive(true);
			Destroy(tempParticle, 1.4f);
		}
    }
}