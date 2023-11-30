using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotKey : MonoBehaviour
{
    public bool gotKey = false;
    public GameObject keyHUD;
    public DoorOpener door;
    private Image HUDimage;
    public Sprite keySprite;
    private Color spriteColor;

    private void Start()
    {
        keySprite = GetComponent<SpriteRenderer>().sprite;
        //spriteColor

        gotKey = false;

        HUDimage = keyHUD.GetComponent<Image>();
        HUDimage.sprite = null;
        HUDimage.color = Color.clear;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus") // Checks if collision is with the player
        {
            gotKey = true;
            HUDimage.sprite = keySprite;
            HUDimage.color = Color.white;
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (door != null && door.doorStatus == "open")
        {
            if (HUDimage != null)
            {
                HUDimage.sprite = null;
            }   
        }
    }
}
