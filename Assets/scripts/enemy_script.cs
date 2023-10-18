using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class enemy_script : MonoBehaviour
{
    public Transform[] waypoints;  // Array of patrol waypoints, as Transforms
    public Transform spawnPoint;
    public float moveSpeed = 2.5f;
    public float stoppingDistance = 0.1f;
    public float animateTime;
    public GameObject sightCone;
    public GameObject player;

    private int wpi; // WayPoint Index
    private float dist; // Distance between character and next waypoint
    private Rigidbody2D rb; // Our RigidBody (which we want to move)
    private Vector2 wpPos; // Next WayPoint Position

    private float direction; // Angular direction of character, where 0/360 deg is South

    private float playerDist; // Distance to player
    private string status = "calm"; // calm, sus, aggro
    public float susDist = 20.0f; // sus threshold
    public float atkDist = 10.0f; // attack threshold
    public SpriteRenderer coneSprite;
    // Made new colors to let the cone be translucent (final value, alpha, is transparency)
    private readonly Color coneRed = new Color(1, 0, 0, 0.1f);
    private readonly Color coneYellow = new Color(1, 0.92f, 0.016f, 0.1f);
    private readonly Color coneGreen = new Color(0, 1, 0, 0.1f);

    // For changing sprite direction
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

    void Start() // Runs at the start of the game, before any frames
    {
        rb = GetComponent<Rigidbody2D>();
        
        transform.position = spawnPoint.position;
        wpi = 1; // Spawns at WayPoint 0, moves towards WayPoint 1
        wpPos = new Vector2(waypoints[wpi].position.x, waypoints[wpi].position.y);

        si = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player) // Checks if collision is with the player
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Update() // Updates each frame
    {
        // Move Enemy
        Move();
        // Update Cone characteristics (Angle, position, color)
        UpdateCone();
        // Updates based on alert status
        OnStatusChange();
        // Updates sprite based on angle direction
        UpdateDirectionSprites();
    }

    // Method that actually makes Enemy walk
    private void Move()
    {
        // Calculate the direction to the next waypoint
        Vector2 moveDirection = (wpPos - rb.position).normalized;

        // Move the NPC using Rigidbody2D velocity
        rb.velocity = moveDirection * moveSpeed;

        // Calculate direction
        // This angle has 0 = South, with 90 = East, ...
        direction = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90;

        // Check if the NPC has reached the current waypoint
        dist = Vector2.Distance(rb.position, wpPos);

        if (dist <= stoppingDistance)
        {
            // Increment the waypoint index to move to the next waypoint
            wpi++;

            // If the NPC has reached the last waypoint, loop back to the first waypoint
            if (wpi >= waypoints.Length)
            {
                wpi = 0;
            }
            // New position vector
            wpPos = new Vector2(waypoints[wpi].position.x, waypoints[wpi].position.y);
        }
    }

    // Method to update the Cone color and position
    private void UpdateCone()
    {
        // Rotates the cone towards NPC facing direction
        Vector3 angles = new Vector3(0, 0, direction);
        // Changes cone position to go around NPC
        Vector3 position = new Vector3(rb.velocity.normalized.x*2.5f,
            rb.velocity.normalized.y*3.0f,
            0);
        sightCone.transform.eulerAngles = angles;
        sightCone.transform.position = transform.position + position;
        //Debug.Log(position);

        // Distance to player
        playerDist = Vector3.Distance(player.transform.position, transform.position);

        // Updates color of cone based on distance
        if (playerDist <= atkDist) { coneSprite.color = coneRed; status = "aggro"; }
        else if (playerDist <= susDist) { coneSprite.color = coneYellow; status = "sus"; }
        else { coneSprite.color = coneGreen; status = "calm"; }
    }

    // Method to update enemy behavior based on status: calm, sus, aggro
    private void OnStatusChange()
    {
        switch (status)
        {
            case "calm":
                return;
            case "sus":
                return;
            case "aggro":
                return;
        }
    }

    // Updates sprite based on direction
    void UpdateDirectionSprites()
    {
        if (direction >= 315f || direction < 45f)
        {
            nsew = "s";
            currentSpriteGroup = southSprites;
        }
        else if (direction >= 45f && direction < 135f)
        {
            nsew = "e";
            currentSpriteGroup = eastSprites;
        }
        else if (direction >= 135f && direction < 225f)
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
            characterSprite.sprite = southSprites[0];
        }

    }

    // Animates sprite based on directional sprite groups
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
