using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GotKey key;
    public Sprite openDoor;
    public Sprite closedDoor;

    private SpriteRenderer doorSprite;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        doorSprite.sprite = closedDoor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus")
        {
            if (key.gotKey)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // do pop up screen
        }
    }
}
