using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    public Transform spawnPoint;

    public void Spawn()
    {
        transform.position = spawnPoint.position;
    }
}
