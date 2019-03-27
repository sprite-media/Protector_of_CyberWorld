using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    public float damage = 1;
    public float range = 100f;

    public float reFireTime = 0.15f;
    protected float timer;

    //Charging Laser
    protected bool isCharging = false;
    protected float chargingLaser = 0.0f;
    protected float minChargingLaser = 1.0f;

    private GameObject disappearParticle = null;
    [SerializeField] protected Hand hand;

    public void Awake()
    {
        disappearParticle = transform.parent.Find("Particle_WeaponDestroy").gameObject;
        disappearParticle.SetActive(false);
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;

        if (Time.timeScale != 0)
        {
            if (Input.GetButtonDown("Fire1")) //for now
            {
                isCharging = true;
            }
            else if (Input.GetButtonUp("Fire1")) //for now
            {
                Shoot();
                isCharging = false;
            }

            if (hand != null && hand.grabPinchAction.GetStateDown(hand.handType))
            {
                isCharging = true;
            }

            if (hand != null && hand.grabPinchAction.GetStateUp(hand.handType))
            {
                Shoot();
                isCharging = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1")) //for now
            {
                Shoot();
            }

            if (hand != null && hand.grabPinchAction.GetStateDown(hand.handType))
            {
                Shoot();
            }
        }
    }

    protected virtual void Shoot()
    {
        //Debug.Log("Bang!");
        timer = 0;
    }
    public void Disappear()
    {
        transform.parent.GetComponent<Rigidbody>().isKinematic = false;
        hand = null;
        //Particle effect
        disappearParticle.SetActive(true);
        disappearParticle.transform.parent = null;
        Destroy(disappearParticle, 1.0f);
        Destroy(transform.parent.gameObject);
    }
    public void Grabbed()
    {
        hand = transform.parent.parent.GetComponent<Hand>();
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
    }

}