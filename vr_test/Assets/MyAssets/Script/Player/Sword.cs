using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Hand hand = null;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbed()
    {
        hand = transform.parent.parent.GetComponent<Hand>();
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
    }

    public void Disappear()
    {
        Debug.Log("Disappear");
        transform.parent.GetComponent<Rigidbody>().isKinematic = false;
        hand = null;
        //Particle effect
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
