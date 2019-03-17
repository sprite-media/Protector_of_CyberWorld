using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    public float damage;
    public GameObject particle;
    public Transform model;
    public AudioSource aud;
    private void Start()
    {        
        speed = 20.0f;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Enemy")
        {
            GameObject goParticle = (GameObject)Instantiate(particle, col.transform.position, col.transform.rotation);
            col.transform.GetComponent<Enemy>().TakeDamage(damage);
            model.gameObject.SetActive(false);
            Destroy(gameObject,0.2f);
        }
    }
}
