using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Sword_Line : MonoBehaviour
{
    private float damage = 2.0f;
    private float range = 1.5f;

    private Hand hand = null;
    private bool grabed = false;

    public AudioSource aud;
    private static GameObject particle = null;

    private bool isColliderable = false;
    private float attackRate = 0.2f;
    private float attackCount;

    void Awake()
    {
        attackCount = attackRate;
        if (particle == null)
            particle = Resources.Load("Particle_LightsaberHit", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        if (grabed)
        {
            AttackCooltime();
            Attack();
        }  
    }

    public void Grabbed()
    {
        hand = transform.parent.parent.GetComponent<Hand>();
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
        grabed = true;
    }

    public void Disappear()
    {
        transform.parent.GetComponent<Rigidbody>().isKinematic = false;
        hand = null;
        grabed = false;
        //Particle effect
        //Destroy(transform.parent.gameObject);
    }

    public void Attack()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range, 1))
        {
            //Only instantiate particle for ground
            float dist = Vector3.Distance(hit.point, transform.position);

            if (hit.transform.tag == "Enemy" && isColliderable)
            {
                //aud.Play();
                InstantiateParticle(hit);
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
                attackCount = 0.0f;
            }
            else if (dist < range / 2.0f)
            {
                InstantiateParticle(hit);
            }

          
          
        }
    }

    public void AttackCooltime()
    {
        if (attackCount > attackRate)
            isColliderable = true;
        else
        {
            isColliderable = false;
            attackCount += Time.deltaTime;
        }
    }

    public void InstantiateParticle(RaycastHit _hit)
    {
        //instantiate sword paritlce
        GameObject effect = (GameObject)Instantiate(particle, _hit.point, particle.transform.rotation);
        Destroy(effect, 0.7f);
    }

}
