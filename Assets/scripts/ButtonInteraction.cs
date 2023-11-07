using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    public GameObject Wall; // Reference to the wall object to disable.

    private bool wallEnabled = true; // Check if the wall is currently enabled.

    private bool isPressed = false;

    private Vector3 originalRotation;

    // Update is called when the button is clicked.
    private void Start()
    {
        originalRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPressed) // Replace with your input method (e.g., mouse click, touch).
        {
            isPressed = true;
            if (wallEnabled)
            {
                Wall.SetActive(false); // Disable the wall.
            }
            else
            {
                Wall.SetActive(true); // Enable the wall.
            }
            wallEnabled = !wallEnabled;
            Flipbutton();
            
        }
    }

    private void Flipbutton()
    {
        Vector3 newRotation = originalRotation + new Vector3(180f, 0f, 0f);
        transform.eulerAngles = newRotation;

    }
}