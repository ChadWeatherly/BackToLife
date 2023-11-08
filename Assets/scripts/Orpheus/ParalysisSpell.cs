using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class paralysis_script : MonoBehaviour
{
    public float paralysisDistance;
    public float animateTime = 0.25f;
    private orpheus_script player_script;
    public List<Sprite> paralysisSpellSprites;
    public int numParalysisSpells = 3;
    private float paralysisTime = 5f;
    private float paralysisTimer = 0f;
    private bool wasCasting = false;
    private int spriteIndex = 0;
    private SpriteRenderer sprite;
    private bool stopAnimating = false;
    private float spellTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player_script = GetComponentInParent<orpheus_script>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_script.isCastingParalysis && numParalysisSpells > 0) 
        {
            CastParalysis();
        }
        else // Show no sprite
        {
            sprite.sprite = null;
        }
    }

    void CastParalysis()
    {
        if (wasCasting)
        {
            spellTimer += Time.deltaTime;
            if (spellTimer >= paralysisTime)
            {
                player_script.isCastingParalysis = false;
                stopAnimating = true;
                wasCasting = false;
            }
        }
        else
        {
            spellTimer = 0f;
            paralysisTimer = 0;
            spriteIndex = 0;
            numParalysisSpells -= 1;
            wasCasting = true;
            stopAnimating = false;
        }
        AnimateParalysis();
    }

    void AnimateParalysis()
    {
        if (!stopAnimating)
        {
            sprite.sprite = paralysisSpellSprites[spriteIndex];
            paralysisTimer += Time.deltaTime;
            // Keeps sprite until animateTime is up
            if (paralysisTimer >= animateTime)
            {
                spriteIndex++;
                paralysisTimer = 0f;
                if (spriteIndex >= paralysisSpellSprites.ToArray().Length)
                {
                    stopAnimating = true;
                    sprite.sprite = null;
                }
            }
        }
    }
}
