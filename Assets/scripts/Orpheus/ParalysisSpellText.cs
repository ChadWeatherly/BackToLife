using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParalysisSpellText : MonoBehaviour
{
    private TextMeshProUGUI paralysisSpellTxt;

    // Start is called before the first frame update
    void Start()
    {
        paralysisSpellTxt = GetComponent<TextMeshProUGUI>();
        paralysisSpellTxt.text = "Spells: " + Updater.paralysisSpells;
    }

    // Update is called once per frame
    void Update()
    {
        paralysisSpellTxt.text = "Spells: " + Updater.paralysisSpells;
    }
}
