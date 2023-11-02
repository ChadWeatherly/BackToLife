using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orpheus_script : MonoBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;
    public Transform spawnPoint;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private float angleDirection;

    public SpriteRenderer characterSprite;
    public List<Sprite> northSprites;
    public List<Sprite> southSprites;
    public List<Sprite> eastSprites;
    public List<Sprite> westSprites;

    public AudioSource footStepAudioSource;
    public AudioSource runningFootStepAudioSource;
    private Vector2 lastPosition;
    public float movementThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
        lastPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        UpdateDirectionSprite();
        PlayFootstepSound();     
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Gets x-direction velocity
        float moveY = Input.GetAxisRaw("Vertical");   // Gets y-direction velocity

        moveDirection = new Vector2(moveX, moveY).normalized;
        if (moveX == 0 && moveY == 0)
        {
            angleDirection = 0; // idle state will keep Orpheus looking forward
        }
        else
        {
            angleDirection = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg + 90;
            // This angle has 0 = South, with 90 = East, ...
        }
        //Debug.Log(angleDirection);

    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveDirection.x * sprintSpeed, moveDirection.y * sprintSpeed);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
    }

    // Updates sprite based on angleDirection
    void UpdateDirectionSprite()
    {
        if (angleDirection >= 315f || angleDirection < 45f)
        {
            characterSprite.sprite = southSprites[0];
        }
        else if (angleDirection >= 45f && angleDirection < 135f)
        {
            characterSprite.sprite = eastSprites[0];
        }
        else if (angleDirection >= 135f && angleDirection < 225f)
        {
            characterSprite.sprite = northSprites[0];
        }
        else
        {
            characterSprite.sprite = westSprites[0];
        }
    }
    private void PlayFootstepSound()
    {

        float movementMagnitude = GetMovementMagnitude();
        if (movementMagnitude > movementThreshold)
        {
            if(Input.GetKey(KeyCode.LeftShift) && !runningFootStepAudioSource.isPlaying){
                if(footStepAudioSource.isPlaying)
                    footStepAudioSource.Pause();
                runningFootStepAudioSource.Play();
            }
            else if (!footStepAudioSource.isPlaying && !Input.GetKey(KeyCode.LeftShift))
            {
                if(runningFootStepAudioSource.isPlaying)
                    runningFootStepAudioSource.Pause();
                Debug.Log("Walking");
                footStepAudioSource.Play();
            }
        }else
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
        lastPosition = new Vector2(transform.position.x, transform.position.y);   
    }
    private float GetMovementMagnitude()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        float magnitude = (currentPosition - lastPosition).magnitude;

        Debug.Log(magnitude);
        return magnitude;
    }
}
