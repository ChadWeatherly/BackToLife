using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GotKey key;
    public List<Sprite> spriteList;
    public GameObject textBubble;

    private SpriteRenderer doorSprite;
    private int si; // sprite index
    private float animateTime = 0.3f;
    private float spriteTimer;
    private bool animate;
    public string doorStatus;
    private BoxCollider2D boxCol;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        doorSprite.sprite = spriteList[0];
        spriteTimer = 0;
        si = 1;
        animate = false;
        doorStatus = "closed";
        boxCol = GetComponent<BoxCollider2D>();
        textBubble.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus" && key.gotKey)
        {
            animate = true;
        }
        else if (doorStatus == "closed")
        {
            textBubble.SetActive(true);
        }
    }

    private void Update()
    {
        if (animate)
        {
            AnimateDoor();
        }
        if (doorStatus == "open")
        {
            boxCol.enabled = false;
        }
    }

    private void AnimateDoor()
    {
        spriteTimer += Time.deltaTime;
        if (spriteTimer >= animateTime)
        {
            spriteTimer = 0f;
            doorSprite.sprite = spriteList[si];
            si++;
            if (si >= spriteList.ToArray().Length)
            {
                animate = false;
                doorStatus = "open";
            }
        }
    }
}
