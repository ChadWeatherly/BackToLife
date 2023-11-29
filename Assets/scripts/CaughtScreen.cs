using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaughtScreen : MonoBehaviour
{
    public GameManager gameManager;
    public float fadeInDuration = 3.5f; // Duration of the fade-in effect in seconds
    private float fadeTimer = 0f; // Timer to track the fade-in duration
    public bool hasBeenCaught = false;
    public Image image;

    //private void Start()
    //{
    //    fadeTimer = 0f;
    //    hasBeenCaught = false;
    //    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    //}

    private void OnEnable()
    {
        hasBeenCaught = true;
    }

    private void Update()
    {
        print(fadeTimer);
        if (hasBeenCaught) 
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeInDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    
        }
        if (fadeTimer >= fadeInDuration)
        {
            gameManager.isPaused = true;
        }

    }

    public void RestartLvl()
    {
        // Load the current scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
