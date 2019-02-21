using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public enum WeaponType
    {
        LeftGun,
        RightGun,
        Sword,
        Count
    };
    public WeaponType weaponType = WeaponType.LeftGun;

    private static GameObject[] weapon = null;

    private GameObject spawnedWeapon = null;

    private const float spawnCoolTime = 2.0f;
    private float spawnTimer = 0.0f;

    private void Awake()
    {
        if (weapon == null)
        {
            weapon = new GameObject[(int)WeaponType.Count];
            
            weapon[0] = Resources.Load("LeftGun", typeof(GameObject)) as GameObject;
            weapon[1] = Resources.Load("RightGun", typeof(GameObject)) as GameObject;
            //weapon[2] = Resources.Load("LeftGun", typeof(GameObject)) as GameObject;
        }
    }

    void Update()
    {
        if(spawnedWeapon == null)
        {
            spawnTimer += Time.deltaTime;

            if(spawnTimer >= spawnCoolTime)
            {
                spawnedWeapon = (GameObject)Instantiate(weapon[(int)weaponType], transform.position, transform.rotation);
                spawnedWeapon.transform.parent = transform;
                spawnTimer = 0.0f;
            }
        }

        if (transform.childCount == 0)
            spawnedWeapon = null;
    }
}
