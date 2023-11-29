using System.Collections.Generic;
using UnityEngine;

public class ParalysisSpell : MonoBehaviour
{
    public float paralysisDistance = 7f;
    public float animateTime = 0.2f;
    public List<Sprite> paralysisSpellSprites;
    public int numParalysisSpells = 3;
    public bool isCastingParalysis = false;

    private float paralysisTime = 5f;
    private float paralysisTimer = 0f;
    public bool wasCasting = false;
    private int spriteIndex = 0;
    private SpriteRenderer sprite;
    private bool stopAnimating = false;
    private float animateTimer = 0f;
    private bool rotate;
    private float rotationAngle = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = null;
        wasCasting = false;
    }

    private void Update()
    {
        if (isCastingParalysis)
        {
            //Debug.Log("Casting Paralysis!");
            Cast();
        }
    }

    public void Cast()
    {
        if (numParalysisSpells > 0)
        {
            if (wasCasting)
            {
                paralysisTimer += Time.deltaTime;
                if (paralysisTimer >= paralysisTime)
                {
                    isCastingParalysis = false;
                    stopAnimating = true;
                    rotate = false;
                    wasCasting = false;
                    sprite.sprite = null;
                    numParalysisSpells -= 1;
                }
            }
            else
            {
                paralysisTimer = 0f;
                animateTimer = 0;
                spriteIndex = 0;
                wasCasting = true;
                stopAnimating = false;
                rotate = false;
            }
            
        }
        AnimateParalysis();
    }

    void AnimateParalysis() // Animates the paralysis spell from Orpheus
    {
        if (!stopAnimating && numParalysisSpells > 0)
        {
            sprite.sprite = paralysisSpellSprites[spriteIndex];
            animateTimer += Time.deltaTime;
            // Keeps sprite until animateTime is up
            if (animateTimer >= animateTime)
            {
                spriteIndex++;
                animateTimer = 0f;
                if (spriteIndex >= paralysisSpellSprites.ToArray().Length)
                {
                    stopAnimating = true;
                    rotate = true;
                    float scale = 3.5f;
                    gameObject.transform.localScale = new Vector3(scale, scale, scale);
                    animateTimer = 0f;
                }
            }
        }
        else if (rotate)
        {
            animateTimer += Time.deltaTime;
            if (animateTimer >= animateTime)
            {
                // rotate
                Vector3 angles = new Vector3(0, 0, gameObject.transform.eulerAngles.z + rotationAngle);
                gameObject.transform.eulerAngles = angles;
                animateTimer = 0f;
            }
        }
    }
}
