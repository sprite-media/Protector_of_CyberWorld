using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class JoystickController : MonoBehaviour
{
    private Player player;

    public SteamVR_Action_Vector2 joystickAction;
    public Transform vrCamera;

    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    private bool isTeleporting;

    private Vector2 AddRotation;
    private bool isRotable;
    private float isRotCD;
    private float isRotCT;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        AddRotation = Vector2.zero;
        isRotCD = 1.0f;
        isRotCT = 1.0f;
        isRotable = true;
        isTeleporting = false;
    }
    private void Update()
    {
        ControlJoystick();
        CooltimeForRotation();
    }

    void ControlJoystick()
    {
        Vector2 joystickValue = joystickAction.GetAxis(SteamVR_Input_Sources.Any);

        if (teleportAction.GetStateDown(SteamVR_Input_Sources.Any))
            isTeleporting = true;

        if (teleportAction.GetStateUp(SteamVR_Input_Sources.Any))
            isTeleporting = false;

        if(!isTeleporting)
        {
            if (joystickValue.x > 0.7f && isRotable)
            {
                AddRotation.y += 90.0f;
                isRotCD = 0.0f;
            }

            if (joystickValue.x < -0.7f && isRotable)
            {
                AddRotation.y -= 90.0f;
                isRotCD = 0.0f;
            }
            if(Mathf.Abs(joystickValue.x) < 0.1)
            {
                isRotCD = isRotCT;
            }
        }
     

        player.trackingOriginTransform.rotation = Quaternion.Euler(transform.rotation.x, vrCamera.rotation.y + AddRotation.y, transform.rotation.z);

    }

    void CooltimeForRotation()
    {
        if (isRotCD > isRotCT)
        {
            isRotable = true;
        }
        else
        {
            isRotable = false;
            isRotCD += Time.deltaTime;
        }
    }
}
