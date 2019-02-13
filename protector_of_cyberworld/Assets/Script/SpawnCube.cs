using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject cube;
    
    public void spawnCube()
    {
        Instantiate(cube);
    }
}
