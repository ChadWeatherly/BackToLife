using UnityEngine;

public class EnemyFootstep : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public float maxDistance = 20f; // Adjust this based on your game's scale

    public AudioSource footstep;


    void Update()
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y);

        float distance = Vector2.Distance(enemyPosition, playerPosition);
        // Adjust the distance thresholds and volume based on your game's scale
        float volume = Mathf.Clamp01(1f - distance / maxDistance);

        footstep.volume = volume;

        if (distance < maxDistance)
        {
            PlayFootstepSound();
        }
        else
        {
            StopFootstepSound();
        }
        Debug.Log("Distance: " + distance + ", Volume: " + volume + ", Is Playing: " + footstep.isPlaying);
    }

    void PlayFootstepSound()
    {
        if (!footstep.isPlaying)
        {
            footstep.Play();
        }
    }

    void StopFootstepSound()
    {
        if (footstep.isPlaying)
        {
            footstep.Pause();
        }
    }
}
