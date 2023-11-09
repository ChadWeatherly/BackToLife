using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParalysisSpellText : MonoBehaviour
{
    public GameObject spell;
    private TextMeshProUGUI paralysisSpellTxt;                           // Text Mesh Pro Canvas Spell -deb
    private ParalysisSpell paralysisSpell;
    private int numSpells;

    // Start is called before the first frame update
    void Start()
    {
        paralysisSpellTxt = GetComponent<TextMeshProUGUI>();             // Accesses text Mesh Pro Canvas Spell -deb
        paralysisSpellTxt.text = "Spells: " + 3.ToString();
        paralysisSpell = spell.GetComponent<ParalysisSpell>();
        
    }

    // Update is called once per frame
    void Update()
    {
        numSpells = paralysisSpell.numParalysisSpells;
        paralysisSpellTxt.text = "Spells: " + numSpells.ToString();
    }
}
