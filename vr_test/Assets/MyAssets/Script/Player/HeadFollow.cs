using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollow : MonoBehaviour
{
    private Transform head = null;

    private void Awake()
    {
        head = transform.parent.parent.Find("VRCamera");
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, head.rotation.eulerAngles.y , transform.rotation.eulerAngles.z));
    }
}
