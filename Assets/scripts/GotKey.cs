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
    private Sprite keySprite;
    private Color spriteColor;

    private void Start()
    {
        HUDimage = keyHUD.GetComponent<Image>();
        spriteColor = new Color(HUDimage.color.r,
                                HUDimage.color.g,
                                HUDimage.color.b,
                                1f);
        HUDimage.sprite = null;
        spriteColor = new Color(HUDimage.color.r,
                                HUDimage.color.g,
                                HUDimage.color.b,
                                1f);
        HUDimage.color = new Color(0, 0, 0, 0);
        keySprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus") // Checks if collision is with the player
        {
            gotKey = true;
            HUDimage.sprite = keySprite;
            HUDimage.color = spriteColor;
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (door.doorStatus == "open")
        {
            HUDimage.sprite = null;
        }
    }
}
