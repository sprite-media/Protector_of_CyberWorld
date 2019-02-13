using UnityEngine;

namespace Hyukin
{
    public class GunParent : MonoBehaviour
    {
        public int damage = 1;
        public float range = 100f;

        public float reFireTime = 0.15f;
        protected float timer;
          

        public virtual void Update()
        {
            timer += Time.deltaTime;

            //if (Input.GetButton("Fire1") && timer >= reFireTime) //for now
            //{
            //    Shoot();
            //}
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Shoot();

            }
        }

        protected virtual void Shoot()
        {
            //Debug.Log("Bang!");
            timer = 0;
        }

    }
}