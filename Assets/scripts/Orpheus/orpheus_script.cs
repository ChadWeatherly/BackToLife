using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public CrystalCounter crystalCounter;
    public MemoryCounter memCount;

    public ParalysisSpellRings spell;
    public Animator spellAnima;
    public ParalysisSpellCasting castingSpell;

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

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            spellAnima.SetBool("isCastingParalysis", true);
            castingSpell.CastSpell();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crystal"))
        {
            crystalCounter.CrystalCollection();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Soul"))
        {
            memCount.MemoryCollection(0.25f);
        }
    }
}
