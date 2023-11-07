using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParalysisSpellText : MonoBehaviour
{
    private TextMeshProUGUI paralysisSpellTxt;                           // Text Mesh Pro Canvas Spell -deb

    // Start is called before the first frame update
    void Start()
    {
        paralysisSpellTxt = GetComponent<TextMeshProUGUI>();             // Accesses text Mesh Pro Canvas Spell -deb
        paralysisSpellTxt.text = "Spells: " + Updater.paralysisSpells; // Updates Canvas with current number of spells -deb
    }

    // Update is called once per frame
    void Update()
    {
        paralysisSpellTxt.text = "Spells: " + Updater.paralysisSpells; // Updates Canvas with new number of spells -deb
    }
}
