using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

namespace Hyukin
{
    public class GunParent : MonoBehaviour
    {
        public int damage = 1;
        public float range = 100f;

        public float reFireTime = 0.15f;
        protected float timer;

        [SerializeField]private Hand hand;

        public virtual void Update()
        {
            timer += Time.deltaTime;

            if (Input.GetButton("Fire1") && timer >= reFireTime) //for now
            {
                Shoot();
            }
            if (hand != null && hand.grabPinchAction.GetStateDown(hand.handType))
            {
                Shoot();
            }
        }

        protected virtual void Shoot()
        {
            //Debug.Log("Bang!");
            timer = 0;
        }
        public void Disappear()
        {
            Debug.Log("Disappear");
            transform.parent.GetComponent<Rigidbody>().isKinematic = false;
            hand = null;
            //Particle effect
            Destroy(transform.parent.gameObject);
        }
        public void Grabbed()
        {
            hand = transform.parent.parent.GetComponent<Hand>();
            transform.parent.GetComponent<Rigidbody>().useGravity = true;
        }

    }
}