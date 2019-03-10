using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    private Transform player;
    private Vector3 playerPosition;

    [SerializeField]
    float distance = 0.5f;

    [SerializeField]
    float yOffst = 1.0f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        LookAtPlayer();
    }
    private void Update()
    {
        if (playerPosition != player.position)
        {
            LookAtPlayer();
        }
        //Vector3 temp = player.position + (player.forward * distance);
        //transform.position = new Vector3(temp.x, temp.y + yOffst, temp.z);
    }

    public void LookAtPlayer()
    {
        playerPosition = player.position;
        Transform head = player.transform.Find("SteamVRObjects").Find("VRCamera");

        Vector3 temp = head.position + (head.forward * distance);
        transform.position = new Vector3(temp.x, temp.y, temp.z);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, head.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
