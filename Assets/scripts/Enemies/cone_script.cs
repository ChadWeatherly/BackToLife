using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cone_script : MonoBehaviour
{
    public GameObject player;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player) // Checks if collision is with the player
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
