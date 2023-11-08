using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class orpheus_script : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float sprintSpeed = 8f;
    public Transform spawnPoint;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private float angleDirection;

    public SpriteRenderer characterSprite;
    public List<Sprite> northSprites;
    public List<Sprite> southSprites;
    public List<Sprite> eastSprites;
    public List<Sprite> westSprites;

    //public ParalysisSpellCasting castingSpell;
    //public AudioSource casting;
    public bool isCastingParalysis = false;

    public SoulDoors openDoors;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        UpdateDirectionSprite();

    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;                                // for smother player motion -deb

        //if (Input.GetKeyDown(KeyCode.DownArrow) && Updater.paralysisSpells > 0) // checks for spell casting key and if any
        //{                                                                            // spells left -deb
        //    //castingSpell.SetSpellAnimator();                                    // goes to animate and cast the spell -deb
        //    GetComponents<AudioSource>()[0].Play();                             // plays the spell sound -deb
        //}
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Gets x-direction velocity
        float moveY = Input.GetAxisRaw("Vertical");   // Gets y-direction velocity
        Debug.Log(moveX);
        Debug.Log(moveY);

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
            isCastingParalysis = true;
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
