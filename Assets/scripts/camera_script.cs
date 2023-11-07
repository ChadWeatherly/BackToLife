using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{

    public Transform target; // Reference to the character GameObject
    public Vector3 offset = new Vector3(0f, 0.5f, -5f); // Camera offset
    public float smoothing = 5f; // Camera movement smoothing

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Updates after the Update is called for all gameObjects
    void LateUpdate()
    {
        // Follows main character, Orpheus
        if (target != null)
        {
            // Calculate the desired camera position
            Vector3 desiredPosition = target.position + offset;

            // Use Lerp to smoothly interpolate between the current position and the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothing);
        }
    }
}
