using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float damage;
    public bool on = false;

    private void OnTriggerEnter(Collider col)
    {

        if (on && col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage*30);
        }
    }
}
