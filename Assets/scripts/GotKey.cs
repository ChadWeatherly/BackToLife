using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotKey : MonoBehaviour
{
    public bool gotKey = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus") // Checks if collision is with the player
        {
            gotKey = true;
            Destroy(gameObject);
        }

    }
}
