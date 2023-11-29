using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateButton : MonoBehaviour
{
    public bool gateOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BoxDetector") // Checks if collision is with the player
        {
            gateOpen = true;
            Destroy(gameObject);
        }
    }

}
