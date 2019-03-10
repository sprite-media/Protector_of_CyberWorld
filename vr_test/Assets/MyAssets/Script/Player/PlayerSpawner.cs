using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject player;
    public GameObject teleporting;


    private void Awake()
    {
        if (GameObject.Find("Player") == null)
        {
            Instantiate(player, spawnPosition.position, spawnPosition.rotation);
            Instantiate(teleporting, spawnPosition.position, spawnPosition.rotation);
        }
        else
            return;
    }
}
