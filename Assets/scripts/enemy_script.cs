using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using Pathfinding;


public class enemy_script : MonoBehaviour
{
    public GameManager gameManager;
    
    // Basic movement parameters and general variables
    public Transform[] waypoints;  // Array of patrol waypoints, as Transforms
    public Transform spawnPoint;
    public float moveSpeed = 1.5f;
    public float stoppingDistance = 0.1f;
    public float stuckDist = 0.25f; // If move less than less, recalculate path
    public float animateTime;
    public GameObject player;

    // For sound
    public float maxSoundDistance = 15f;
    public AudioSource footstep;

    // For finding player
    // Seeker is the component that will help us find a path from A -> B
    private Seeker seeker;

    private int wpi; // WayPoint Index
    private int swpi; // Seeker waypoint index on path
    private float dist; // Distance between character and next waypoint
    private Rigidbody2D rb; // Our RigidBody (which we want to move)
    private Vector2 wpPos; // Next WayPoint Position
    private string pathType;
    private bool patrol = true; // Bool to tell us whether enemy is back on waypoint path
    private bool pathExists = false; // finds whether a path has been completed or not
    private List<Vector3> vpath; // current vector path

    private float direction; // Angular direction of character, where 0/360 deg is South

    public float playerDist = 100f; // Distance to player
    private Vector2 playerPos2D;
    private Vector2 enemyPos2D;
    private string currStatus = "calm"; // calm, sus, aggro
    private string prevStatus = "start";
    private float susDist = 12f; // sus threshold
    private float atkDist = 7.5f; // attack threshold (for now)
    private float caughtDist = 3.5f;

    // For controlling the cone
    public GameObject sightCone;
    private SpriteRenderer coneSprite;
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

    // For paralysis spell
    private ParalysisSpell paralysisSpell;
    //For blackhole spell
    public BlackHoleSpell blackHoleSpell;
    public float gravityDistance = 10f; // The distance within which the gravity effect will be applied
    public float gravityForce = 10f; // The force of the gravity effect
    private Vector2 prevVelocity;

    void Start() // Runs at the start of the game, before any frames
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        paralysisSpell = player.GetComponentInChildren<ParalysisSpell>();
        coneSprite = sightCone.GetComponent<SpriteRenderer>();
        blackHoleSpell = player.GetComponentInChildren<BlackHoleSpell>();
        transform.position = spawnPoint.position;
        // Distance to player
        wpi = 1; // Spawns at WayPoint 0, moves towards WayPoint 1
        wpPos = new Vector2(waypoints[wpi].position.x, waypoints[wpi].position.y);

