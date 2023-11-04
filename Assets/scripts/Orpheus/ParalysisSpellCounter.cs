using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalysisSpellCounter : MonoBehaviour
{
    public int spellNum;        // adds to the crystalTxt count - deb

    public void CrystalCollection()
    {
        spellNum = Updater.crystalCount -= 1;     // updates the crystal counter -deb
    }
}
