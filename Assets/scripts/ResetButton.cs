using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject box, spawn;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "orpheusgrippers") // Checks if collision is with the player
        {
            box.transform.position = spawn.transform.position;
        }
    }
}
