using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootIfGrabbed : MonoBehaviour
{
    private SimpleShoot simpleShoot;
    private OVRGrabbable ovrGrabbable;
    public OVRInput.Button shootingBtn;

    void Start()
    {
        simpleShoot = GetComponent<SimpleShoot>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (ovrGrabbable.isGrabbed && OVRInput.GetDown(shootingBtn, ovrGrabbable.grabbedBy.GetController()))
        {
            //Shoot
            simpleShoot.TriggerShot();
        }
    }
}
