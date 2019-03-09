using UnityEngine;

public class BossHandColliderCheck : MonoBehaviour
{
    public GameObject[] cols;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tower" || other.tag == "Trap" || other.tag == "Base")
        {
            other.GetComponent<PlayerBuilding>().TakeDamage(Boss.Instance.GetDamage());
        }
    }
}
