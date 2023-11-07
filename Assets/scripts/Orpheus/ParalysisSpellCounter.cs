using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ParalysisSpellCounter : MonoBehaviour
{
    public int spellNum;                            // stores the number of spells -deb

    public void SpellCountUpdater()
    {
        spellNum = Updater.paralysisSpells -= 1;    // updates the spell counter -deb
    }
}
