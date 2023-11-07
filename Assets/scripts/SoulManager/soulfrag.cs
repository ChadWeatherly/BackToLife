using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soulfrag : MonoBehaviour
{
    public SoulCounter soulFragNum;
    private bool levelComplete = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Orpheus" && !levelComplete)
        {

            soulFragNum.SoulCollection();
            Invoke("ChangeScene", 4);
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelComplete = true;
    }
}
