using UnityEngine;
using UnityEngine.SceneManagement;

public class CaughtScreenManager : MonoBehaviour
{
    // Public variables
    public GameObject caughtScreen; // The panel that acts as the caught screen
    public float delayBeforeRestart = 3f; // Time in seconds before the scene restarts
    public float fadeInDuration = 1f; // Duration of the fade-in effect in seconds

    // Private variables
    private CanvasGroup canvasGroup; // The CanvasGroup component attached to the caught screen
    private float fadeTimer; // Timer to track the fade-in duration
    private bool isFadingIn = false; // Flag to check if the caught screen is currently fading in

    // Start is called before the first frame update
    private void Start()
    {
        // Check if the caught screen is assigned
        if (caughtScreen != null)
        {
            // Get the CanvasGroup component from the caught screen
            canvasGroup = caughtScreen.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                // Set the initial alpha of the CanvasGroup to 0, making it invisible
                canvasGroup.alpha = 0f;
                Debug.Log("Caught screen is set up and ready.");
            }
            else
            {
                // Log an error if the CanvasGroup component is not found
                Debug.LogError("CanvasGroup component not found on " + caughtScreen.name);
            }
        }
        else
        {
            // Log an error if the caught screen is not assigned in the inspector
            Debug.LogError("Caught Screen not assigned in the inspector!");
        }
    }

    // Method to show the caught screen
    public void ShowCaughtScreen()
    {
        // Check if the caught screen is assigned
        if (caughtScreen != null)
        {
            // Reset the fade timer and set the fading flag to true
            fadeTimer = 0f;
            isFadingIn = true;
            Debug.Log("Caught screen fade in started.");
            // Invoke the RestartGame method after a delay
            Invoke("RestartGame", delayBeforeRestart);
        }
        else
        {
            // Log an error if the caught screen is not assigned
            Debug.LogError("Caught screen is not assigned.");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the caught screen is active, the CanvasGroup is assigned, and it's not fully visible yet
        if (isFadingIn && canvasGroup != null && canvasGroup.alpha < 1f)
        {
            // Increase the fade timer
            fadeTimer += Time.deltaTime;
            // Calculate the new alpha value based on the fade duration
            float alpha = Mathf.Clamp01(fadeTimer / fadeInDuration);
            // Set the new alpha value
            canvasGroup.alpha = alpha;
            Debug.Log("Fading in... Alpha: " + alpha);
        }
    }

    // Method to restart the game
    private void RestartGame()
    {
        Debug.Log("Restarting game.");
        // Load the current scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
