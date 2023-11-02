using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private List<Sprite> currentSpriteGroup;
    private string nsew; // North, South, East, West
    private string prev_nsew;
    private int si; // Sprite index
    private float timer;
    public float animateTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        UpdateDirectionSprites();

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
    void UpdateDirectionSprites()
    {
        if (angleDirection >= 315f || angleDirection < 45f)
        {
            nsew = "s";
            currentSpriteGroup = southSprites;
        }
        else if (angleDirection >= 45f && angleDirection < 135f)
        {
            nsew = "e";
            currentSpriteGroup = eastSprites;
        }
        else if (angleDirection >= 135f && angleDirection < 225f)
        {
            nsew = "n";
            currentSpriteGroup = northSprites;
        }
        else
        {
            nsew = "w";
            currentSpriteGroup = westSprites;
        }

        // If character is not idle
        if (rb.velocity.magnitude > 0)
        {
            AnimateSprite();
        }
        else
        {
            switch (prev_nsew)
            {
                case "s":
                    characterSprite.sprite = southSprites[0];
                    return;
                case "n":
                    characterSprite.sprite = northSprites[0];
                    return;
                case "e":
                    characterSprite.sprite = eastSprites[0];
                    return;
                case "w":
                    characterSprite.sprite = westSprites[0];
                    return;
            }
        }

    }

    void AnimateSprite()
    {
        // If direction changes
        if (nsew != prev_nsew)
        {
            si = 0;
            timer = 0;
        }
        else
        {
            characterSprite.sprite = currentSpriteGroup[si];
            timer += Time.deltaTime;
            // Keeps sprite until animateTime is up
            if (timer >= animateTime)
            {
                si++;
                timer = 0f;
                if (si >= currentSpriteGroup.ToArray().Length)
                {
                    si = 0;
                }
            }
        }

        prev_nsew = nsew;
    }
}
