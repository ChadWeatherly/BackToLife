using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ParalysisSpellRings : MonoBehaviour
{
    public float speed;

    private Vector2 target;
    private Transform enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemies").transform;
        target = new Vector2(enemies.position.x, enemies.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
