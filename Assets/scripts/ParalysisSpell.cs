using System.Collections.Generic;
using UnityEngine;

public class ParalysisSpell : MonoBehaviour
{
    public float paralysisDistance = 20f;
    public float animateTime = 0.25f;
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
        if (wasCasting)
        {
            paralysisTimer += Time.deltaTime;
            if (paralysisTimer >= paralysisTime)
            {
                isCastingParalysis = false;
                stopAnimating = true;
                wasCasting = false;
                sprite.sprite = null;
            }
        }
        else
        {
            paralysisTimer = 0f;
            animateTimer = 0;
            spriteIndex = 0;
            numParalysisSpells -= 1;
            wasCasting = true;
            stopAnimating = false;
        }
        AnimateParalysis();
    }

    void AnimateParalysis() // Animates the paralysis spell from Orpheus
    {
        if (!stopAnimating)
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
                    gameObject.transform.localScale = new Vector3(2, 2, 2);
                }
            }
        }
    }
}
