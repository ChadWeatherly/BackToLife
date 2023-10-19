using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private AudioSource footStepAudioSource;
    private Vector2 lastPosition;
    public float movementThreshold = 0.1f;

    private void Start()
    {
        footStepAudioSource = GetComponent<AudioSource>();
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
            if (!footStepAudioSource.isPlaying)
            {
                footStepAudioSource.Play();
            }
        }
        else
        {
            if (footStepAudioSource.isPlaying)
            {
                footStepAudioSource.Pause();
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
