using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class orpheus_script : MonoBehaviour
{
    // Basic parameters for Orpheus and his movement
    public float moveSpeed = 4f;
    public float sprintSpeed = 8f;
    public Transform spawnPoint;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private float angleDirection;

    // Info for animation
    public float animateTime = 0.25f;
    private string nsew; // North, South, East, West
    private string prev_nsew;
    private int si; // Sprite index
    private float animateTimer;
    public SpriteRenderer characterSprite;
    public List<Sprite> northSprites;
    public List<Sprite> southSprites;
    public List<Sprite> eastSprites;
    public List<Sprite> westSprites;
    private List<Sprite> animationSprites;
    private Sprite restingSprite;

    // For spells
    private ParalysisSpell paralysisSpell;

    public SoulDoors openDoors;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
        rb = GetComponent<Rigidbody2D>();
        paralysisSpell = GetComponentInChildren<ParalysisSpell>();
        prev_nsew = "none";
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        AnimateSprite();

    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;                                // for smother player motion -de
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

        // Paralysis Spell Casting
        if (Input.GetKey(KeyCode.Space)) {
            paralysisSpell.isCastingParalysis = true;
        }

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
    void AnimateSprite()
    {
        if (angleDirection >= 315f || angleDirection < 45f)
        {
            animationSprites = southSprites;
            restingSprite = southSprites[0];
            nsew = "south";
        }
        else if (angleDirection >= 45f && angleDirection < 135f)
        {
            animationSprites = eastSprites;
            restingSprite = eastSprites[0];
            nsew = "east";
        }
        else if (angleDirection >= 135f && angleDirection < 225f)
        {
            animationSprites = northSprites;
            restingSprite = northSprites[0];
            nsew = "north";
        }
        else
        {
            animationSprites = westSprites;
            restingSprite = westSprites[0];
            nsew = "west";
        }

        // If direction changes
        if (rb.velocity.magnitude == 0)
        {
            characterSprite.sprite = restingSprite;
        }
        else if (nsew != prev_nsew)
        {
            si = 0;
            animateTimer = 0;
        }
        else
        {
            characterSprite.sprite = animationSprites[si];
            animateTimer += Time.deltaTime;
            // Keeps sprite until animateTime is up
            if (animateTimer >= animateTime)
            {
                si++;
                animateTimer = 0f;
                if (si >= animationSprites.ToArray().Length)
                {
                    si = 0;
                }
            }
        }

        prev_nsew = nsew;
    }

    private void OnTriggerEnter2D(Collider2D collision)             // checks if collision has a trigger -deb
    {
        //if (collision.gameObject.CompareTag("Crystal"))             // checks the tag of the object collided with -deb
        //{
        //    crystalCounter.CrystalCollection();                     // if tag is Crystal goes to update the counter -deb
        //    Destroy(collision.gameObject);                          // gets rid of crystal Orpheus "picked up" -deb
        //}
        //else if (collision.gameObject.CompareTag("Soul"))           // checks the tag of the object collided with -deb
        //{
        //    memCount.MemoryCollection(memRegained);                     // if tag is Soul goes to give Orpheus 25% memory -deb
        //}
    }
}
