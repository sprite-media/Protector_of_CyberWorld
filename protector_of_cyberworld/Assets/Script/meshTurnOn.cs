using System.Collections;
using UnityEngine;

public class meshTurnOn : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TurnOn());
    }
        
    IEnumerator TurnOn()
    {

        yield return new WaitForSeconds(1);
        gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

}
