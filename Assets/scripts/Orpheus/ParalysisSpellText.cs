using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParalysisSpellText : MonoBehaviour
{
    public GameObject spell;
    private TextMeshProUGUI paralysisSpellTxt;                           // Text Mesh Pro Canvas Spell -deb
    private paralysis_script paralysisScript;
    private int numSpells;

    // Start is called before the first frame update
    void Start()
    {
        paralysisSpellTxt = GetComponent<TextMeshProUGUI>();             // Accesses text Mesh Pro Canvas Spell -deb
        paralysisSpellTxt.text = "Spells: " + 3.ToString();
        paralysisScript = spell.GetComponent<paralysis_script>();
        
    }

    // Update is called once per frame
    void Update()
    {
        numSpells = paralysisScript.numParalysisSpells;
        paralysisSpellTxt.text = "Spells: " + numSpells.ToString();
    }
}
