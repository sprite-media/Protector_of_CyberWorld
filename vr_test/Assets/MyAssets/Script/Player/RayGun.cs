using UnityEngine;

namespace Hyukin
{
    public class RayGun : GunParent
    {
        LineRenderer gunLine;
        public float effectsDisplayTime = 0.2f; // how long lineRenderer will stay in a scene
        int shootableMask; //enemy or should be shootable

        private void Start()
        {
            gunLine = GetComponent<LineRenderer>();
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
             
                if (hit.transform.tag == "Enemy")
                {
                    //Debug.Log("takeDDDD");
                    hit.transform.GetComponent<Virus1>().TakeDamage(damage);
                }
            }
            else
            {
                gunLine.SetPosition(1, shotTransform.position + shotTransform.forward * range);
            }
        }
    }
}