        si = 0;
        
    }

    void Update() // Updates each frame
    {
        /*if (gameManager.caught)
        {
            rb.velocity = new Vector2(0, 0);
        }*/
        if (playerDist <= caughtDist && currStatus != "paralyzed")
        {
            gameManager.caught = true;
            PauseMenu.GameIsPaused = true;
        }
        //else if (!gameManager.GameIsPaused){
            // Updates alert status based on distance to player
            // Also handles all movement
            UpdateStatus();
            // Update Cone characteristics (Angle, position, color)
            UpdateCone();
            // Updates sprite based on angle direction
            UpdateDirectionSprites();
        //}
    }

    private void FixedUpdate()
    {
        //Debug.Log(playerDist);
        playerPos2D = new Vector2(player.transform.position.x, player.transform.position.y);
        enemyPos2D = new Vector2(transform.position.x, transform.position.y);
        playerDist = Vector2.Distance(playerPos2D, enemyPos2D);        
        
        // Updates Volume of footsteps
        UpdateFootstepVol();
    }

    private void UpdateStatus()
    {
        prevStatus = currStatus;
        if(blackHoleSpell.isCastingBlackHole || (paralysisSpell.isCastingParalysis &&
            (playerDist <= paralysisSpell.paralysisDistance ||
            prevStatus == "paralyzed"))){
            if (blackHoleSpell.isCastingBlackHole)
            {
                Vector2 direction = (blackHoleSpell.transform.position - transform.position).normalized;
                Debug.Log("Direction: " + direction); // Log the direction

                // Define the maximum distance for the force to be applied
                float maxDistance = 5f;

                // Apply the force to objects within the maximum distance
                ApplyForceToObjects(direction, gravityForce, maxDistance);

                prevVelocity = rb.velocity;
                Debug.Log("Velocity: " + rb.velocity); // Log the velocity
            }
            if (paralysisSpell.isCastingParalysis &&
                (playerDist <= paralysisSpell.paralysisDistance ||
                prevStatus == "paralyzed"))
            {
                currStatus = "paralyzed";
                moveSpeed = 0f;
            }
            }
        else if (playerDist <= atkDist) // if aggro
        {
            currStatus = "aggro";
            moveSpeed = 2.25f;
            pathExists = false;
            patrol = false;
            Move(player.transform.position);
            return;
        }
        else if (playerDist <= susDist) // if sus
        {
            currStatus = "sus";
            moveSpeed = 2f;
            patrol = false;

            if (prevStatus != "sus") // if status changes, make new path to player
            {
                pathExists = false;
                pathType = "sus";
                seeker.StartPath(transform.position, player.transform.position,
                    OnPathComplete);
            }
            else if (pathExists) // Otherwise, calculate a new path
            {
                Move(vpath);
            }
        }
        else // if calm
        {
            currStatus = "calm";
            moveSpeed = 1.5f;

            if (prevStatus != "calm") // if status changed to calm 
            {
                if (pathExists) // if a path already exists
                {
                    pathType = "sus"; // continue to follow the suspicious path
                    Move(vpath);
                }
                else // if no path exists, then begin to move back to patrolling
                {
                    pathType = "calm";
                    seeker.StartPath(transform.position, waypoints[wpi].position,
                                        OnPathComplete);
                }
            }
            else if (pathType == "sus") // even if enemy has been calm, continue suspicious path
            {
                if (pathExists) { Move(vpath); }
                else // Once that path has been traversed, move back to patrolling
                {
                    pathType = "calm";
                    seeker.StartPath(transform.position, waypoints[wpi].position,
                                        OnPathComplete);
                }
            }
            else if (pathExists) // This is triggered only if a path back to waypoints exists
            {
                Move(vpath);
            }
            else if (patrol)
            {
                Move(waypoints);
            }
            else
            {
                pathType = "calm";
                seeker.StartPath(transform.position, waypoints[wpi].position,
                                    OnPathComplete);
            }
        }
    }


    
    // Method to update the Cone color and position
    private void UpdateCone()
    {
        if (currStatus == "paralyzed")
        {
            rb.velocity = new Vector2(0, 0);
            coneSprite.color = new Color(0, 0, 0, 0); // makes cone invisible
        }
        else
        {
            // Rotates the cone towards NPC facing direction
            Vector3 angles = new Vector3(0, 0, direction);
            // Changes cone position to go around NPC
            Vector3 position = new Vector3(rb.velocity.normalized.x * 5f,
                rb.velocity.normalized.y * 6.0f,
                0);
            sightCone.transform.eulerAngles = angles;
            sightCone.transform.position = transform.position + position;
            //Debug.Log(position);

            // Updates color of cone based on distance
            switch (currStatus)
            {
                case "calm":
                    coneSprite.color = coneGreen;
                    return;
                case "sus":
                    coneSprite.color = coneYellow;
                    return;
                case "aggro":
                    coneSprite.color = coneRed;
                    return;
            }
        }

    }

    private void OnPathComplete (Path p)
    {
        pathExists = true;
        vpath = seeker.GetCurrentPath().vectorPath;
        swpi = 0;
    }

    // Method that actually makes Enemy walk
    private void Move(Transform[] wpArray) // Takes in an array of Transform objects
    {
        // Calculate the direction to the next waypoint
        // New position vector of next waypoint
        wpPos = new Vector2(wpArray[wpi].position.x, wpArray[wpi].position.y);
        Vector2 moveDirection = (wpPos - rb.position).normalized;

        // Move the NPC using Rigidbody2D velocity
        rb.velocity = moveDirection * moveSpeed;

        // Calculate direction
        // This angle has 0/360 = South, with 90 = East, ...
        direction = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90;

        // Check if the NPC has reached the current waypoint
        dist = Vector2.Distance(rb.position, wpPos);

        if (dist <= stoppingDistance)
        {
            // Increment the waypoint index to move to the next waypoint
            wpi++;

            // If the NPC has reached the last waypoint, loop back to the first waypoint
            if (wpi >= wpArray.Length)
            {
                wpi = 0;
            }
        }
    }

    // Method that actually makes Enemy walk
    // This is an "overload" of the function Move(),
    // Which behaves differently depending on input type
    private void Move(List<Vector3> wpArray) // Takes in a list of 3D vectors (outputs of seeker)
    {
        //Debug.Log(wpArray.Count);
        // Calculate the direction to the next waypoint
        // New position vector of next waypoint
        wpPos = new Vector2(wpArray[swpi].x, wpArray[swpi].y);
        Vector2 moveDirection = (wpPos - rb.position).normalized;

        // Move the NPC using Rigidbody2D velocity
        rb.velocity = moveDirection * moveSpeed;

        // Calculate direction
        // This angle has 0/360 = South, with 90 = East, ...
        direction = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90;

        // Check if the NPC has reached the current waypoint
        dist = Vector2.Distance(rb.position, wpPos);

        if (dist <= stoppingDistance)
        {
            // Increment the waypoint index to move to the next waypoint
            swpi++;

            // If the NPC has reached the last waypoint,
            // it means they are back on their patrolling position
            if (swpi >= wpArray.Count)
            {
                pathExists = false;
                if (pathType == "calm") { patrol = true; }
                else {patrol = false; }
            }
        }
    }

    // Method that actually makes Enemy walk
    // This is an "overload" of the function Move(),
    // Which behaves differently depending on input type
    private void Move(Vector3 target) // Takes in a single 3D vector to move towards (only during aggro)
    {
        // New position vector of next waypoint
        wpPos = new Vector2(target.x, target.y);
        
        // Check if the NPC has reached the current waypoint
        dist = Vector2.Distance(rb.position, wpPos);

        if (dist > stoppingDistance)
        {
            Vector2 moveDirection = (wpPos - rb.position).normalized;

            // Move the NPC using Rigidbody2D velocity
            rb.velocity = moveDirection * moveSpeed;

            // Calculate direction
            // This angle has 0/360 = South, with 90 = East, ...
            direction = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90;
        }
        else { return; }
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

    void UpdateFootstepVol()
    {
        
        float volume = Mathf.Clamp01(1f - playerDist / maxSoundDistance);
        footstep.volume = volume;
        //volume *= AudioManager.GlobalVolume //this is once we added a slider for the volume
        if (playerDist <= maxSoundDistance && rb.velocity.magnitude > .0001)
        {
            footstep.volume = volume;
            if (!footstep.isPlaying)
            {
                footstep.Play();
            }
        }
        else if (footstep.isPlaying)
        {
            footstep.Pause();
        }
        
    }
    private void ApplyForceToObjects(Vector2 direction, float gravityForce, float maxDistance)
    {
    // Get all objects in the scene with a Rigidbody
        Rigidbody2D[] allObjects = FindObjectsOfType<Rigidbody2D>();

        foreach (Rigidbody2D obj in allObjects)
        {
            // Calculate the distance between the black hole and the object
            float distance = Vector2.Distance(blackHoleSpell.transform.position, obj.transform.position);

            // If the object is within the maximum distance, apply the force
            if (distance <= maxDistance)
            {
                obj.AddForce(direction * gravityForce, ForceMode2D.Impulse);
            }
        }
    }

}
