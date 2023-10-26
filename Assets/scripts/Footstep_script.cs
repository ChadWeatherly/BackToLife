using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource footStepAudioSource;
    public AudioSource runningFootStepAudioSource;
    private Vector2 lastPosition;
    public float movementThreshold = 0.1f;

    private void Start()
    {
        lastPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        PlayFootstepSound();
        lastPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private void PlayFootstepSound()
    {
        float movementMagnitude = GetMovementMagnitude();
        if (movementMagnitude > movementThreshold)
        {
            if(Input.GetKey(KeyCode.LeftShift) && !runningFootStepAudioSource.isPlaying){
                Debug.Log("Running");
                runningFootStepAudioSource.Play();
            }
            else if (!footStepAudioSource.isPlaying && !Input.GetKey(KeyCode.LeftShift))
            {
                if(runningFootStepAudioSource.isPlaying)
                    runningFootStepAudioSource.Pause();
                Debug.Log("Walking");
                footStepAudioSource.Play();
            }
        }
        else
        {
            if (footStepAudioSource.isPlaying)
            {
                Debug.Log("Stop walking");
                footStepAudioSource.Pause();
            }
            if (runningFootStepAudioSource.isPlaying)
            {
                Debug.Log("Stop running");
                runningFootStepAudioSource.Pause();
            }
        }
    }

    private float GetMovementMagnitude()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        float magnitude = (currentPosition - lastPosition).magnitude;

        // Debug information to the console
        return magnitude;
    }
}
